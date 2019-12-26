using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public Variables
    public static GameManager instance;
    public GameObject startgamePanel, gameOverPanel;
    public ShreddingScript shreddingScript;


    
    #endregion

    #region Public Methods
    public void StartGame()
    {
        startgamePanel.SetActive(false);
        shreddingScript.cheeseSpeed = 5;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
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