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

    private void Awake()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<DiverPlayer>();
    }

    private void GameOver()
    {
        gameUI_Manager.ShowGameOverUI();
    }
    private void GameFinished()
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
        if (playerCharacter.PlayerStateController.CurrentState == EDiverPlayerState.Dead)
        {
            gameIsOver = true;
            GameOver();
        }
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(0);
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
