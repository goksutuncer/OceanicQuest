using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MiscEvents : MonoBehaviour
{
    public event Action onKoiFishesCollected;
    public void KoiFishCollected()
    {
        if (onKoiFishesCollected != null)
        {
            onKoiFishesCollected();
        }
    }

    public event Action onSharkKilled;
    public void SharkKilled()
    {
        if (onSharkKilled != null)
        {
            onSharkKilled();
        }
    }

    public event Action onCoinCollected;
    public void CoinCollected()
    {
        if (onCoinCollected != null)
        {
            onCoinCollected();
        }
    }

    public event Action onGemCollected;
    public void GemCollected()
    {
        if (onGemCollected != null)
        {
            onGemCollected();
        }
    }
}
