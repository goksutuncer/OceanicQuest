using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    private void OnEnable()
    {
        if(GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerEvents.onPlayerExperienceChange += PlayerExperienceChange;
            GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.playerEvents.onPlayerExperienceChange -= PlayerExperienceChange;
            GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
        }
    }

    private void PlayerExperienceChange(int experience)
    {
        xpSlider.value = (float)experience / (float)100;
    }

    private void PlayerLevelChange(int level)
    {
        levelText.text = "Level " + level;
    }
}
