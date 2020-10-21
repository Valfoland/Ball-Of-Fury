using UnityEngine;
using SimplePlayerHealth;
using PlayerSimpleAbility;


public class SimplePlayer : Player
{
    private SimplePlayer heroesInAttacked;
    private static SimplePlayer mainPlayer;

    [SerializeField] private AbilityDataSet[] abilityDataSet;
    [SerializeField] private SimplePlayerPanelDataSet simplePlayerPanelDataSet;
    private static bool canTakeEnergy;
    private PanelMainSimplePlayer panel;
    private int[] coolDown;
    private bool[] isCanMove;
    private bool[] isStartCoolDown;

    public override void InitSettingsPlayer(bool isMain, int idTurn)
    {
        isCanMove = new bool[simplePlayerPanelDataSet.ButtonsAbilities.Count];
        isStartCoolDown = new bool[simplePlayerPanelDataSet.ButtonsAbilities.Count];
        coolDown = new int[simplePlayerPanelDataSet.ButtonsAbilities.Count];

        for (int i = 0; i < simplePlayerPanelDataSet.ButtonsAbilities.Count; i++)
        {
            isCanMove[i] = false;
            isStartCoolDown[i] = false;
        }

        IsMain = isMain;
        this.idTurn = idTurn;

        playerHealth = new PlayerHealth(playerHealthDataSet);
        playerHealth.onChangeHealth += SetStatusHero;

        if (IsMain)
        {
            mainPlayer = this;
            panel = new PanelMainSimplePlayer(simplePlayerPanelDataSet);
            panel.onClickAbilityButton += SetAbility;
        }
        InitAbilities();
    }

    private void InitAbilities()
    {
        new Damage(abilityDataSet[0], -1, true);
        new Shield(abilityDataSet[1], true);
        new BurstEnergy(abilityDataSet[2], true);
    }

    #region AIPlayer
    public override void ChooseAbility(object enemyPlayer)
    {
        if (!IsMain)
        {
            int randAbility = Random.Range(0, simplePlayerPanelDataSet.NameButtons.Count);
            ChooseEnemyHero(enemyPlayer as SimplePlayer);
            SetAbility(simplePlayerPanelDataSet.NameButtons[randAbility]);
        }
    }

    private void ChooseEnemyHero(SimplePlayer player)
    {
        if (!IsMain)
        {
            heroesInAttacked = player;
        }
    }
    #endregion

    public override void CheckCoolDown()
    {
        ResetPlayer();
        for (int i = 0; i < coolDown.Length; i++)
        {
            isCanMove[i] = false;
            if (isStartCoolDown[i])
            {
                if (coolDown[i] >= abilityDataSet[i].CoolDown)
                {
                    isCanMove[i] = true;
                    isStartCoolDown[i] = false;
                }
                coolDown[i]++;
            }
            else
            {
                isCanMove[i] = true;
            }
            coolDown[i]++;
        }
        SetCanMove();
    }

    private void SetCanMove(bool isEndMove = false)
    {
        if (IsMain)
        {
            if (!isEndMove)
            {
                panel.IsEnablePanel(isCanMove);
            }
            else
            {
                panel.IsEnablePanel();
            }
        }
    }

    public void OnClickEnemyHero()
    {
        mainPlayer.heroesInAttacked = this;
    }

