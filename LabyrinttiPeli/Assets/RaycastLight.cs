using System.Collections.Generic;
using UnityEngine;

// Attach to any GameObject with a MeshFilter + MeshRenderer.
// Casts rays 360 degrees around the object on the X/Y plane and
// builds a light mesh from what they hit. Recalculates every frame
// so it stays accurate as the light or obstacles move.
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RaycastLight2D : MonoBehaviour
{
    [Header("Light Shape")]
    public float radius = 8f;
    [Range(8, 720)] public int rayCount = 180;
    public LayerMask obstacleMask;

    [Header("Extra Rays")]
    [Tooltip("Cast an extra pair of rays slightly offset around each obstacle edge, so corners don't get clipped short.")]
    public bool castEdgeRays = true;
    public float edgeAngleOffset = 0.2f; // degrees

    private Mesh lightMesh;
    private MeshFilter meshFilter;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        lightMesh = new Mesh { name = "RaycastLight2D Mesh" };
        meshFilter.mesh = lightMesh;
    }

    void LateUpdate()
    {
        List<Vector3> points = new List<Vector3>();
        List<float> angles = GetSortedAngles();

        foreach (float angle in angles)
        {
            points.Add(CastRay(angle));
        }

        BuildMesh(points);
    }

    // Builds the list of angles to cast: evenly spaced base rays,
    // plus extra rays hugging each obstacle corner for sharper shadows.
    List<float> GetSortedAngles()
    {
        List<float> angles = new List<float>();

        for (int i = 0; i < rayCount; i++)
        {
            angles.Add((360f / rayCount) * i);
        }

        if (castEdgeRays)
        {
            Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, radius, obstacleMask);
            foreach (var col in obstacles)
            {
                // Approximate corners using the collider's bounds.
                Bounds b = col.bounds;
                Vector3[] corners =
                {
                    new Vector3(b.min.x, b.min.y),
                    new Vector3(b.min.x, b.max.y),
                    new Vector3(b.max.x, b.min.y),
                    new Vector3(b.max.x, b.max.y),
                };

                foreach (var corner in corners)
                {
                    float baseAngle = AngleTo(corner);
                    angles.Add(baseAngle - edgeAngleOffset);
                    angles.Add(baseAngle + edgeAngleOffset);
                }
            }
        }

        angles.Sort();
        return angles;
    }

    float AngleTo(Vector3 worldPoint)
    {
        Vector3 dir = worldPoint - transform.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    // Casts a single ray at the given angle (degrees, X/Y plane) and
    // returns either the hit point or the max-radius point if nothing's hit.
    Vector3 CastRay(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, radius, obstacleMask);

        Vector3 worldPoint = hit.collider != null
            ? (Vector3)hit.point
            : (Vector3)((Vector2)transform.position + dir * radius);

        return transform.InverseTransformPoint(worldPoint); // local space for the mesh
    }

    // Turns the ray hit points into a triangle-fan mesh centered on this object.
    void BuildMesh(List<Vector3> points)
    {
        Vector3[] vertices = new Vector3[points.Count + 1];
        vertices[0] = Vector3.zero; // center
        for (int i = 0; i < points.Count; i++)
            vertices[i + 1] = points[i];

        int triCount = points.Count;
        int[] triangles = new int[triCount * 3];
        for (int i = 0; i < triCount; i++)
        {
            int next = (i + 1) % triCount;
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = next + 1;
        }

        lightMesh.Clear();
        lightMesh.vertices = vertices;
        lightMesh.triangles = triangles;
        lightMesh.RecalculateBounds();
        lightMesh.RecalculateNormals();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}