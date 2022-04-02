using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int aggressiveness = 0;
    public Slider slider;

    public EndGamePanel endGamePanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void EnemyKilled()
    {
        aggressiveness++;
        slider.value = aggressiveness;
    }

    public void EndGame()
    {
        endGamePanel.gameObject.SetActive(true);
        string message = "";
        switch (aggressiveness)
        {
            case 0:
                message = "Tu es un ange";
                break;
            case 1:
                message = "Un accident ça arrive";
                break;
            case 2:
                message = "Une petite tendance à éclater les autres";
                break;
            case 3:
                message = "Carrément un psychopathe";
                break;
        }
        endGamePanel.SetMessage(message);
    }
}
