using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PlayerSimpleAbility
{
    public abstract class PlayerAbility
    {
        protected Dictionary<string, int> setAbility = new Dictionary<string, int>();
        protected const string keyHealth = "Health";
        protected const string keyProtect = "Protect";
        public abstract Dictionary<string, int> BuyAbility();
        public abstract Dictionary<string, int> UseAbility();
        public abstract Dictionary<string, int> TotalUseAbility();
        public abstract Dictionary<string, int> UpGradeAbility();
    }

    [System.Serializable]
    public class AbilityDataSet
    {
        [Header("Настройки способностей")]
        [Tooltip("Просто название скила, чтоб не забыть потом")] public string NameAbility;
        [Tooltip("КД способности")] public int CoolDown;
        [Tooltip("Цена способности")] public int CostAbility;
        [Tooltip("Степень способности")] public int CountUse;
        [Tooltip("Степень прироста при апгрейде")] public int CountUpGrade;
        [Tooltip("Лимит на апгрейд")] public int CountLevelLimit;
        [System.NonSerialized] public int CurrentLevel;
        
        [Header("Надстройки отображения параметров")]
        [Tooltip("Количество прироста игрока")] public Text InfoAbilityUsePlayer;
        public Text InfoAbilityUse;
        public Text InfoAbilityCost;
        public Text InfoAbilityCostUp;
        public Slider LevelLimit;
        public GameObject[] effects;
    }
}
