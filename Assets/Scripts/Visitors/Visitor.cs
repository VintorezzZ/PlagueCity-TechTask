using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Visitor
{
    [SerializeField] private string _name;
    [SerializeField] private string _surname;
    [SerializeField] private string _message;
    [SerializeField] private Sprite _avatar;

    public string Name => _name;
    public string Surname => _surname;
    public string Message => _message;
    public Sprite Avatar => _avatar;

    public Visitor(string name, string surname, string message, Sprite avatar)
    {
        _name = name;
        _surname = surname;
        _message = message;
        _avatar = avatar;
    }
}
