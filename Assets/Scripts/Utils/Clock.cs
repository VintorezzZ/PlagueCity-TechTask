using System;
using UnityEngine;
using TMPro;
using Utils;

public class Clock : SingletonBehaviour<Clock>
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text dayNumber;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private double startHour;
    [SerializeField] private double timeMultiplier = 500; // 3 min realtime = 24hr in game
    private DateTime _currentTime;
    private bool _isPaused = false;
    private int _currentDay = 1;
    public DateTime CurrentTime => _currentTime;
    public bool IsPaused => _isPaused;

    private void Awake()
    {
        InitializeSingleton();
        EventHub.daySkip += OnDaySkip;
    }

    public void Init()
    {
        if (GameManager.Instance.playerInfo.DateTime.Day != 1)
            LoadDate();
        else
            StartNewDate();
    }

    private void LoadDate()
    {
        _currentTime = GameManager.Instance.playerInfo.DateTime;
        _currentDay = _currentTime.Day;
    }

    private void StartNewDate()
    {
        _currentTime = new DateTime() + TimeSpan.FromHours(startHour);
    }
    private void Update()
    {
        if(_isPaused)
            return;
        
        UpdateTimeOfDay();
    }
    private void UpdateTimeOfDay()
    {
        _currentTime = _currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = _currentTime.ToString("HH:mm");
            dayNumber.text = _currentTime.Day.ToString();
        }
        
        if(_currentTime.Day != _currentDay)
        {
            _currentDay = _currentTime.Day;
            EventHub.OnNextDayBegin();
        }
    }

    private void OnDaySkip()
    {
        _currentTime = _currentTime.AddDays(1) - _currentTime.TimeOfDay;
        _isPaused = false;
    }
    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }

    private void OnDestroy()
    {
        EventHub.daySkip -= OnDaySkip;
    }
}