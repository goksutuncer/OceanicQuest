using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI questNameText;
    public TMPro.TextMeshProUGUI questDescriptionText;

    private void Start()
    {
        UpdateQuestDisplay();
    }

    public void UpdateQuestDisplay()
    {
        // Assuming the first quest in the list is the active quest
        var activeQuest = QuestManager.Instance.quests[0]; // Modify this logic based on your quest activation rules

        questNameText.text = activeQuest.name;
        questDescriptionText.text = activeQuest.description;
        // Update other UI elements as needed
    }
}
