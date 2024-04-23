using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public GameUIManager gameUI_Manager;
    public DiverPlayer playerCharacter;
    private bool gameIsOver;

    // Static reference to the instance
    private static GameManager instance;

    // Getter for the instance
    public static GameManager Instance
    {
        get
        {
            // If there is no GameManager instance yet, find it in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // If it's still null, create a new GameObject and add the GameManager component to it
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public void GameOver()
    {
        gameIsOver = true;
        gameUI_Manager.ShowGameOverUI();
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
