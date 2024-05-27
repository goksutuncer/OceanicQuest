using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    private static GameEventsManager _instance;
    public static GameEventsManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameEventsManager>();
            }

            return _instance;
        }
    }

    public PlayerEvents playerEvents = new PlayerEvents();
    public GoldEvents goldEvents = new GoldEvents();
    public MiscEvents miscEvents = new MiscEvents();
    public QuestEvents questEvents = new QuestEvents();
    public InputEvents inputEvents = new InputEvents();
}
