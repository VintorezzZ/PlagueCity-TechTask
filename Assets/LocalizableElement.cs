using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class LocalizableElement : MonoBehaviour, ILocalizable
{
    [SerializeField] private string rusLocalization;
    [SerializeField] private string engLocalization;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void Localize(int value)
    {
        _text.text = value switch
        {
            0 => rusLocalization,
            1 => engLocalization
        };
    }
}
