using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class VisitorView : View
{
    private VisitorCard _visitorCard;
    
    [SerializeField] private Button background;
    [SerializeField] private Button closeButton;

    [SerializeField] private Image avatar;
    [SerializeField] private TMP_Text visitorName;
    [SerializeField] private TMP_Text message;

    public override void Initialize()
    {
        base.Initialize();

        EventHub.visitorViewShow += LoadVisitorInfo;
        
        background.onClick.AddListener(() => base.FadeIn(.2f));
        closeButton.onClick.AddListener(() => FadeIn(.2f));
    }

    public override void FadeIn(float duration = 1)
    {
        _visitorCard.gameObject.SetActive(false);
        EventHub.OnVisitorViewClose(_visitorCard);
        base.FadeIn(duration);
    }

    public override void Show()
    {
        FadeOut(.2f);
    }

    private void LoadVisitorInfo(VisitorCard visitorCard)
    {
        _visitorCard = visitorCard;
        avatar.sprite = visitorCard.Visitor.Avatar;
        visitorName.text = visitorCard.Visitor.Name + " " + visitorCard.Visitor.Surname;
        message.text = visitorCard.Visitor.Message;
    }

    private void OnDestroy()
    {
        EventHub.visitorViewShow -= LoadVisitorInfo;
    }
}
