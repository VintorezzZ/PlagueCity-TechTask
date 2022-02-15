using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using MyGame.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SettingsView : View
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Toggle soundEffectsToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] _resolutions = new Resolution[] { };
    private LocalizableElement[] _localizableElements = new LocalizableElement[] { };

    private PlayerInfo _playerInfo => GameManager.Instance.playerInfo;
    public override void Initialize()
    {
        base.Initialize();

        FillResolutionDropdown();
        FindAllLocalizableElements();
        
        ApplySavedSettings();

        backButton.onClick.AddListener(() => SaveSettings(false));
        saveButton.onClick.AddListener(() => SaveSettings(true));
        soundEffectsToggle.onValueChanged.AddListener(AudioSettings.SetUiVolume);
        musicToggle.onValueChanged.AddListener(AudioSettings.SetMusicVolume);
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    private void SaveSettings(bool needSave)
    {
        if (!needSave)
            ApplySavedSettings();
        else
            EventHub.OnSettingsChanged(true);
        
        ViewManager.ShowLast();
    }

    private void FindAllLocalizableElements()
    {
        _localizableElements = FindObjectsOfType<LocalizableElement>();
    }

    private void ChangeLanguage(int value)
    {
        foreach (var localizableElement in _localizableElements)
        {
            localizableElement.Localize(value);
        }
        
        _playerInfo.currentLanguage = languageDropdown.value = value;
    }

    private void ChangeResolution(int value)
    {
        string res = resolutionDropdown.options[value].text;
        string[] array = res.Split('x');
        int width = Convert.ToInt32(array[0]);
        int height = Convert.ToInt32(array[1]);
        Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);

        _playerInfo.currentScreenResolution.width = width;
        _playerInfo.currentScreenResolution.height = height;
    }

    private void ApplySavedSettings()
    {
        ApplyScreenResolution();
        ApplyAudioSettings();
        ChangeLanguage(_playerInfo.Language);
    }

    private void ApplyAudioSettings()
    {
        AudioSettings.musicVolume = _playerInfo.Music;
        AudioSettings.uiVolume = _playerInfo.SoundEffects;

        musicToggle.isOn = _playerInfo.currentMusicState = Convert.ToBoolean(_playerInfo.Music);
        soundEffectsToggle.isOn = _playerInfo.currentSoundEffectsState = Convert.ToBoolean(_playerInfo.SoundEffects);
    }

    private void ApplyScreenResolution()
    {
        var gs = _playerInfo.ScreenResolution;
        resolutionDropdown.value = _resolutions.ToList().FindIndex(res => res.width == gs.width && res.height == gs.height);
        resolutionDropdown.RefreshShownValue();
        Screen.SetResolution(gs.width, gs.height, FullScreenMode.FullScreenWindow);
    }

    private void FillResolutionDropdown()
    {
        _resolutions = GetResolutionsWithoutRefreshRate();
        resolutionDropdown.options.Clear();

        foreach (var res in _resolutions)
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + " x " + res.height));
    }

    private Resolution[] GetResolutionsWithoutRefreshRate()
    {
        var resDict = new Dictionary<string, Resolution>();

        foreach (var item in Screen.resolutions)
            if (!resDict.ContainsKey(item.width + "x" + item.height))
                resDict.Add(item.width + "x" + item.height, item);

        return resDict.Values.ToArray();
    }

    public override void Show()
    {
        base.Show();

        soundEffectsToggle.isOn = AudioSettings.uiVolume;
        musicToggle.isOn = AudioSettings.musicVolume;
    }
}
