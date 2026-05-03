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

    public int maxJumps = 2;

    // 🔊 AUDIO
    public AudioSource audioSource;
    public AudioClip jumpSound;

    private Rigidbody rb;
    private bool grounded;
    private int jumpsLeft;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpsLeft = maxJumps;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float x = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1f;

        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

        if (grounded)
        {
            jumpsLeft = maxJumps;
        }

        Vector3 v = rb.linearVelocity;
        v.x = x * moveSpeed;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsLeft > 0)
        {
            v.y = jumpForce;
            jumpsLeft--;

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        rb.linearVelocity = v;

        if (visual != null)
        {
            if (x < 0) visual.rotation = Quaternion.Euler(0, -90, 0);
            if (x > 0) visual.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}