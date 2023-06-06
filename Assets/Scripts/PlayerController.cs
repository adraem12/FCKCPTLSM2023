using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float turnSpeed;
    public ParticleSystem waterRipples;
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

    void SpawnRipples(int start, int end, int delta, int speed, int size, int lifetime)
    {
        Vector3 forward = waterRipples.transform.eulerAngles;
        forward.y = start;
        waterRipples.transform.eulerAngles = forward;
        for(int i = start; i < end; i += delta)
        {
            waterRipples.Emit(transform.position + waterRipples.transform.forward * 0.5f, waterRipples.transform.forward * speed, size, lifetime, Color.white);
            waterRipples.transform.eulerAngles += Vector3.up * 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 4 && rb.velocity.magnitude > 0.01f)
            SpawnRipples(-180, 180, 3, 2, 2, 2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 4 && rb.velocity.magnitude > 0.01f && Time.renderedFrameCount % 5 == 0)
        {
            int y = (int)transform.position.y;
            SpawnRipples(y-90, y+90, 3, 5, 2, 1);
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
            SpawnRipples(-180, 180, 3, 2, 2, 2);
    }
    */
}