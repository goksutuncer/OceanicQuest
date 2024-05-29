using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSharksQuestStep : QuestStep
{
    private int sharksKilled = 0;
    private int sharksToComplete = 2;
    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onSharkKilled += SharksKilled;
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null)
        {
            GameEventsManager.instance.miscEvents.onSharkKilled -= SharksKilled;
        }
        else
        {
            return;
        }

    }

    private void SharksKilled()
    {
        if (sharksKilled < sharksToComplete)
        {
            sharksKilled++;
            UpdateState();
        }
        if (sharksKilled >= sharksToComplete)
        {
            FinishQuestStep();
        }
    }
    private void UpdateState()
    {
        string state = sharksKilled.ToString();
        string status = "Killed " + sharksKilled + " / " + sharksToComplete + " sharks.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        UpdateState();
    }
}
