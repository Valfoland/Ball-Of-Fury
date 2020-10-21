using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using PlayerSimpleAbility;

[System.Serializable]
public class SimplePlayerPanelDataSet
{
    public Animation AnimChooseAbility;
    public GameObject PanelMainPlayerObject;
    public List<GameObject> ButtonsAbilities = new List<GameObject>(5);
    public List<string> NameButtons;
}

public class PanelMainSimplePlayer : Panel
{
    
    public System.Action<string> onClickAbilityButton;
    private List<Button> ButtonsAbilities = new List<Button>(5);
    private List<Image> ImageAbilities = new List<Image>(5);
    private SimplePlayerPanelDataSet panelMainPlayer;
        
    public PanelMainSimplePlayer(SimplePlayerPanelDataSet panelMainPlayer) : base(panelMainPlayer.PanelMainPlayerObject)
    {  
        this.panelMainPlayer = panelMainPlayer;
        for (int i = 0; i < panelMainPlayer.ButtonsAbilities.Count; i++)
        {
            ButtonsAbilities.Add(panelMainPlayer.ButtonsAbilities[i].GetComponent<Button>());
            ImageAbilities.Add(panelMainPlayer.ButtonsAbilities[i].GetComponent<Image>());
        }

        ButtonsAbilities[0]?.onClick.AddListener(() => OnClickButton(panelMainPlayer.NameButtons[0]));
        ButtonsAbilities[1]?.onClick.AddListener(() => OnClickButton(panelMainPlayer.NameButtons[1]));
        ButtonsAbilities[2]?.onClick.AddListener(() => OnClickButton(panelMainPlayer.NameButtons[2]));
        ButtonsAbilities[3]?.onClick.AddListener(() => OnClickButton(panelMainPlayer.NameButtons[3]));
        ButtonsAbilities[4]?.onClick.AddListener(() => OnClickButton(panelMainPlayer.NameButtons[4]));
        ShowPanel();
    }

    protected override void ShowPanel()
    {
        panelObject.SetActive(true);
        PhaseManager.onChooseAbility += LightOfPanel;
    }

    protected override void HidePanel()
    {
        base.HidePanel();
        for (int i = 0; i < panelMainPlayer.ButtonsAbilities.Count; i++)
        {
            ButtonsAbilities[i].onClick.RemoveAllListeners();
        }
    }

    public void IsEnablePanel(bool[] isEnable = null)
    {
        if (isEnable == null)
        {
            isEnable = new bool[panelMainPlayer.ButtonsAbilities.Count];
            for (int i = 0; i < panelMainPlayer.ButtonsAbilities.Count; i++)
            {
                isEnable[i] = false;
            }
        }
        for (int i = 0; i < panelMainPlayer.ButtonsAbilities.Count; i++)
        {
            ButtonsAbilities[i].interactable = isEnable[i];
        }
    }

    public void OnClickButton(string nameAbility)
    {
        onClickAbilityButton?.Invoke(nameAbility);
        panelMainPlayer.AnimChooseAbility?.Stop();
        PhaseManager.onChooseAbility = null;
        for (int i = 0; i < panelMainPlayer.ButtonsAbilities.Count; i++)
        {
            ImageAbilities[i].color = Color.white;
        }
    }

    private void LightOfPanel()
    {
        panelMainPlayer.AnimChooseAbility?.Play();
    }
}

