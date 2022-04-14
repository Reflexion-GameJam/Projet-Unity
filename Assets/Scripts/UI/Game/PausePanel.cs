using UnityEngine;

/// <summary>
/// Class to manage the pause panel
/// </summary>
public class PausePanel : MonoBehaviour
{
    public void OnBackToMenuBtnClick()
    {
        GameManager.BackToMenu();
    }
}
