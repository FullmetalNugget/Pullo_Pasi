using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;

    private PlayerControls controls;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void FixedUpdate()
    {
        if (controls.Player.MoveLeft.IsPressed())
        {
            rb.AddForce(-Vector3.right * speed * Time.deltaTime);
        }
        if (controls.Player.MoveRight.IsPressed())
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime);
        }
        if (controls.Player.MoveUp.IsPressed())
        {
            rb.AddForce(-Vector3.down * speed * Time.deltaTime);
        }
        if (controls.Player.MoveDown.IsPressed())
        {
            rb.AddForce(Vector3.down * speed * Time.deltaTime);
        }
    }
}