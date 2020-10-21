using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimplePlayerHealth;
using PlayerSimpleAbility;

public abstract class Player : MonoBehaviour
{
    public static System.Action onReadyHero;
    public static System.Action<int> onDeathHero;
    public static int CountReadyHero;

    protected PlayerAbility playerAbility;
    protected PlayerAbility playerUpAbility;
    protected PlayerAbility playerEnergyAbility;
    protected PlayerHealth playerHealth;
    [SerializeField] protected PlayerHealthDataSet playerHealthDataSet;

    public abstract void InitSettingsPlayer(bool isMain, int idTurn);
    public abstract void CheckSourceEnergy();
    public abstract void BuyAbility();
    public abstract void UseAbility();

    public abstract void CheckCoolDown();
    public abstract void ChooseAbility(object player);
    public bool IsMain;
    protected int idTurn;
}