    private void SetAbility(string nameAbility)
    {
        PlayerAbility playerAbility = null;
        try
        {
            if (!nameAbility.Contains("Burst"))
            {
                if (nameAbility == simplePlayerPanelDataSet.NameButtons[0] && !isStartCoolDown[0])
                {
                    playerAbility = new Damage(abilityDataSet[0], heroesInAttacked.abilityDataSet[0], idTurn, heroesInAttacked.idTurn);
                    isStartCoolDown[0] = true;
                    coolDown[0] = 0;
                    if(idTurn == heroesInAttacked.idTurn)
                    {
                        playerAbility = null;
                        heroesInAttacked = null;
                        isStartCoolDown[0] = false;
                    }
                }
                else if (nameAbility == simplePlayerPanelDataSet.NameButtons[1] && !isStartCoolDown[1])
                {
                    playerAbility = new Shield(abilityDataSet[1]);
                    heroesInAttacked = null;
                    isStartCoolDown[1] = true;
                    coolDown[1] = 0;
                }
                this.playerAbility = playerAbility;
            }
            else
            {
                if (nameAbility == simplePlayerPanelDataSet.NameButtons[2] && !isStartCoolDown[2])
                {
                    if (abilityDataSet[2].CurrentLevel < abilityDataSet[2].CountLevelLimit)
                    {
                        playerAbility = new BurstEnergy(abilityDataSet[2]);
                        isStartCoolDown[2] = true;
                        if (IsMain && abilityDataSet[2].CurrentLevel >= abilityDataSet[2].CountLevelLimit - 1)
                        {
                            simplePlayerPanelDataSet.ButtonsAbilities[2].SetActive(false);
                        }
                    }
                    coolDown[2] = 0;
                }
                else if (nameAbility == simplePlayerPanelDataSet.NameButtons[3])
                {
                    if (abilityDataSet[0].CurrentLevel < abilityDataSet[0].CountLevelLimit)
                    {
                        playerAbility = new Damage(abilityDataSet[0], idTurn);
                        if (IsMain && abilityDataSet[0].CurrentLevel >= abilityDataSet[0].CountLevelLimit - 1)
                        {
                            simplePlayerPanelDataSet.ButtonsAbilities[3].SetActive(false);
                        }
                    }
                }
                else if (nameAbility == simplePlayerPanelDataSet.NameButtons[4])
                {
                    if(abilityDataSet[1].CurrentLevel < abilityDataSet[1].CountLevelLimit)
                    {
                        playerAbility = new Shield(abilityDataSet[1]);
                        if (IsMain && abilityDataSet[1].CurrentLevel >= abilityDataSet[1].CountLevelLimit - 1)
                        {
                            simplePlayerPanelDataSet.ButtonsAbilities[4].SetActive(false);
                        }
                    }
                }
                heroesInAttacked = null;
                this.playerUpAbility = playerAbility;
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("Выбери цель с начала, а потом жми (Речь про дамажущую кнопочку)");
        }
        if (playerAbility != null)
        {
            SetCanMove(true);
            CountReadyHero++;
            onReadyHero?.Invoke();
        }
    }

    public override void CheckSourceEnergy()
    {
        canTakeEnergy = playerHealth.CheckGeneralHealth(abilityDataSet[2].CountUse);
    }

    public override void BuyAbility()
    {
        playerEnergyAbility = new BurstEnergy(abilityDataSet[2]);
        if (canTakeEnergy)
        {
            playerHealth.UpdateHealth(playerEnergyAbility.TotalUseAbility()); 
        }
        Invoke("DelayBuyAbility", 0.5f);
    }

    private void DelayBuyAbility()
    {
        if (playerAbility != null)
        {
            if (playerHealth.CheckPlayerHealth(abilityDataSet[0].CostAbility))
            {
                playerHealth.UpdateHealth(playerAbility.BuyAbility());
            }
        }
        else if (playerUpAbility != null)
        {
            if (playerHealth.CheckPlayerHealth(abilityDataSet[0].CostAbility))
            {
                playerHealth.UpdateHealth(playerUpAbility.UpGradeAbility());
            }
        }
    }

    public override void UseAbility()
    {
        if (playerAbility != null)
        {
            if (playerHealth.CheckPlayerHealth(abilityDataSet[0].CostAbility))
            {
                if (heroesInAttacked != null)
                {
                    heroesInAttacked.playerHealth.UpdateHealth(playerAbility.UseAbility());    
                }
                else
                {
                    playerHealth.UpdateHealth(playerAbility.UseAbility());
                }
            }
        }
    }

    private void SetStatusHero(LiveStates status)
    {
        if (status == LiveStates.IsDead)
        {
            onDeathHero?.Invoke(idTurn);
        }
        else if(status == LiveStates.IsLive)
        {

        }
    }

    private void ResetPlayer()
    {
        playerUpAbility = null;
        playerAbility = null;
        playerEnergyAbility = null;
        heroesInAttacked = null; 
    }

    private void OnDestroy()
    {
        if (IsMain)
        {
            panel.onClickAbilityButton -= SetAbility;
        }
    }
}
