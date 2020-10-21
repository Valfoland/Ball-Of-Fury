using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class PanelEndGameDecorator : Panel
{
    protected DataSetEndGamePanel dataSetEndGamePanel;
    protected Panel panel;

    public PanelEndGameDecorator(Panel panel, DataSetEndGamePanel dataSetendGamePanel)
    {
        this.panel = panel;
        this.dataSetEndGamePanel = dataSetendGamePanel;
    }

    protected virtual void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

