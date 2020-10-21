using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxCountPlayer;
    [SerializeField] private GameObject[] prefabPlayer;
    [SerializeField] private Transform transformHeroes;
    [SerializeField] private Transform transfromParentPlayer;
    [SerializeField] private PhaseManager phase;
    [SerializeField] private int maxTimeTurn;
    private Dictionary<int,Player> players = new Dictionary<int, Player>();
    private List<int> idDeathPlayer = new List<int>();
    private bool isMain;
    public static System.Action<Dictionary<int, Player>> onStartTurn;
    public static System.Action<bool> onEndGameWin;
    private bool isFirstTurn;
    private bool isCanStartDelay;
    private bool isTimeLost;
    private float deltaTime;
    private void Start()
    {
        InitParameters();
        InitHeroes();
    }

    private void OnDestroy()
    {
        Player.onReadyHero = null;
        Player.onDeathHero = null;
    }

    private void InitParameters()
    {
        Player.onReadyHero += CheckEndTurn;
        Player.onDeathHero += DeathHero;
        isMain = true;
        isFirstTurn = true;
    }


    private void InitHeroes()
    {
        for (int i = 0; i < maxCountPlayer; i++)
        {
            GameObject hero = Instantiate(prefabPlayer[i], transformHeroes.position, Quaternion.identity, transfromParentPlayer) as GameObject;
            hero.name = hero.name + (i);

            if (hero.GetComponent<SimplePlayer>() != null)
            {
                players.Add(i,hero.GetComponent<SimplePlayer>());
            }
            players[i].InitSettingsPlayer(isMain, i);
            isMain = false;
        }
        StartTurn();
    }

    private void StartTurn()
    {
        if(!players.ContainsKey(0))
        {
            onEndGameWin?.Invoke(false);
        }
        else if (players.Count <= 1)
        {
            onEndGameWin?.Invoke(true);
        }
        else
        {
            foreach (var player in players)
            {
                player.Value.CheckCoolDown();
            }
            onStartTurn?.Invoke(players);
            isCanStartDelay = true;
            isFirstTurn = false;
        }
    }

    private void Update()
    {
        StartDelay();
    }

    private void StartDelay()
    {
        if (isCanStartDelay)
        {
            deltaTime += Time.deltaTime;
            if(deltaTime < maxTimeTurn)
            {
                phase.StartChooseAbilityPhase((int)deltaTime, maxTimeTurn, true);
            }
            else
            {
                isTimeLost = true;
                CheckEndTurn();
            }
        }
    }

    private void CheckEndTurn()
    {
        if (Player.CountReadyHero >= players.Count || isTimeLost)
        {
            phase.StartChooseAbilityPhase((int)deltaTime, maxTimeTurn, false);
            isTimeLost = false;
            isCanStartDelay = false;
            deltaTime = 0;
            Player.CountReadyHero = 0;
            StartCoroutine(BuyAbility());
        }
    }

    private IEnumerator BuyAbility()
    {
        yield return new WaitForSeconds(1f);
        phase.StartRandomEventPhase();
        yield return new WaitForSeconds(1f);
        players.Values.ToList().ForEach(x => x.CheckSourceEnergy());
        players.Values.ToList().ForEach(x => x.BuyAbility());
        yield return new WaitForSeconds(0.75f);
        players.Values.ToList().ForEach(x => x.UseAbility());
        yield return new WaitForSeconds(0.75f);
        CheckDeathHero();
        StartTurn();
    }

    private void CheckDeathHero()
    {
        foreach (var id in idDeathPlayer)
        {
            if (players != null)
            {
                Destroy(players[id].gameObject, 0.3f);
                players.Remove(id);
            }
        }
        idDeathPlayer.Clear();
    }

    private void DeathHero(int idTurn)
    {
        if (!idDeathPlayer.Contains(idTurn))
        {
            idDeathPlayer.Add(idTurn);
        }
    }
}
