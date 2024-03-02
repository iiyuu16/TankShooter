using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    public int maxHP = 50;
    public int maxShield = 10;
    private int currentHP;
    private int currentShield;

    public GameObject Explosion;
    public GameObject Clash;
    public GameController gameController;

    void Start()
    {
        currentHP = maxHP;
        currentShield = maxShield;
    }

    void Update()
    {
        CheckBaseStatus();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "enemyProjectile")
        {
            Vector2 expos = transform.position;

            if (currentShield > 0)
            {
                currentShield--;
                GameObject clash = (GameObject)Instantiate(Clash);
                gameController.PlayExplosion(gameController.hitSFX);
                explosion.transform.position = expos;
                Debug.Log(" "+currentShield);
            }

            else
            {
                currentHP--;
                GameObject explosion = (GameObject)Instantiate(Explosion);
                gameController.PlayExplosion(gameController.explosionSFX);
                explosion.transform.position = expos;
                Debug.Log(" "+currentHP);
            }



            Destroy(other.gameObject);
        }
    }
    
    private void CheckBaseStatus()
    {
        if(currentHP <= 0)
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
}
