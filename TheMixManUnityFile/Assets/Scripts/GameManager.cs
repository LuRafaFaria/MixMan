using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;
    public string nextLevel = "TuesdayLevel";
    public int levelToUnlock = 2;
    public SceneFader sceneFader;

    void Start()
    {
        Application.targetFrameRate = 60;
        //GameIsOver = false;
    }

    void Update()
    {
        /*if(GameIsOver)  
            return;*/
        
    }

    void EndGame()
    {
        /*GameIsOver = true;
        gameOverUI.SetActive(true);*/
    }

    public void WinLevel()
    {
        Debug.Log("aha you won, loser");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }
}
