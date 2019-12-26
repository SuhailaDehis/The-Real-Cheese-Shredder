using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public Variables
    public static GameManager instance;
    public GameObject startgamePanel, gameOverPanel,gamePlayPanel;
    public ShreddingScript shreddingScript;


    
    #endregion

    #region Public Methods
    public void StartGame()
    {
        startgamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        shreddingScript.cheeseSpeed = 5;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        startgamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gamePlayPanel.SetActive(false);
    }

    #endregion


    #region Unity Callbacks
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
}