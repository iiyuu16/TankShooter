using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public static EnemySpawner enemySpawner;

    public float xPosMax = 7f;
    public float xPosMin = 5f;
    public float yPosMax = 2.75f;
    public float yPosMin = -2.75f;
    float spawnInterval;
    public float difficultyMult = 0f;
    private float timer = 0f;
    private float difficultyIncreaseInterval = 8f; // Increase difficulty every x seconds
    float adder = 0.5f;
    void Awake()
    {
        if (enemySpawner == null)
        {
            enemySpawner = this;
        }
        else if (enemySpawner != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnInterval = Random.Range(1, 3);
    }

    void Update()
    {
        spawnInterval -= Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= difficultyIncreaseInterval)
        {
            difficultyMult += 0.1f;
            timer = 0f;
            Debug.Log("diff: "+ difficultyMult);
        }

        if (spawnInterval <= difficultyMult)
        {
            float spawnXPosition = Random.Range(xPosMin + adder, xPosMax + adder);
            float spawnYPosition = Random.Range(yPosMin + adder, yPosMax + adder);

            GameObject enemyShip = Instantiate(enemyPrefab);
            enemyShip.transform.position = new Vector2(spawnXPosition, spawnYPosition);

            spawnInterval = Random.Range(1, 3);
        }
    }
}
