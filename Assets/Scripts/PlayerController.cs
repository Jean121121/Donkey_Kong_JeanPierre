using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform visual;

    private Rigidbody rb;
    private bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1f;

        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

        Vector3 v = rb.linearVelocity;
        v.x = x * moveSpeed;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && grounded)
        {
            v.y = jumpForce;
        }

        rb.linearVelocity = v;

        if (visual != null)
            {
                if (x < 0) visual.rotation = Quaternion.Euler(0, -90, 0);
                if (x > 0) visual.rotation = Quaternion.Euler(0, 90, 0);
            }
    }
}