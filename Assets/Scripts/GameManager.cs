using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public GameUIManager gameUI_Manager;
    private bool gameIsOver;

    // Static reference to the instance
    private static GameManager instance;

    // Getter for the instance
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public Action OnGameOver { get; set; }

    public void GameOver()
    {
        gameIsOver = true;
        gameUI_Manager.ShowGameOverUI();
        OnGameOver?.Invoke();
    }

    public void GameFinished()
    {
        gameUI_Manager.ShowGameOverUI();
    }

    private void Update()
    {
        if (gameIsOver)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameUI_Manager.TogglePauseUI();
        }
    }

    void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToTheMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
