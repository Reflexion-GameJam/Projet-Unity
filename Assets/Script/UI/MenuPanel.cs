using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject controls;

    public void OnPlayBtnClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnControlsBtnClick()
    {
        menu.SetActive(false);
        controls.SetActive(true);
    }

    public void OnBackBtnClick()
    {
        controls.SetActive(false);
        menu.SetActive(true);
    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
