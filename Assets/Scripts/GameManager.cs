using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private DiverPlayer _diverPlayer;
    private bool _isGameOver;

    private void Awake()
    {
        _diverPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<DiverPlayer>();

        _diverPlayer.OnPlayerDied += OnPlayerDied;
    }

    private void OnDestroy()
    {
        _diverPlayer.OnPlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        GameOver();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
