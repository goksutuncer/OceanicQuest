using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvents : MonoBehaviour
{
    public event Action<string> onStartQuest;
    public void StartQuest (string id)
    {
        if(onStartQuest != null)
        {
            onStartQuest(id);
        }
    }

    public event Action<string> onAdvanceQuest;
    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest;
    public void FinishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    /*public event Action<string> onQuestStateChange;
    public void QuestStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStateChange != null)
        {
            onQuestStateChange(id, stepIndex, questStepState);
        }
    }*/
}
