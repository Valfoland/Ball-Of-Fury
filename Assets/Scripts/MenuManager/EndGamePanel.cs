using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetEndGamePanel
{
    public GameObject AudioGame;
    public GameObject EndGamePanelObject;
    public GameObject TextCountEnergy;
    public GameObject[] Banks;
    public Text TextEndGame;
    public Button BtnBackToMenu;
}

public class EndGamePanel : Panel
{
    private DataSetEndGamePanel dataSetEndGamePanel;
    public EndGamePanel(DataSetEndGamePanel dataSetEndGamePanel) : base(dataSetEndGamePanel.EndGamePanelObject)
    {
        //dataSetEndGamePanel.AudioGame.SetActive(false);
        ShowPanel();
    }

    protected override void ShowPanel()
    {
        panelObject.SetActive(true);
        onChangeBackGround?.Invoke(true);        
    }
}

