using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to manage the menu panel
/// </summary>
public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject menu; // menu panel
    [SerializeField]
    private GameObject controls; // controls panel

    public void OnPlayBtnClick() // when play button is clicked
    {
        SceneManager.LoadScene("Game"); // load game scene
    }

    public void OnControlsBtnClick() // when controls button is clicked
    {
        menu.SetActive(false); // hide menu panel
        controls.SetActive(true); // show controls panel
    }

    public void OnBackBtnClick() // when back button is clicked
    {
        controls.SetActive(false); // hide controls panel
        menu.SetActive(true); // show menu panel
    }

    public void OnQuitBtnClick()  // when quit button is clicked
    {
#if UNITY_STANDALONE
        Application.Quit(); // quit application
#endif
    }
}
