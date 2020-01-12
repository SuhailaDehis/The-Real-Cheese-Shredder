using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameStates { MainMenu, Playing, EndMenu }
public class GameManager : MonoBehaviour
{
    #region Public Variables
    public GameObject startgamePanel, gameOverPanel, gamePlayPanel;
    public delegate void CheckState(GameStates currentState);
    public static event CheckState CheckMyStates;
    #endregion
    static GameStates currentState;

    public static GameStates CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            CheckMyStates.Invoke(currentState);
        }
    }
    #region Public Methods
    public void StartGame()
    {
        CurrentState = GameStates.Playing;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        CurrentState = GameStates.EndMenu;
    }

    #endregion


    #region Unity Callbacks
    private void Start()
    {
        CurrentState = GameStates.MainMenu;
    }
    #endregion
}