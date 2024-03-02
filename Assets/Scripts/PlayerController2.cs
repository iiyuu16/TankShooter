using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerController2 : MonoBehaviour
{
    public static PlayerController2 playerController;
    public float speed;
    public Animator anim;
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
        float x = Input.GetAxisRaw("HorizontalP2");
        float y = Input.GetAxisRaw("VerticalP2");
        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
        Fire();
    }

    void Fire()
    {
        nextFire -= Time.deltaTime;
        if (Input.GetKey(KeyCode.RightShift) && nextFire <= 0)
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

        Vector2 posP2 = transform.position;
        
        posP2 += direction * speed * Time.deltaTime;
        posP2.x = Mathf.Clamp(posP2.x, min.x, max.x);
        posP2.y = Mathf.Clamp(posP2.y, min.y, max.y);

        transform.position = posP2;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "enemyProjectile")
        {
            PlayerStats2.playerStats.playerLife--;
            HitByEnemy();
            Destroy(other.gameObject);
            Vector2 expos = transform.position;
            gameController.PlayExplosion(gameController.explosionSFX);
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
        }

        if (other.tag == "enemyProjectile")
        {
            Vector2 expos = transform.position;
            gameController.PlayHit(gameController.hitSFX);
            GameObject clash = (GameObject)Instantiate(Clash);
            clash.transform.position = expos;
        }

        if (other.tag == "Boss")
        {
            PlayerStats2.playerStats.playerLife--;
            HitByEnemy();
            Vector2 expos = transform.position;
            gameController.PlayExplosion(gameController.explosionSFX);
            GameObject explosion = (GameObject)Instantiate(Explosion);
            explosion.transform.position = expos;
        }

        if (other.tag == "HP")
        {
            PlayerStats2.playerStats.addLife();
            PlayerStats2.playerStats.UpdateLife();
            gameController.PlayItem(gameController.itemSFX);
        }


        if (other.tag == "INVI")
        {
            Debug.Log("p2gotInvi");
            gameController.PlayItem(gameController.itemSFX);
            ImmunityMode();
        }
    }
    public void HitByEnemy()
    {
        if (PlayerStats2.playerStats.playerLife > 0)
        {
            ImmunityMode();
            transform.position = RespawnPoint.transform.position;
        }
        else
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "1P_SpaceShooter")
            {
                gameController.GameOver1_1P();
            }
            else if (currentSceneName == "1P_BossLevel")
            {
                gameController.GameOver2_1P();
            }
            else if (currentSceneName == "2P_SpaceShooter")
            {
                gameController.GameOver1_2P();
            }
            else if (currentSceneName == "2P_BossLevel")
            {
                gameController.GameOver2_2P();
            }
        }
    }

    public void ImmunityMode()
    {
        StartCoroutine(IFrameSprite(8f));
        StartCoroutine(IFrames(8f));
    }

    private IEnumerator IFrameSprite (float seconds)
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        while (seconds > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            seconds -= 0.1f;

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
