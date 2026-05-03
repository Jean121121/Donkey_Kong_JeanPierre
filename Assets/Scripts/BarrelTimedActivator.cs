using UnityEngine;

public class BarrelTimedActivator : MonoBehaviour
{
    public GameObject[] barrels;
    public float[] activationTimes;

    void Start()
    {
        for (int i = 0; i < barrels.Length; i++)
        {
            if (barrels[i] != null)
            {
                barrels[i].SetActive(false);
            }

            if (i < activationTimes.Length)
            {
                StartCoroutine(ActivateBarrel(i, activationTimes[i]));
            }
        }
    }

    System.Collections.IEnumerator ActivateBarrel(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (barrels[index] != null)
        {
            barrels[index].SetActive(true);
        }
    }
}