using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected = 0;
    private int coinsToComplete = 2;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected += CoinCollected;
    }

    private void OnDisable()
    {
        if(GameEventsManager.instance != null)
        {
            GameEventsManager.instance.miscEvents.onCoinCollected -= CoinCollected; 
        }
        else
        {
            return;
        }

    }

    private void CoinCollected()
    {
        if(coinsCollected < coinsToComplete)
        {
            coinsCollected++;
            UpdateState();
        }
        if(coinsCollected >= coinsToComplete)
        {
            FinishQuestStep();
        }
    }
    private void UpdateState()
    {
        string state = coinsCollected.ToString();
        string status = "Collected " + coinsCollected + " / " + coinsToComplete + " coins.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.coinsCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
