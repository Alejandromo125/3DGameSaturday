using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;
    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;
    public int points = 0;

    AudioSource audioSource;
    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    public Text timeText;
    public Text goldKeyText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text crystalText;
    public Image snowFlake;
    public GameObject infoPanel;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useInfo;
    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        snowFlake.enabled = true;
        InvokeRepeating("Stopper", freez, 1);
    }
    public void AddPoints(int point)
    {
        points += point;
        crystalText.text = points.ToString();
    }
    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        timeText.text = timeToEnd.ToString();
    }
    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
            goldKeyText.text = goldKey.ToString();
        }
        else if (color == KeyColor.Green)
        {
            greenKey++;
            greenKeyText.text = greenKey.ToString();
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
            redKeyText.text = redKey.ToString();
        }
    }
    void Start()
    {
       if(gameManager == null)
       {
            gameManager = this;
       }

       if(timeToEnd <= 0) 
       {
            timeToEnd = 100;
       }
        snowFlake.enabled = false;
        timeText.text = timeToEnd.ToString();
        infoPanel.SetActive(false);
        reloadInfo.text = "";
        SetUseInfo("");
        Debug.Log("Time: " + timeToEnd + " s");
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stopper", 2, 1);
    }

    void Update()
    {
        PickUpCheck();
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void PickUpCheck()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Actual Time: " + timeToEnd);
            Debug.Log("Key red: " + redKey + " green: " + greenKey + " gold: " + goldKey);
            Debug.Log("Points: " + points);
        }
    }
    void Stopper()
    {
        timeToEnd--;
        Debug.Log("Time: " + timeToEnd + " s");
        timeText.text = timeToEnd.ToString();
        snowFlake.enabled = true;
        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }

        if (endGame)
        {
            EndGame();
        }
    }

    public void PauseGame()
    {
        if(!endGame)
        {
            PlayClip(pauseClip);
            infoPanel.SetActive(true);
            Debug.Log("Pause Game");
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }

    public void ResumeGame()
    {
        if(!endGame)
        {
            PlayClip(resumeClip);
            infoPanel.SetActive(false);
            Debug.Log("Resume Game");
            Time.timeScale = 1f;
            gamePaused = false;
        }
    }

    public void EndGame()
    {
        CancelInvoke("Stopper");
        infoPanel.SetActive(true);
        Time.timeScale = 0f;
        if (win)
        {
            PlayClip(winClip);
            pauseEnd.text = "You WIN!!!!";
            reloadInfo.text = "Reload? Y/N";
            Debug.Log("You Win!!! Reoad?");
        } else
        {
            PlayClip(loseClip);
            pauseEnd.text = "You LOSE!!!!";
            reloadInfo.text = "Reload? Y/N";
            Debug.Log("You Lose!!! Reload?");
        }
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    public void SetUseInfo(string info)
    {
        useInfo.text = info;
    }
}
