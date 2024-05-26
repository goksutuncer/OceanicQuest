using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> quests;
    private Dictionary<string, int> progressTracker = new Dictionary<string, int>();

    public static QuestManager Instance { get; private set; } 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
    }
    public void UpdateProgress(string targetType, int amount)
    {
        foreach (var quest in quests)
        {
            foreach (var objective in quest.objectives)
            {
                if (objective.target == targetType && !progressTracker.ContainsKey(quest.name))
                {
                    progressTracker[quest.name] = 0;
                }

                if (objective.target == targetType)
                {
                    progressTracker[quest.name] += amount;
                    CheckQuestCompletion(quest);
                }
            }
        }
    }

    private void CheckQuestCompletion(QuestData quest)
    {
        bool isComplete = true;
        foreach (var objective in quest.objectives)
        {
            if (!progressTracker.ContainsKey(objective.type + objective.target) || progressTracker[objective.type + objective.target] < objective.count)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            CompleteQuest(quest);
        }
    }

    private void CompleteQuest(QuestData quest)
    {
        Debug.Log($"Quest '{quest.name}' completed!");
        // Distribute rewards
        // Remove quest from active quests
    }
}
