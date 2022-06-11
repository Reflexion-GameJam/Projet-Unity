using UnityEngine;

/// <summary>
/// Class to manage the pause panel
/// </summary>
public class PausePanel : MonoBehaviour
{
    public void OnBackToMenuBtnClick() // when the back to menu button is clicked
    {
        GameManager.BackToMenu(); // go back to the menu
    }
}
