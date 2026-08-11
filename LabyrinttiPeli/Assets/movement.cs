using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{

    public float speed;

    void Awake()
    {

    }

    void FixedUpdate()
    {

        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody>().AddForce(-Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey("w"))
        {
            GetComponent<Rigidbody>().AddForce(-Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * speed * Time.deltaTime);
        }
    }
}