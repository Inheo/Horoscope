using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : OpenOrClosePanelWithFade
{
    [SerializeField] private HoroscopePanel _horoscopePanel;

    public static MenuPanel Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void OpenHoroscopePanel(int i)
    {
        TemporaryVariables.SelectedZodiac = i;
        _horoscopePanel.UpdateUI();
        _horoscopePanel.Open();
    }
}
