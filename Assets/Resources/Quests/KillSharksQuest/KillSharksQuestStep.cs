using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSharksQuestStep : QuestStep
{
    private int sharksKilled = 0;
    private int sharksToComplete = 2;


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
