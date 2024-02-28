using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed;
    public GameObject enemyProj;
    public GameObject ProjectilePostition;
    float fireInterval = 1f;
    float waitToFire;

    // Start is called before the first frame update
    void Start()
    {
        speed = -1.0f;
        waitToFire = fireInterval;
    }

    // Update is called once per frame
    void Update()
    {
        waitToFire -= Time.deltaTime;

        if (waitToFire < 0)
        {
            FireProjectile();
            waitToFire = fireInterval;
        }

        Vector2 position = transform.position;
        position = new Vector2 (position.x + speed * Time.deltaTime, position.y);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.x < min.x)
        {
            Destroy(gameObject);
        }
    }

    void FireProjectile()
    {
        GameObject enemyproj = (GameObject)Instantiate(enemyProj);
        enemyproj.transform.position = ProjectilePostition.transform.position;
    }
}
