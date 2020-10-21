using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Panel
{
    public static System.Action<bool> onChangeBackGround;
    protected GameObject panelObject;
    protected abstract void ShowPanel();
    protected virtual void HidePanel()
    {
        panelObject.SetActive(false);
    }

    public Panel(GameObject panelObject = null)
    {
        this.panelObject = panelObject;
    }
}
