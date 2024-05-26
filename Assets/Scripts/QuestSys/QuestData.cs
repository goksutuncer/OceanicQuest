using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData
{
    public string name;
    public string description;
    public Objective[] objectives;
    public Reward reward;
}
[Serializable]
public class Objective
{
    public string type; // Type of objective, e.g., "kill", "collect"
    public string target; // Target entity, e.g., "shark", "blueFish"
    public int count; // Number required to complete the objective
}
[Serializable]
public class Reward
{
    public int gold;
    public int experience;
    // Add other types of rewards as needed
}


