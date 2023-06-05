using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float turnSpeed;
    Vector3 input;
    public Controls controls;
    Rigidbody rb;

    private void Awake()
    {
        controls = new();
        controls.Enable();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input = new Vector3(controls.Keyboard.Hrtz.ReadValue<float>(), 0, controls.Keyboard.Vrt.ReadValue<float>());
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        if (input  != Vector3.zero)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(input.ToIso().normalized, Vector3.up), turnSpeed * 100f * Time.deltaTime);
    }

    void Move()
    {
        rb.MovePosition(transform.position + input.ToIso().normalized.magnitude * movementSpeed * Time.deltaTime * transform.forward);
    }
}