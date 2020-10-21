using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BankAnim : MonoBehaviour
{
    [SerializeField] private Slider sliderBank;
    [SerializeField] private Text textBank;

    private void OnEnable()
    {
        WinPanel.onStartAnimBank += BankAnimation;
        sliderBank.maxValue = SourceEnergyManager.CountMaxHealth;
        sliderBank.value = 0;
    }

    private void OnDisable()
    {
        WinPanel.onStartAnimBank -= BankAnimation;
    }

    private void BankAnimation()
    {
        StartCoroutine(StartBankAnimation());
    }

    IEnumerator StartBankAnimation()
    {
        float countTime = 0;
        while (sliderBank.value < SourceEnergyManager.CountHealth)
        {
            countTime += Time.deltaTime;
            sliderBank.value = Mathf.Lerp(0, SourceEnergyManager.CountHealth, countTime);
            int valueEnergy = (int) sliderBank.value;
            textBank.text = valueEnergy.ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
