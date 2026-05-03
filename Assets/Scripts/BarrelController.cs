using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrelController : MonoBehaviour
{
    public float speed = 1.5f;
    public float dropSpeed = 3f;

    // 5 caídas diferentes
    public float[] dropDistances = { 3.6f, 3f, 2.5f, 4f, 3f };

    private int direction = 1;
    private float fixedY;
    private float fixedZ;
    private bool dropping = false;
    private float targetY;
    private int fallCount = 0;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        if (dropping)
        {
            float newY = Mathf.MoveTowards(
                transform.position.y,
                targetY,
                dropSpeed * Time.deltaTime
            );

            transform.position = new Vector3(
                transform.position.x,
                newY,
                fixedZ
            );

            if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
            {
                fixedY = targetY;
                dropping = false;
            }

            return;
        }

        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        transform.position = new Vector3(
            transform.position.x,
            fixedY,
            fixedZ
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fallzone") && !dropping)
        {
            float distance = dropDistances[
                Mathf.Min(fallCount, dropDistances.Length - 1)
            ];

            targetY = fixedY - distance;
            dropping = true;

            fallCount++;
        }
    }
}