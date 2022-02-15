using System;
using System.Collections.Generic;
using System.Linq;
using MyGame.Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class MainMenuView : View
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button settingsButton;
    
    [SerializeField] private List<VisitorCard> visitorCards;
    private const int MAX_VISITORS_PER_DAY = 5;

    [SerializeField] private Image pauseButtonImage;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite resumeSprite;

    public readonly List<Visitor> visitorsQueue = new List<Visitor>();

    public override void Initialize()
    {
        base.Initialize();
        
        pauseButton.onClick.AddListener(() =>
        {
            if(Clock.Instance.IsPaused)
            {
                pauseButtonImage.sprite = pauseSprite;
                Clock.Instance.Resume();
                GameManager.Instance.ResumeGame();
            }
            else
            {
                pauseButtonImage.sprite = resumeSprite;
                Clock.Instance.Pause();
                GameManager.Instance.PauseGame();
            }
        });
        
        nextDayButton.onClick.AddListener(() =>
        {
            EventHub.OnDaySkip();
        });
        
        settingsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayClick();
            ViewManager.Show<SettingsView>();
        });

        EventHub.visitorViewClose += RemoveVisitorFromQueue;
        EventHub.nextDayBegin += OnNextDayBegin;
        EventHub.daySkip += OnNextDayBegin;
        LoadVisitors();
        Clock.Instance.Init();
    }

    private void OnNextDayBegin()
    {
        visitorsQueue.Clear();
        LoadVisitors();
        ShowVisitorCards();
    }

    private void OnDestroy()
    {
        EventHub.visitorViewClose -= RemoveVisitorFromQueue;
    }

    private void RemoveVisitorFromQueue(VisitorCard visitorCard)
    {
        visitorsQueue.Remove(visitorCard.Visitor);
    }

    public override void Show()
    {
        base.Show();
        
        ShowVisitorCards();
    }

    private void LoadVisitors()
    {
        if (GameManager.Instance.playerInfo.Visitors.Count > 0)
        {
            visitorsQueue.AddRange(GameManager.Instance.playerInfo.Visitors);
            GameManager.Instance.playerInfo.ClearLastVisitors();
        }        
        else
        {
            GenerateNewVisitors();
        }
    }

    private void GenerateNewVisitors()
    {
        for (int i = 0; i < MAX_VISITORS_PER_DAY; i++)
        {
            Visitor visitor = VisitorsGenerator.GenerateVisitor();
            visitorsQueue.Add(visitor);
        }
    }

    private void ShowVisitorCards()
    {
        for (int i = 0; i < visitorsQueue.Count; i++)
        {
            if(visitorsQueue[i] != null)
            {
                visitorCards[i].Init(visitorsQueue[i]);
                visitorCards[i].gameObject.SetActive(true);
            }        
        }
    }

    private void OnApplicationQuit()
    {
        GameManager.Instance.playerInfo.currentVisitors.AddRange(visitorsQueue);
        GameManager.Instance.playerInfo.currentDatetime = Clock.Instance.CurrentTime;
        DataManager.Save();
    }
}
