using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    [SerializeField] AudioSource musicSource, uiSource;
    [SerializeField] AudioMixer uiMixer, musicMixer;

    [SerializeField] AudioClip musicSfx;
    [SerializeField] AudioClip clickSfx;
    private Coroutine _fadeCoroutine;

    private void Awake()
    {
        InitializeSingleton();

        EventHub.audioSettingsChanged += OnAudioSettingsChanged;
    }

    public void Init()
    {
        FadeMixerGroup(musicMixer, AudioSettings.musicVolume, 0.01f);
        FadeMixerGroup(uiMixer, AudioSettings.uiVolume, 0.01f);
  
        PlayMusic();
    }
    
    private void OnAudioSettingsChanged()
    {
        FadeMixerGroup(musicMixer, AudioSettings.musicVolume);
        FadeMixerGroup(uiMixer, AudioSettings.uiVolume);
    }

    private void FadeMixerGroup(AudioMixer audioMixer, bool enabled, float duration = .3f , string exposedParam = "Volume")
    {
        _fadeCoroutine = StartCoroutine(MixerGroupFader.StartFade(audioMixer, exposedParam, duration, Convert.ToInt32(enabled)));
    }
    
    public void PlayClick()
    {
        uiSource.PlayOneShot(clickSfx);
    }
    
    public void PlayMusic()
    {
        musicSource.Play();
    }
    
    private void OnDestroy()
    {
        StopAllCoroutines();
        EventHub.audioSettingsChanged -= OnAudioSettingsChanged;
    }
}
