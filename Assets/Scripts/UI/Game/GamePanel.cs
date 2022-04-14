using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to manage the game panel
/// </summary>
public class GamePanel : MonoBehaviour
{
    [SerializeField]
    private PausePanel pausePanel;
    [SerializeField]
    private EndGamePanel endGamePanel;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject interactionText;

    public void SetAggressiveness(int value)
    {
        slider.value = value;
    }

    /// <summary>
    /// Show pause panel
    /// </summary>
    public void Pause()
    {
        pausePanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide pause panel
    /// </summary>
    public void Unpause()
    {
        pausePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show the end game panel and give it the corresponding sprite to show
    /// </summary>
    /// <param name="message">Sprite to show at the end</param>
    public void EndGame(Sprite message)
    {
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.SetMessage(message);
    }

    public void ShowInteractionText()
    {
        interactionText.SetActive(true);
    }

    public void HideInteractionText()
    {
        interactionText.SetActive(false);
    }
}
