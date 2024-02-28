using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab1;
    public GameObject asteroidPrefab2;

    public static AsteroidSpawner asteroidSpawner;

    public float xPosMax = 7f;
    public float xPosMin = 5f;
    public float yPosMax = 2.75f;
    public float yPosMin = -2.75f;
    public float spawnInterval = 12f;
    public int spawnCount = 5;
    float adder = 0.5f;
    void Awake()
    {
        if (asteroidSpawner == null)
        {
            asteroidSpawner = this;
        }
        else if (asteroidSpawner != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnRandomItem();
            }
        }
    }

    void SpawnRandomItem()
    {
        float spawnXPosition = Random.Range(xPosMin + adder , xPosMax + adder);
        float spawnYPosition = Random.Range(yPosMin + adder, yPosMax + adder);

        GameObject itemPrefab = Random.Range(0f, 1f) < 0.5f ? asteroidPrefab1 : asteroidPrefab2;

        GameObject item = Instantiate(itemPrefab, new Vector3(spawnXPosition, spawnYPosition, 0f), Quaternion.identity);
    }
}
