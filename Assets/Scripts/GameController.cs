using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource BGSource;

    public AudioClip menuBGM;
    public AudioClip defaultBGM;
    public AudioClip bossBGM;
    public AudioClip gameOverBGM;

    public AudioClip explosionSFX;
    public AudioClip hitSFX;
    public AudioClip gameOverSFX;
    public AudioClip shootSFX;
    public AudioClip itemSFX;
    
    public GameObject gameOverScreen;
    public GameObject gamePauseScreen;
    public GameObject timer;
    private Timer time;
    private bool isPaused = false;

    private void Start()
    {
        BGSource.clip = defaultBGM;
        BGSource.Play();
    }
    public void PlayGameOverBGM()
    {
        BGSource.clip = gameOverBGM;
        BGSource.Play();
    }

    public void PlayBossBGM()
    {
        BGSource.clip = bossBGM;
        BGSource.Play();
    }

    void Awake()
    {
        if(gameController == null)
        {
            gameController = this;
        }
        else if(gameController != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void PlayExplosion(AudioClip explosionSFX)
    {
        SFXSource.PlayOneShot(explosionSFX);
    }

    public void PlayHit(AudioClip hitSFX)
    {
        SFXSource.PlayOneShot(hitSFX);
    }

    public void PlayShoot(AudioClip shootSFX)
    {
        SFXSource.PlayOneShot(shootSFX, 0.25f);
    }

    public void PlayItem(AudioClip itemSFX)
    {
        SFXSource.PlayOneShot(itemSFX, 0.3f);
    }

    public void GameOver1_1P()
    {
        EnemySpawner.enemySpawner.gameObject.SetActive(false);
        PlayerController1.playerController.gameObject.SetActive(false);
        PlayGameOverBGM();
        GameOverSFX(gameOverSFX);
        gameOverScreen.SetActive(true);
        timer.SetActive(false);
        time.StopTimer();
    }

    public void GameOver2_1P()
    {
        EnemySpawner.enemySpawner.gameObject.SetActive(false);
        Boss.boss.gameObject.SetActive(false);
        PlayerController1.playerController.gameObject.SetActive(false);
        PlayGameOverBGM();
        GameOverSFX(gameOverSFX);
        gameOverScreen.SetActive(true);
    }

    public void GameOver1_2P()
    {
        EnemySpawner.enemySpawner.gameObject.SetActive(false);
        PlayerController1.playerController.gameObject.SetActive(false);
        PlayerController2.playerController.gameObject.SetActive(false);
        PlayGameOverBGM();
        GameOverSFX(gameOverSFX);
        gameOverScreen.SetActive(true);
        timer.SetActive(false);
        time.StopTimer();
    }

    public void GameOver2_2P()
    {
        EnemySpawner.enemySpawner.gameObject.SetActive(false);
        Boss.boss.gameObject.SetActive(false);
        PlayerController1.playerController.gameObject.SetActive(false);
        PlayerController2.playerController.gameObject.SetActive(false);
        PlayGameOverBGM();
        GameOverSFX(gameOverSFX);
        gameOverScreen.SetActive(true);
    }

    public void GameOverSFX(AudioClip gameOverSFX)
    {
        SFXSource.PlayOneShot(gameOverSFX);
    }

    public void BossBattle_1P()
    {
        PlayBossBGM();
        SceneManager.LoadScene("1P_BossLevel");
    }

    public void BossBattle_2P()
    {
        PlayBossBGM();
        SceneManager.LoadScene("2P_BossLevel");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        BGSource.clip = menuBGM;
        BGSource.Play();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        if(gamePauseScreen != null)
        {
            gamePauseScreen.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        if (gamePauseScreen != null)
        {
            gamePauseScreen.SetActive(false);
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseGame();
        }
        else
            ResumeGame();
    }

}
