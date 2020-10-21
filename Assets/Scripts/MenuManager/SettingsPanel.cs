using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


[System.Serializable]
public class DataSetSettingsPanel
{
    public AudioMixer MasterAudioMixer;
    public Image Sound;
    public Image Music;
    public Button SoundBtn;
    public Button MusicBtn;
    public GameObject SettingsPanelObject;
    public Button BtnBackToMenu;
    public Button BtnGoToSettings;
}

public class SettingsPanel : Panel
{
    private const int MUTE_AUDIO = -80;
    private const int PLAY_AUDIO = 0;

    private DataSetSettingsPanel settingsPanel;

    public SettingsPanel(DataSetSettingsPanel settingsPanel) : base(settingsPanel.SettingsPanelObject)
    {
        this.settingsPanel = settingsPanel;
        GetVolume();
        settingsPanel.SoundBtn.onClick.AddListener(SetVolumeSound);
        settingsPanel.MusicBtn.onClick.AddListener(SetVolumeMusic);
        settingsPanel.BtnBackToMenu.onClick.AddListener(HidePanel);
        settingsPanel.BtnGoToSettings.onClick.AddListener(ShowPanel);
    }

    protected override void ShowPanel()
    {
        panelObject.SetActive(true);
        onChangeBackGround?.Invoke(true);
    }

    protected override void HidePanel()
    {
        base.HidePanel();
    }

    private void GetVolume()
    {
        if (MUTE_AUDIO == PlayerPrefs.GetFloat("VSound"))
        {
            settingsPanel.MasterAudioMixer.SetFloat("Sound", MUTE_AUDIO);
            settingsPanel.Sound.color = Color.gray;
        }
        else
        {
            settingsPanel.MasterAudioMixer.SetFloat("Sound", PLAY_AUDIO);
            settingsPanel.Sound.color = Color.white;
        }
        if (MUTE_AUDIO == PlayerPrefs.GetFloat("VMusic"))
        {
            settingsPanel.MasterAudioMixer.SetFloat("Music", MUTE_AUDIO);
            settingsPanel.Music.color = Color.gray;
        }
        else
        {
            settingsPanel.MasterAudioMixer.SetFloat("Music", PLAY_AUDIO);
            settingsPanel.Music.color = Color.white;
        }
    }

    private void SetVolumeSound()
    {
        if(MUTE_AUDIO == PlayerPrefs.GetFloat("VSound"))
        {
            settingsPanel.MasterAudioMixer.SetFloat("Sound", PLAY_AUDIO);
            PlayerPrefs.SetFloat("VSound", PLAY_AUDIO);
            settingsPanel.Sound.color = Color.white;
        }
        else
        {
            settingsPanel.MasterAudioMixer.SetFloat("Sound", MUTE_AUDIO);
            PlayerPrefs.SetFloat("VSound", MUTE_AUDIO);
            settingsPanel.Sound.color = Color.gray;
        }
    }

    private void SetVolumeMusic()
    {
        if (MUTE_AUDIO == PlayerPrefs.GetFloat("VMusic"))
        {
            settingsPanel.MasterAudioMixer.SetFloat("Music", PLAY_AUDIO);
            PlayerPrefs.SetFloat("VMusic", PLAY_AUDIO);
            settingsPanel.Music.color = Color.white;
        }
        else
        {
            settingsPanel.MasterAudioMixer.SetFloat("Music", MUTE_AUDIO);
            PlayerPrefs.SetFloat("VMusic", MUTE_AUDIO);
            settingsPanel.Music.color = Color.gray;
        }
    }
}

