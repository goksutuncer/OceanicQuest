using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBluePowderTangQuestStep : QuestStep
{
    private int blueTangFishCollected = 0;
    private int blueTangFishToComplete = 2;

    private void BlueTangCollected()
    {
        if (blueTangFishCollected < blueTangFishToComplete)
        {
            blueTangFishCollected++;
            UpdateState();
        }
        if (blueTangFishCollected >= blueTangFishToComplete)
        {
            FinishQuestStep();
        }
    }
    private void UpdateState()
    {
        string state = blueTangFishCollected.ToString();
        string status = "Collected " + blueTangFishCollected + " / " + blueTangFishToComplete + " Blue Powder Tang Fishes.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        UpdateState();
    }
}
