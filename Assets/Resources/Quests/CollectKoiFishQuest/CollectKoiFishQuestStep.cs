using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKoiFishQuestStep : QuestStep
{
    private int koiFishCollected = 0;
    private int koiFishToComplete = 2;

    private void KoiFishesCollected()
    {
        if (koiFishCollected < koiFishToComplete)
        {
            koiFishCollected++;
            UpdateState();
        }
        if (koiFishCollected >= koiFishToComplete)
        {
            FinishQuestStep();
        }
    }
    private void UpdateState()
    {
        string state = koiFishCollected.ToString();
        string status = "Collected " + koiFishCollected + " / " + koiFishToComplete + " Koi Fishes.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        UpdateState();
    }

}
