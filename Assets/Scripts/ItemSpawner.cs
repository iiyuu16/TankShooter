using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject HPPrefab;
    public GameObject shieldPrefab;

    public static ItemSpawner itemSpawner;

    public float xPosMax = 7f;
    public float xPosMin = 5f;
    public float yPosMax = 2.75f;
    public float yPosMin = -2.75f;
    public float spawnInterval = 5f;
    float adder = 0.5f;

    void Awake()
    {
        if (itemSpawner == null)
        {
            itemSpawner = this;
        }
        else if (itemSpawner != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            for (int i = 0; i < 3; i++)
            {
                SpawnRandomItem();
            }
        }
    }

    void SpawnRandomItem()
    {
        float spawnXPosition = Random.Range(xPosMin + adder, xPosMax + adder);
        float spawnYPosition = Random.Range(yPosMin + adder, yPosMax + adder);

        GameObject itemPrefab = Random.Range(0f, 1f) < 0.5f ? HPPrefab : shieldPrefab;

        GameObject item = Instantiate(itemPrefab, new Vector3(spawnXPosition, spawnYPosition, 0f), Quaternion.identity);
    }
}
