using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QualityGame : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    
    private void Start()
    {
        try
        {
            dropdown.value = DataGeneral.instance.GameQuality;
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e);
        }
    }
    
    public void ChangeQualityLevel(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
        DataGeneral.instance.GameQuality = (byte)qualityLevel;
        DataGeneral.instance.SaveData();
    }
}
