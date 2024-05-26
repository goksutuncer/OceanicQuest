using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoldEvents : MonoBehaviour
{
    public event Action<int> onGoldGained;
    public void GoldGained(int gold)
    {
        if (onGoldGained != null)
        {
            onGoldGained(gold);
        }
    }
    public event Action<int> onGoldChange;
    public void GoldChange(int gold)
    {
        if (onGoldChange != null)
        {
            onGoldChange(gold);
        }
    }
}
