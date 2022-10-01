using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public CharacterController controller = null;
    [Header("Collisions")]
    [SerializeField] Transform groundCheck = null;
    [SerializeField] float groundDistance = 0f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    [Header("Movement")]
    [SerializeField] float speed = 0f;
    [SerializeField] float gravity = 0f;
    [SerializeField] float jumpHeight = 0f;
    [SerializeField] float verticalVelocity = 0f;
    Vector3 velocity;
    Vector3 move;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool jumpPressed = Input.GetButtonDown("Jump");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = verticalVelocity;

        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (jumpPressed && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * verticalVelocity * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}