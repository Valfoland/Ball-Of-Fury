using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace SimplePlayerHealth
{
    public enum LiveStates
    {
        empty,
        IsLive,
        IsLiveWithP,
        IsDead
    }

    [System.Serializable]
    public class PlayerHealthDataSet
    {
        [Header("Настройки здоровья игрока")]
        [Tooltip("Здоровье на старте")] public int StartingHealth;

        public Slider SliderHealth;
        public Text TextHealth;

        public Transform ParentOnChangeHealth;
        public GameObject TextOnChangeHealthPrefab;
        public GameObject EffectDeath;

        public const string keyHealth = "Health";
        public const string keyProtect = "Protect";
    }

    public class PlayerHealth : ScriptableObject
    {
        private Text textOnChangeHealth;
        private int tempCurrentHealth;
        public int CurrentHealth;
        public int CurrentProtect;

        public System.Action<LiveStates> onChangeHealth;
        private PlayerHealthDataSet playerHealthDataSet;

        public PlayerHealth(PlayerHealthDataSet playerHealthDataSet)
        {
            this.playerHealthDataSet = playerHealthDataSet;
            CurrentHealth = playerHealthDataSet.StartingHealth;
            InitParameters();
        }

        private void InitParameters()
        {
            tempCurrentHealth = CurrentHealth;
            playerHealthDataSet.SliderHealth.maxValue = playerHealthDataSet.StartingHealth;
        }

        public bool CheckGeneralHealth(int countHealthCheck)
        {
            return SourceEnergyManager.CheckSourceEnergy(countHealthCheck);
        }

        public bool CheckPlayerHealth(int countHealthCheck)
        {
            int countCheck = CurrentHealth;
            countCheck -= countHealthCheck;
            return countCheck >= 0 ? true : false;
        }

        public void UpdateHealth(Dictionary<string, int> amountHealth)
        {
            SourceEnergyManager.TempCountHealth = SourceEnergyManager.CountHealth;
            playerHealthDataSet.SliderHealth.maxValue = playerHealthDataSet.StartingHealth;
            foreach (var a in amountHealth)
            {
                if (a.Key == PlayerHealthDataSet.keyHealth)
                {
                    CurrentHealth += a.Value;
                }
                else if (a.Key == PlayerHealthDataSet.keyProtect)
                {
                    if (a.Value > 0)
                    {
                        CurrentProtect = a.Value;
                    }
                    else if(a.Value < 0)
                    {
                        CurrentProtect += a.Value;
                        if (CurrentProtect < 0)
                        {
                            CurrentHealth += CurrentProtect;
                        }
                        CurrentProtect = 0;
                    }
                }
            }
            SetHealth();
            SetStatusHealth();
        }

        private void SetHealth()
        {
            if (CurrentHealth > playerHealthDataSet.StartingHealth)
                CurrentHealth = playerHealthDataSet.StartingHealth;

            playerHealthDataSet.SliderHealth.value = CurrentHealth;
            playerHealthDataSet.TextHealth.text = CurrentHealth.ToString();
            OnChangeHealth(CurrentHealth);
        }

        private void SetStatusHealth()
        {
            if (CurrentHealth <= 0)
            {
                playerHealthDataSet.EffectDeath.SetActive(true);
                onChangeHealth?.Invoke(LiveStates.IsDead);
            }
            else if (CurrentHealth > 0)
            {
                onChangeHealth?.Invoke(LiveStates.IsLive);
            }
        }

        private void OnChangeHealth(int CurrentHealth)
        {
            if (tempCurrentHealth != CurrentHealth)
            {
                GameObject prefab = Instantiate(playerHealthDataSet.TextOnChangeHealthPrefab, 
                                                playerHealthDataSet.ParentOnChangeHealth) as GameObject;
                textOnChangeHealth = prefab.GetComponent<Text>();

                if (tempCurrentHealth > CurrentHealth)
                {
                    int health = tempCurrentHealth - CurrentHealth;
                    textOnChangeHealth.text = "-" + health.ToString();
                }
                else
                {
                    int health = CurrentHealth - tempCurrentHealth;
                    SourceEnergyManager.UdpdateSourceEnergy(-1 * health);
                    textOnChangeHealth.text = "+" + health.ToString(); 
                }
                tempCurrentHealth = CurrentHealth;
                Destroy(prefab, 4f);
            }
        }
    }
}
