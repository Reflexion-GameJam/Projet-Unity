using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public int pauseInt;

    private void Start()
    {
        // Si le menu de pause n'est pas assigné
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PauseSystem");
        }
        
        pausePanel.SetActive(false);

        pauseInt = 0;
    }

    private void Update()
    {
        // Si le joueur appuie sur "Echap"
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetButtonUp("Cancel"))
        {
            // Si le panel est désactiver
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }   // Si activer
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }

        // Déblocage de la souris
        if (pauseInt == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Mise en pause
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseInt = 1;
    }

    // Mise en marche
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseInt = 0;
    }
}
