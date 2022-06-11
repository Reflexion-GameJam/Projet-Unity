using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to manage the end game panel
/// </summary>
public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private Image messageImage; // Image to display the message

    public void SetMessage(Sprite message) // Set the message to display
    {
        messageImage.sprite = message; // Set the image to display the message
    }

    public void OnBackToMenuBtnClick() // Called when the back to menu button is clicked
    {
        GameManager.BackToMenu(); // Go back to the menu
    }

}
