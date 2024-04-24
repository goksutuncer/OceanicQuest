using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI_Manager : MonoBehaviour
{
    public void Button_Start()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ButtonQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
