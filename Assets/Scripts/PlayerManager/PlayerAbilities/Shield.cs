using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePlayerHealth;

namespace PlayerSimpleAbility
{
    public class Shield : PlayerAbility
    {
        [SerializeField] private AbilityDataSet shieldDataSet; 

        public Shield(AbilityDataSet shieldDataSet, bool init = false)
        {
            this.shieldDataSet = shieldDataSet;

            if(init)
            {
                InitAbility();
            }
        }

        private void InitAbility()
        {
            if (this.shieldDataSet.InfoAbilityCost != null)
            {
                this.shieldDataSet.InfoAbilityCost.text = this.shieldDataSet.CostAbility.ToString();
            }
            if (this.shieldDataSet.InfoAbilityCostUp != null)
            {
                this.shieldDataSet.InfoAbilityCostUp.text = this.shieldDataSet.CostAbility.ToString();
            }
            if (this.shieldDataSet.InfoAbilityUse != null)
            {
                this.shieldDataSet.InfoAbilityUse.text = this.shieldDataSet.CountUse.ToString();
            }
        }

        public override Dictionary<string,int> BuyAbility()
        {
            int koeffShieldFree = 1;
            setAbility.Clear();
            if (RandomEventsManager.RandomEvent == RandomEvents.ShieldFree)
            {
                koeffShieldFree = 0;
            }
            if (RandomEventsManager.RandomEvent != RandomEvents.ShieldDis)
            {
                shieldDataSet.effects[0].SetActive(false);
                shieldDataSet.effects[0].SetActive(true);
                setAbility.Add(keyHealth, -shieldDataSet.CostAbility * koeffShieldFree);
                setAbility.Add(keyProtect, shieldDataSet.CountUse);
            }
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
            setAbility.Add(keyHealth, -shieldDataSet.CostAbility);
            shieldDataSet.CountUse += shieldDataSet.CountUpGrade;
            shieldDataSet.CurrentLevel += 1;
            if (shieldDataSet.InfoAbilityUse != null)
            {
                shieldDataSet.InfoAbilityUse.text = shieldDataSet.CountUse.ToString();
            }
            if(shieldDataSet.LevelLimit != null)
            {
                shieldDataSet.LevelLimit.value = shieldDataSet.CurrentLevel;
            }
            return setAbility;
        }

        public override Dictionary<string, int> TotalUseAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}
