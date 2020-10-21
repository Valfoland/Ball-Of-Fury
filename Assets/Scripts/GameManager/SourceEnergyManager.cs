using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceEnergyManager : MonoBehaviour
{
    [Header("Количество энергии в источнике")]
    public int CurrentGeneralHealth;
    private static Text textCurrentEnergy;
    private static Slider sliderEnergy;
    public static int CountHealth { get; private set; }
    public static int CountMaxHealth { get; private set; }
    public static int TempCountHealth;

    [SerializeField] private GameObject textCurrentEnergyObject;
    [SerializeField] private GameObject sliderCurrentEnergyObject;
    
    private void Awake()
    {
        textCurrentEnergy = textCurrentEnergyObject.GetComponent<Text>();
        sliderEnergy = sliderCurrentEnergyObject.GetComponent<Slider>();
        sliderEnergy.maxValue = CurrentGeneralHealth;
        textCurrentEnergy.text = CurrentGeneralHealth.ToString();
        CountHealth = CurrentGeneralHealth;
        CountMaxHealth = CurrentGeneralHealth;
        TempCountHealth = CurrentGeneralHealth;
    }

    public static bool CheckSourceEnergy(int countHealthCheck)
    {
        TempCountHealth -= countHealthCheck;
        return TempCountHealth >= 0 ? true : false;
    }

    public static void UdpdateSourceEnergy(int amountHealth)
    {
        CountHealth += amountHealth;
        textCurrentEnergy.text = CountHealth.ToString();
        sliderEnergy.value = CountHealth;
    }
}