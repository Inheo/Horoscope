using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanel : OpenOrClosePanelWithFade
{
    [SerializeField] private InputField _nameInputField;
    [SerializeField] private InputField _birthdayInputField;
    [SerializeField] private Toggle[] _sexToggles;

    private void Start()
    {
        _nameInputField.text = Game.Instance.LongParametrs.Name;
        _birthdayInputField.text = Game.Instance.LongParametrs.Birthday;
        _sexToggles[Game.Instance.LongParametrs.Sex].isOn = true;
    }

    public void ChangeSex(int i)
    {
        Game.Instance.LongParametrs.Sex = i;
    }

    public void Save()
    {
        Close();
        Game.Instance.LongParametrs.Name = _nameInputField.text;
        Game.Instance.LongParametrs.Birthday = _birthdayInputField.text;
        Game.Instance.LongParametrs.Save(Game.Instance.LongParametrs);
    }
}
