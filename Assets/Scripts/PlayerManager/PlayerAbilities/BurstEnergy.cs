using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSimpleAbility
{
    public class BurstEnergy : PlayerAbility
    {
        [SerializeField] private AbilityDataSet burstEDataSet;
        private ParticleSystem particleSystem;
        private Color32 colBlue = new Color32(179, 255, 230, 80);
        private Color32 colRed = new Color32(250, 140, 50, 80);
        private const float COEFF_BURST_INC = 1.3f;

        public BurstEnergy(AbilityDataSet burstEDataSet, bool init = false)
        {
            particleSystem = burstEDataSet.effects[0].GetComponent<ParticleSystem>();
            this.burstEDataSet = burstEDataSet;
            if (init)
            {
                InitAbility();
            }
        }

        private void InitAbility()
        {
            if (burstEDataSet.InfoAbilityUse != null)
            {
                burstEDataSet.InfoAbilityUse.text = "+" + burstEDataSet.CountUse.ToString();
            }
            if (burstEDataSet.InfoAbilityUsePlayer != null)
            {
                burstEDataSet.InfoAbilityUsePlayer.text = burstEDataSet.CountUse.ToString();
            }
            if (burstEDataSet.InfoAbilityCost != null)
            {
                burstEDataSet.InfoAbilityCost.text = burstEDataSet.CostAbility.ToString();
            }
        }

        public override Dictionary<string, int> BuyAbility()
        {
            setAbility.Clear();
            setAbility.Add(keyHealth, -burstEDataSet.CostAbility);
            return setAbility;
        }

        public override Dictionary<string, int> UseAbility()
        {
            setAbility.Clear();
            setAbility.Add(keyProtect, 0);
            return setAbility;
        }

        public override Dictionary<string, int> UpGradeAbility()
        {
            setAbility.Clear();
            setAbility.Add(keyHealth, -burstEDataSet.CostAbility);
            burstEDataSet.CountUse += burstEDataSet.CountUpGrade;
            burstEDataSet.CurrentLevel += 1;
            if (burstEDataSet.InfoAbilityUsePlayer != null)
            {
                burstEDataSet.InfoAbilityUsePlayer.text = burstEDataSet.CountUse.ToString();
            }
            if (burstEDataSet.LevelLimit != null)
            {
                burstEDataSet.LevelLimit.value += burstEDataSet.CurrentLevel;
            }
            return setAbility;
        }

        public override Dictionary<string, int> TotalUseAbility()
        {
            float koeff = 1;
            particleSystem.startColor = colRed;
            if (RandomEventsManager.RandomEvent == RandomEvents.IncBurstE)
            {
                koeff = COEFF_BURST_INC;
            }
            else if (RandomEventsManager.RandomEvent == RandomEvents.DecBurstE)
            {
                koeff = 0;
            }
            else if (RandomEventsManager.RandomEvent == RandomEvents.TakeDamage)
            {
                particleSystem.startColor = colBlue;
                koeff = -1;
            }
            if (RandomEventsManager.RandomEvent != RandomEvents.DecBurstE)
            {
                burstEDataSet.effects[0].SetActive(false);
                burstEDataSet.effects[0].SetActive(true);
            }
            setAbility.Clear();
            setAbility.Add(keyHealth, (int)(burstEDataSet.CountUse * koeff));
            return setAbility;
        }
    }
}
