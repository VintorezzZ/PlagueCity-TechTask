using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[System.Serializable]
public class PlayerInfo
{
    private Resolution _screenResolution;
    private int _language = 0;
    private bool _music = true;
    private bool _soundEffects = true;
    private List<Visitor> _visitors = new List<Visitor>();
    private DateTime _dateTime;
    
    public Resolution currentScreenResolution;
    public int currentLanguage;
    public bool currentMusicState;
    public bool currentSoundEffectsState;
    public List<Visitor> currentVisitors = new List<Visitor>();
    public DateTime currentDatetime;
    
    public int Language => _language;
    public bool Music => _music;
    public bool SoundEffects => _soundEffects;
    public Resolution ScreenResolution => _screenResolution;
    public List<Visitor> Visitors => _visitors;
    public DateTime DateTime => _dateTime;
    
    private void SaveNewSettings(bool isChanged)
    {
        if (isChanged)
        {
            _language = currentLanguage;
            _screenResolution.width = currentScreenResolution.width;
            _screenResolution.height = currentScreenResolution.height;
            _music = currentMusicState;
            _soundEffects = currentSoundEffectsState;
        }
        else
        {
            currentLanguage = _language;
            currentScreenResolution.width = _screenResolution.width;
            currentScreenResolution.height = _screenResolution.height;
            currentMusicState = _music;
            currentSoundEffectsState = _soundEffects;
        }
        
        DataManager.Save();
    }

    public PlayerInfo(ObjectToSave savedData)
    {
        _language = savedData.language;
        _music = savedData.music;
        _soundEffects = savedData.soundEffects;
        _screenResolution.width = savedData.screenResolutionWidth;
        _screenResolution.height = savedData.screenResolutionHeight;
        _visitors.AddRange(savedData.visitors);
        DateTime.TryParse(savedData.dateTime, out _dateTime);
        
        EventHub.settingsChanged += SaveNewSettings;
        
        if (_screenResolution.width == 0 || _screenResolution.height == 0)
        {
            _screenResolution.width = 1920;
            _screenResolution.height = 1080;
        }
    }
    public void ClearLastVisitors()
    {
        _visitors.Clear();
    }
}

[Serializable]
public class ObjectToSave
{
    public int screenResolutionWidth;
    public int screenResolutionHeight;
    public int language = 0;
    public bool music = true;
    public bool soundEffects = true;
    public List<Visitor> visitors = new List<Visitor>();
    public string dateTime;
}