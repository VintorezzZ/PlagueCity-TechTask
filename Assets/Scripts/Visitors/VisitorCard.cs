using MyGame.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class VisitorCard : MonoBehaviour
{
    private Button _button;
    private Visitor _visitor;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text surname;
    [SerializeField] private Image avatar;

    public Visitor Visitor => _visitor;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayClick();
            EventHub.OnVisitorViewShow(this);
            ViewManager.ShowPopup<VisitorView>();
        });
    }

    public void Init(Visitor visitor)
    {
        _visitor = visitor;
        name.text = visitor.Name;
        surname.text = visitor.Surname;
        avatar.sprite = visitor.Avatar;
    }
}
