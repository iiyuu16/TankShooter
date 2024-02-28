using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerController1 : MonoBehaviour
{
    public static PlayerController1 playerController;
    public float speed;
    public GameObject Projectile;
    public GameObject projectilePosition;
    public float fireInterval = .3f;
    float nextFire;
    public GameObject Explosion;
    public GameObject Clash;

    public bool isGameOver = false;
    public GameObject RespawnPoint;

    public GameController gameController;
    void Awake()
    {
        if(playerController == null)
        {
            playerController = this;
        }
        else if (playerController != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextFire = fireInterval;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        float x = Input.GetAxisRaw("HorizontalP1");
        float y = Input.GetAxisRaw("VerticalP1");
        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
        Fire();
    }

    void Fire()
    {
        nextFire -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && nextFire <= 0)
        {
            GameObject projectile = (GameObject)Instantiate(Projectile);
            projectile.transform.position = projectilePosition.transform.position;
            nextFire = fireInterval;
            gameController.PlayShoot(gameController.shootSFX);
        }
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //fixed reso
        min.x = -6.86f; 
        max.x = 6.86f;

/*      min.x = -10.2f;
        max.x = 10.2f;*/

        min.y = -4.4f;
        max.y = 4.4f;

        Vector2 posP1 = transform.position;
        
        posP1 += direction * speed * Time.deltaTime;
        posP1.x = Mathf.Clamp(posP1.x, min.x, max.x);
        posP1.y = Mathf.Clamp(posP1.y, min.y, max.y);

        transform.position = posP1;
    }

    void Aim(Vector2 direction)
    {
        //rotate barrel
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "enemyProjectile")
        {
            PlayerStats1.playerStats.playerLife--;
            HitByEnemy();
            Destroy(other.gameObject);
            Vector2 expos = transform.position;
            gameController.PlayExplosion(gameController.explosionSFX);
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
        }

        if (other.tag == "Boss")
        {
            PlayerStats1.playerStats.playerLife--;  
            HitByEnemy();
            Vector2 expos = transform.position;
            gameController.PlayExplosion(gameController.explosionSFX);
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
        }

        if (other.tag == "HP")
        {
            PlayerStats1.playerStats.addLife();
            PlayerStats1.playerStats.UpdateLife();
            gameController.PlayItem(gameController.itemSFX);
        }

        if (other.tag == "INVI")
        {
            Debug.Log("p1gotInvi");
            gameController.PlayItem(gameController.itemSFX);
            ImmunityMode();
        }
    }

    public void HitByEnemy()
    {
        if (PlayerStats1.playerStats.playerLife > 0)
        {
            ImmunityMode();
            transform.position = RespawnPoint.transform.position;
        }
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "1P_TankShooter")
            {
                gameController.GameOver1_1P();
            }
            else if (currentSceneName == "1P_BossLevel")
            {
                gameController.GameOver2_1P();
            }
            else if (currentSceneName == "2P_TankShooter")
            {
                gameController.GameOver1_2P();
            }
            else if (currentSceneName == "2P_BossLevel")
            {
                gameController.GameOver2_2P();
            }
        }
    }

    private void ImmunityMode()
    {
        StartCoroutine(IFrameSprite(5f));
        StartCoroutine(IFrames(5f));
    }

    private IEnumerator IFrameSprite (float seconds)
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        while (seconds > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.05f);
            seconds -= 0.05f;

        }

        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;
    }

    private IEnumerator IFrames(float seconds)
    {
        GetComponent<BoxCollider2D>().enabled = false;            
        Debug.Log("immune");
        yield return new WaitForSeconds(seconds);
        GetComponent<BoxCollider2D>().enabled = true;
        Debug.Log("not immune");
    }
}
