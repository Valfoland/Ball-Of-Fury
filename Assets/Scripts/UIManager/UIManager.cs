using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DataSetEndGamePanel dataEndGamePanel;
    [SerializeField] private DataSetSettingsPanel dataSettings;

    public static System.Action<bool> onEndGame;
    public static System.Action<bool> onChangePage;

    private static UIManager Instance;

    private void OnEnable()
    {
        GameManager.onEndGameWin += ShowEndGamePanel;
    }

    private void OnDisable()
    {
        GameManager.onEndGameWin -= ShowEndGamePanel;
    }

    private void Start()
    {
        new SettingsPanel(dataSettings);
    }

    public void ShowEndGamePanel(bool isWin)
    {
        Panel endGamePanel = new EndGamePanel(dataEndGamePanel);
        if (isWin)
        {
            new WinPanel(endGamePanel, dataEndGamePanel);
        }
        else
        {
            new LosePanel(endGamePanel, dataEndGamePanel);
        }
    }

    public void PlayGame()
    {
         SceneManager.LoadScene("Game");
    }

    public void ShowPausePanel()
    {

    }

}

