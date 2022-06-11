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
    private PausePanel pausePanel; // Reference to the pause panel
    [SerializeField]
    private EndGamePanel endGamePanel; // Reference to the end game panel

    [SerializeField]
    private Slider slider; // Reference to the slider
    [SerializeField]
    private GameObject interactionText; // Reference to the interaction text

    public void SetAggressiveness(int value) // Set the slider value
    {
        slider.value = value;
    }

    /// <summary>
    /// Show pause panel
    /// </summary>
    public void Pause() 
    {
        pausePanel.gameObject.SetActive(true); // Show the pause panel
    }

    /// <summary>
    /// Hide pause panel
    /// </summary>
    public void Unpause()
    {
        pausePanel.gameObject.SetActive(false); // Hide the pause panel
    }

    /// <summary>
    /// Show the end game panel and give it the corresponding sprite to show
    /// </summary>
    /// <param name="message">Sprite to show at the end</param>
    public void EndGame(Sprite message)
    {
        endGamePanel.gameObject.SetActive(true); // Show the end game panel
        endGamePanel.SetMessage(message); // Set the sprite to show
    }

    public void ShowInteractionText() // Show the interaction text
    {
        interactionText.SetActive(true);
    }

    public void HideInteractionText()   // Hide the interaction text
    {
        interactionText.SetActive(false);
    }
}
