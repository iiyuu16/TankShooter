using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss boss;
    public float speed = -.8f;
    public GameObject enemyProj;
    public GameObject enemyProj2;
    public GameObject enemyProj3;
    public GameObject enemyProj4;
    public GameObject ProjectilePostition;
    public GameObject ProjectilePostition2;
    public GameObject ProjectilePostition3;
    public GameObject ProjectilePostition4;
    public GameObject Explosion;
    public GameObject Clash;
    GameController gameController;
    float fireInterval;
    float fireInterval2;
    float fireInterval3;
    float fireInterval4;
    float waitToFire;
    float waitToFire2;
    float waitToFire3;
    float waitToFire4;
    public int bossHP = 5;
    public float bossPos = 4f;
    private int randomNo;
    private int randomNo2;
    private int randomNo3;
    private int randomNo4;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        if (boss == null)
        {
            boss = this;
        }
        else if (boss != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateRandomNumber());

        fireInterval = randomNo;
        waitToFire = fireInterval;

        fireInterval2 = randomNo2;
        waitToFire2 = fireInterval2;

        fireInterval3 = randomNo3;
        waitToFire3 = fireInterval3;

        fireInterval4 = randomNo4;
        waitToFire4 = fireInterval4;
    }

    // Update is called once per frame
    void Update()
    {
        bossMove();
    }


    public void bossMove()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x + speed * Time.deltaTime, position.y);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));


        if (Mathf.Abs(transform.position.x - bossPos) < 0.1f)
        {
            speed = 0;
            bossAttack();
            Debug.Log("boss on pos");
        }

        if (transform.position.x < min.x)
        {
            Destroy(gameObject);
        }
    }

    public void bossAttack()
    {
        waitToFire -= Time.deltaTime;
        waitToFire2 -= Time.deltaTime;
        waitToFire3 -= Time.deltaTime;
        waitToFire4 -= Time.deltaTime;

        if (waitToFire < -1)
        {
            FireProjectile1();
            waitToFire = fireInterval;
        }

        if (waitToFire2 < -1)
        {
            FireProjectile2();
            waitToFire2 = fireInterval2;
        }

        if (waitToFire3 < -1)
        {
            FireProjectile3();
            waitToFire3 = fireInterval3;
        }

        if (waitToFire4 < -1)
        {
            FireProjectile4();
            waitToFire4 = fireInterval4;
        }
    }

    void FireProjectile1()
    {
        GameObject enemyproj = (GameObject)Instantiate(enemyProj);
        enemyproj.transform.position = ProjectilePostition.transform.position;
    }
    void FireProjectile2()
    {
        GameObject enemyproj2 = (GameObject)Instantiate(enemyProj2);
        enemyproj2.transform.position = ProjectilePostition2.transform.position;
    }
    void FireProjectile3()
    {
        GameObject enemyproj3 = (GameObject)Instantiate(enemyProj3);
        enemyproj3.transform.position = ProjectilePostition3.transform.position;
    }
    void FireProjectile4()
    {
        GameObject enemyproj4 = (GameObject)Instantiate(enemyProj4);
        enemyproj4.transform.position = ProjectilePostition4.transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 expos = transform.position;

            if (bossHP != 0)
            {
                bossHP--;
                gameController.PlayHit(gameController.hitSFX);
                Debug.Log("boss hp: " + bossHP);
                GameObject clash = (GameObject)Instantiate(Clash);
                clash.transform.position = expos;
            }

            else if (bossHP == 0)
            {                
                Debug.Log("boss dead");
                gameController.PlayExplosion(gameController.explosionSFX);
                KillBoss();
                GameObject explosion = (GameObject)Instantiate(Explosion);
                explosion.transform.position = expos;
                SceneManager.LoadScene("End");
            }
        }
    }

    public void KillBoss()
    {
        if (bossHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator UpdateRandomNumber()
    {
        while (true)
        {
            randomNo = Random.Range(0, 4);
            randomNo2 = Random.Range(0, 4);
            randomNo3 = Random.Range(0, 4);
            randomNo4 = Random.Range(0, 4);

            yield return new WaitForSeconds(3f);
        }
    }
}
