using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGeneral : MonoBehaviour
{
    public static DataGeneral instance;

    public byte GameQuality = 3;
    public byte Languages = 0;
    
    public void Awake()
    {
        instance = this;
        
        if(PlayerPrefs.HasKey("GameQuality"))
        {
            GameQuality = (byte)PlayerPrefs.GetInt("GameQuality");
        }
        else
        {
            PlayerPrefs.SetInt("GameQuality", 3);
        }
        
        if(PlayerPrefs.HasKey("Languages"))
        {
            Languages = (byte)PlayerPrefs.GetInt("Languages");
        }
        else
        {
            PlayerPrefs.SetInt("Languages", 0);
        }
    }
    
    public void Start()
    {
        try
        {
            QualitySettings.SetQualityLevel(GameQuality);
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
            Debug.Log("Relaunch");
        }
    }
    
    // Sauvegarder toute les données
    public void SaveData()
    {
        PlayerPrefs.SetInt("GameQuality", GameQuality);
        PlayerPrefs.SetInt("Languages", Languages);
        Debug.Log("<color=green>Sauvegarde des données</color>");
    }

    public void DestroyAllData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("<color=red>Suppression des données</color>");
        // relancer la même scène du jeu
        Application.LoadLevel(Application.loadedLevel);
    }
}
