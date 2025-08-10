using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float groundDrag;

    [Header("Ground Check")]
    [SerializeField] float playerHeight = 2.0f;
    [SerializeField] LayerMask Ground;
    bool grounded;

    [SerializeField] GameObject orientation;

    Rigidbody rb;

    Vector3 moveVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Send raycast (from position, which direction, what length, what layer mask)
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        GetInput();
        SpeedControl();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void GetInput()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        // Set the move direction to be aligned with how the camera is oriented.
        moveVector = orientation.transform.right * moveVector.x + orientation.transform.forward * moveVector.y;
        moveVector = moveVector.normalized;

        rb.AddForce(moveVector * moveSpeed, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVelo = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.y);

        if (flatVelo.magnitude > moveSpeed)
        {
            Vector3 limitedVelo = flatVelo.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelo.x, rb.linearVelocity.y, limitedVelo.z);
        }
    }
}
