using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI CoinText;
    public Slider HealthSlider;
    public GameObject UI_Pause;
    public GameObject UI_GameOver;
    public GameObject UI_GameIsFinished;
    public DiverPlayer _player;


    // State Machine for UI
    private enum GameUI_State
    {
        GamePlay, Pause, GameOver, GameIsFinished
    }
    GameUI_State currentState;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<DiverPlayer>();
        SwitchUIState(GameUI_State.GamePlay);
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = _player.Health.CurrentHealthPercentage;
        CoinText.text = _player.Coin.ToString();
    }

    private void SwitchUIState(GameUI_State state)
    {
        UI_Pause.SetActive(false);
        UI_GameOver.SetActive(false);
        UI_GameIsFinished.SetActive(false);

        Time.timeScale = 1;

        switch (state)
        {
            case GameUI_State.GamePlay:
                break;
            case GameUI_State.Pause:
                Time.timeScale = 0;
                UI_Pause.SetActive(true);
                break;
            case GameUI_State.GameOver:
                UI_GameOver.SetActive(true);
                break;
            case GameUI_State.GameIsFinished:
                UI_GameIsFinished.SetActive(true);
                break;
        }
        currentState = state;
    }
    public void TogglePauseUI()
    {
        if (currentState == GameUI_State.GamePlay)
        {
            SwitchUIState(GameUI_State.Pause);
        }
        else if (currentState == GameUI_State.Pause)
        {
            SwitchUIState(GameUI_State.GamePlay);
        }
    }
    public void Button_MainMenu()
    {
        GameManager.Instance.ReturnToTheMainMenu();
    }
    public void Button_Restart()
    {
        GameManager.Instance.Restart();
    }
    public void ShowGameOverUI()
    {
        SwitchUIState(GameUI_State.GameOver);
    }
    public void ShowGameIsFinishedUI()
    {
        SwitchUIState(GameUI_State.GameIsFinished);
    }
}
