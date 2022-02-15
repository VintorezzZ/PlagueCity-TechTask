using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public static class AudioSettings
{
    public static bool uiVolume;
    public static bool musicVolume;
    
    public static void SetUiVolume(bool vol)
    {
        uiVolume = vol;
        GameManager.Instance.playerInfo.currentSoundEffectsState = vol;
        EventHub.OnAudioSettingsChanged();
    }
    
    public static void SetMusicVolume(bool vol)
    {
        musicVolume = vol;
        GameManager.Instance.playerInfo.currentMusicState = vol;
        EventHub.OnAudioSettingsChanged();
    }
}
