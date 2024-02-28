using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float currentTime;
    int startMin = 3;
    public TextMeshProUGUI currentTimeText;

    private void Awake()
    {
        StartTimer();
    }

    void Start()
    {
        currentTime = startMin * 60;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime; 
            if(currentTime <= 0)
            {
                timerActive = false;

                string currentSceneName = SceneManager.GetActiveScene().name;
                if (currentSceneName == "1P_SpaceShooter")
                {
                    GameController.gameController.BossBattle_1P();
                }
                else if (currentSceneName == "2P_SpaceShooter")
                {
                    GameController.gameController.BossBattle_2P();
                }

                Debug.Log("times up");
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
