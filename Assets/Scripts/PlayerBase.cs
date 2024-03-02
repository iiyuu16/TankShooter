using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    public float maxHP = 20f;
    public float maxShield = 10f;

    public float currentHP;
    public float currentShield;

    public GameObject Explosion;
    public GameObject Clash;
    public GameController gameController;

    private Coroutine shieldRegenCoroutine;
    private bool isShieldRegenerating = false;
    private float timeSinceLastHit = 0f;

    void Start()
    {
        currentHP = maxHP;
        currentShield = maxShield;
    }

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        if (timeSinceLastHit >= 5f && !isShieldRegenerating && currentShield < maxShield)
        {
            StartShieldRegeneration();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "enemyProjectile")
        {
            Vector2 expos = transform.position;

            if (currentShield > 0)
            {
                currentShield--;
                GameObject clash = Instantiate(Clash);
                gameController.PlayExplosion(gameController.hitSFX);
                clash.transform.position = expos;
                Debug.Log("Shield: " + currentShield);
            }
            else
            {
                currentHP--;
                GameObject explosion = Instantiate(Explosion);
                gameController.PlayExplosion(gameController.explosionSFX);
                explosion.transform.position = expos;
                Debug.Log("HP: " + currentHP);
                CheckBaseStatus();
            }

            timeSinceLastHit = 0f; // Reset time since last hit
            Destroy(other.gameObject);
        }
    }

    private void CheckBaseStatus()
    {
        if (currentHP <= 0)
        {
            Time.timeScale = 0;

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

    private void StartShieldRegeneration()
    {
        shieldRegenCoroutine = StartCoroutine(ShieldRegenerationCoroutine());
    }

    private IEnumerator ShieldRegenerationCoroutine()
    {
        isShieldRegenerating = true;

        while (currentShield < maxShield)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second before regenerating shield
            currentShield++;
            Debug.Log("Shield Regenerated: " + currentShield);
        }

        isShieldRegenerating = false; // Reset the flag when shield regeneration is complete
    }

    private void OnDestroy()
    {
        StopAllCoroutines(); // Stop all coroutines when the object is destroyed
    }
}
