using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    
    private void Start()
    {
        try
        {
            dropdown.value = DataGeneral.instance.Languages;
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e);
        }
    }
    
    public void ChangeLanguages(int Language)
    {
        LocalizationSettings.Instance.SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[Language]);
        DataGeneral.instance.Languages = (byte)Language;
        DataGeneral.instance.SaveData();
    }
}