using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePlayerHealth;

namespace PlayerSimpleAbility
{
    public class Damage : PlayerAbility
    {
        private AbilityDataSet damageDataSet;
        private AbilityDataSet damageDataSetEnemy;
        private int idHeroAttack;
        private int idHeroInAttack;

        public Damage(AbilityDataSet damageDataSet, AbilityDataSet damageDataSetEnemy, int idHeroAttack, int idHeroInAttack)
        {
            this.damageDataSet = damageDataSet;
            this.damageDataSetEnemy = damageDataSetEnemy;
            this.idHeroAttack = idHeroAttack;
            this.idHeroInAttack = idHeroInAttack;
        }
        public Damage(AbilityDataSet damageDataSet, int idHeroAttack = -1, bool init = false)
        {
            this.damageDataSet = damageDataSet;
            this.idHeroAttack = idHeroAttack;
            if (init)
            {
                InitAbility();
            }
        }

        private void InitAbility()
        {
            if (damageDataSet.InfoAbilityCost != null)
            {
                damageDataSet.InfoAbilityCost.text = damageDataSet.CostAbility.ToString();
            }
            if (damageDataSet.InfoAbilityCostUp != null)
            {
                damageDataSet.InfoAbilityCostUp.text = damageDataSet.CostAbility.ToString();
            }
            if (damageDataSet.InfoAbilityUse != null)
            {
                damageDataSet.InfoAbilityUse.text = damageDataSet.CountUse.ToString();
            }
        }

        public override Dictionary<string, int> BuyAbility()
        {
            setAbility.Clear();
            setAbility.Add(keyHealth, -damageDataSet.CostAbility);
            return setAbility;
        }

        public override Dictionary<string, int> UseAbility()
        {
            damageDataSet.effects[0].SetActive(false);
            damageDataSet.effects[1].SetActive(false);
            damageDataSetEnemy.effects[2].SetActive(false);

            if (idHeroAttack == 0 && idHeroInAttack == 1)
            {
                damageDataSet.effects[0].SetActive(true);
            }
            else if (idHeroAttack == 0 && idHeroInAttack == 2)
            {
                damageDataSet.effects[1].SetActive(true);
            }

            if (idHeroAttack == 1 && idHeroInAttack == 2)
            {
                damageDataSet.effects[0].SetActive(true);
            }
            else if (idHeroAttack == 1 && idHeroInAttack == 0)
            {
                damageDataSet.effects[1].SetActive(true);
            }

            if (idHeroAttack == 2 && idHeroInAttack == 1)
            {
                damageDataSet.effects[0].SetActive(true);
            }
            else if (idHeroAttack == 2 && idHeroInAttack == 0)
            {
                damageDataSet.effects[1].SetActive(true);
            }
            damageDataSetEnemy.effects[2].SetActive(true);
            setAbility.Clear();
            setAbility.Add(keyProtect, -damageDataSet.CountUse);
            return setAbility;
        }

        public override Dictionary<string, int> UpGradeAbility()
        {
            setAbility.Clear();
            setAbility.Add(keyHealth, -damageDataSet.CostAbility);
            damageDataSet.CountUse += damageDataSet.CountUpGrade;
            damageDataSet.CurrentLevel += 1;
            if (damageDataSet.InfoAbilityUse != null)
            {
                damageDataSet.InfoAbilityUse.text = damageDataSet.CountUse.ToString();
            }
            if (damageDataSet.LevelLimit != null)
            {
                damageDataSet.LevelLimit.value = damageDataSet.CurrentLevel;
            }
            return setAbility;
        }

        public override Dictionary<string, int> TotalUseAbility()
        {
            throw new System.NotImplementedException();
        }
    }
}
