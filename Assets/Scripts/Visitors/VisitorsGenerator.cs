using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class VisitorsGenerator
{
    private static List<string> namesList = new List<string> {"Оля", "Ваня", "Света", "Наташа", "Семен", "Вячеслав"};
    private static List<string> surnamesList = new List<string> {"Иванов", "Петров", "Сидоров"};
    private static List<string> messagesList = new List<string> {"Пусть всегда будет солнце", "2*2 = 4", "Я мыслю, значит я существую"};
    private static List<Sprite> avatarsList = new List<Sprite>();
    
    public static void LoadVisitorsData()
    {
        Object[] avatars = Resources.LoadAll("Visitors avatars");

        foreach (var avatar in avatars)
        {
            try
            {
                avatarsList.Add((Sprite)avatar);
            }
            catch (Exception e)
            {
                continue;
            }
            
        }
        
        Resources.UnloadUnusedAssets();
    }

    public static Visitor GenerateVisitor()
    {
        var name = namesList[Random.Range(0, namesList.Count)];
        var surname = surnamesList[Random.Range(0, surnamesList.Count)];
        var message = messagesList[Random.Range(0, messagesList.Count)];
        var avatar = avatarsList[Random.Range(0, avatarsList.Count)];
        Visitor visitor = new Visitor(name, surname, message, avatar);
        
        return visitor;
    }
}
