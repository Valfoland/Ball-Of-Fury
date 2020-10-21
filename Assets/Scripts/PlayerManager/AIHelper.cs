using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class AIHelper : MonoBehaviour
{
    private Dictionary<int, Player> simplePlayers = new Dictionary<int, Player>();

    private void OnEnable()
    {
        GameManager.onStartTurn += InitParameters;
    }
    private void OnDisable()
    {
        GameManager.onStartTurn -= InitParameters;
    }

    private void InitParameters(Dictionary<int, Player> simplePlayers)
    {
        StartCoroutine(StartDelay(simplePlayers));
    }

    private IEnumerator StartDelay(Dictionary<int, Player> simplePlayers)
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        this.simplePlayers = simplePlayers;
        SetRandomAbility();
    }

    private void SetRandomAbility()
    {
        try
        {
            foreach (var player in simplePlayers)
            {
                if (!player.Value.IsMain)
                {
                    int randomEnemy = 0;
                    while (true)
                    {
                        randomEnemy = Random.Range(0, simplePlayers.Count + 1);
                        if (simplePlayers.ContainsKey(randomEnemy))
                        {
                            if (player.Value != simplePlayers[randomEnemy])
                            {
                                break;
                            }
                        }
                    }
                    player.Value.ChooseAbility(simplePlayers[randomEnemy]);
                }
            }
        }
        catch (System.InvalidOperationException)
        {

        }
    }
}
