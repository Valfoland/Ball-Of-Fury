using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LosePanel : PanelEndGameDecorator
{
    private Image endGameImage;
    public LosePanel(Panel p, DataSetEndGamePanel endGamePanel) : base(p, endGamePanel)
    {
        endGameImage = dataSetEndGamePanel.EndGamePanelObject.GetComponent<Image>();
        dataSetEndGamePanel.BtnBackToMenu.onClick.AddListener(BackToMenu);
        ShowPanel();
    }

    protected override void ShowPanel()
    {
        dataSetEndGamePanel.TextEndGame.text = "Вы проиграли";
        endGameImage.color = new Color32(255, 122, 98, 255);
        dataSetEndGamePanel.TextCountEnergy.SetActive(false);
        dataSetEndGamePanel.Banks[1].SetActive(true);
    }

    protected override void BackToMenu()
    {
        base.BackToMenu();
    }
}

