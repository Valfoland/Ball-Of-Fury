using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RandomEvents
{
    ShieldDis,
    ShieldFree,
    TakeDamage,
    IncBurstE,
    DecBurstE
}

public class RandomEventsManager
{
    public static RandomEvents RandomEvent;

    public static int ChooseEvent()
    {
        int k = Random.Range(0, 5);
        switch (k)
        {
            case 0:
                RandomEvent = RandomEvents.ShieldDis;
                break;
            case 1:
                RandomEvent = RandomEvents.ShieldFree;
                break;
            case 2:
                RandomEvent = RandomEvents.IncBurstE;
                break;
            case 3:
                RandomEvent = RandomEvents.DecBurstE;
                break;
            case 4:
                RandomEvent = RandomEvents.TakeDamage;
                break;
        }
        return k;
    }
}
