using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
    private bool isStartAnim;
    public static System.Action onChooseAbility;
    [SerializeField] private Text textTime;
    [SerializeField] private GameObject[] startTurnObjects;
    [SerializeField] private GameObject eventObject;
    [SerializeField] private Text eventText;

    public void StartChooseAbilityPhase(int time, int maxTime, bool isStart)
    {
        if (isStart)
        {
            startTurnObjects[0].SetActive(true);
            int t = Mathf.Abs(time - maxTime);
            textTime.text = t.ToString();
            if (!isStartAnim)
            {
                onChooseAbility?.Invoke();
                isStartAnim = true;
            }
        }
        else
        {
            startTurnObjects[0].SetActive(false);
            isStart = false;
            textTime.text = "";
        }
    }

    public void StartRandomEventPhase()
    {
        int typeRandomEvent = RandomEventsManager.ChooseEvent();

        eventObject.SetActive(false);
        eventObject.SetActive(true);
        switch (typeRandomEvent)
        {
            case 0:
                eventText.text = "Щиты обесточены";
                break;
            case 1:
                eventText.text = "Возврат энергии щитов";
                break;
            case 2:
                eventText.text = "Увеличение притока энергии";
                break;
            case 3:
                eventText.text = "Разрыв связи с источником";
                break;
            case 4:
                eventText.text = "Повреждение систем";
                break;
        }
    }
   
}
