using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject barrelPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;

    private float timer = 0f;

    void Update()
    {
        if (barrelPrefab == null || spawnPoint == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Instantiate(barrelPrefab, spawnPoint.position, Quaternion.identity);
            timer = 0f;
        }
    }
}