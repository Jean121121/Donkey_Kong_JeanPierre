using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public float groundDistance = 0.25f;     // distancia del raycast hacia abajo
    public LayerMask groundLayer;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // --- Input horizontal (A/D o flechas) ---
        float x = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1f;
        }

        // --- Movimiento ---
        Vector3 v = rb.linearVelocity;
        v.x = x * moveSpeed;

        // --- Suelo (Raycast) ---
        bool grounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);

        // --- Salto ---
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("SPACE | grounded=" + grounded);

            if (grounded)
                v.y = jumpForce;
        }

        rb.linearVelocity = v;
    }

    // (Opcional) Para ver el rayo en Scene mientras juegas
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundDistance);
    }
}
