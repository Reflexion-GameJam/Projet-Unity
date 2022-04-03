using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private PausePanel pausePanel;
    [SerializeField]
    private EndGamePanel endGamePanel;

    public void SetAggressiveness(int value)
    {
        slider.value = value;
    }

    public void Pause()
    {
        pausePanel.gameObject.SetActive(true);
    }

    public void Unpause()
    {
        pausePanel.gameObject.SetActive(false);
    }

    public void EndGame(Sprite message)
    {
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.SetMessage(message);
    }
}
