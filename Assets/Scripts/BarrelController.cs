using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.AddForce(Vector3.right * 2f, ForceMode.Impulse);
    }
}