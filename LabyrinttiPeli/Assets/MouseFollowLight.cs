using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollowLight : MonoBehaviour
{
    public Camera mainCamera;
    public float rotationSpeed = 10f;

    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (Mouse.current == null)
            return;

        // Get mouse position via the new Input System
        Vector2 mouseScreenPos2D = Mouse.current.position.ReadValue();
        Vector3 mouseScreenPos = new Vector3(mouseScreenPos2D.x, mouseScreenPos2D.y, 0f);
        mouseScreenPos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        // Direction from light to mouse point
        Vector3 direction = mouseWorldPos - transform.position;
        direction.x = 0f; // lock rotation to Y axis only

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}