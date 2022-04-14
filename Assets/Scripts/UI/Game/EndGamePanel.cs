using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to manage the end game panel
/// </summary>
public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private Image messageImage;

    public void SetMessage(Sprite message)
    {
        messageImage.sprite = message;
    }

    public void OnBackToMenuBtnClick()
    {
        GameManager.BackToMenu();
    }

}
