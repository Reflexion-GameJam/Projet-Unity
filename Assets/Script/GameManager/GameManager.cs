using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GamePanel gamePanel;

    private int aggressiveness = 0;
    private bool isPaused;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (!gamePanel)
            gamePanel = GameObject.Find("GamePanel").GetComponent<GamePanel>();

        Unpause();
    }

    void Update()
    {
        // Si le joueur appuie sur "Echap"
        if (Input.GetButtonUp("Cancel"))
        {
            if (isPaused)
                Unpause();
            else
                Pause();
        }
    }

    // Mise en pause
    public void Pause()
    {
        isPaused = true;
        UnlockMouse();
        gamePanel.Pause();
        Time.timeScale = 0;
    }

    // Mise en marche
    public void Unpause()
    {
        Time.timeScale = 1;
        gamePanel.Unpause();
        LockMouse();
        isPaused = false;
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void KilledEnemy()
    {
        aggressiveness++;
        gamePanel.SetAggressiveness(aggressiveness);
    }

    public void EndGame()
    {
        Pause();
        string message = "";
        switch (aggressiveness)
        {
            case 0:
                message = "Tu es un ange";
                break;
            case 1:
                message = "Un accident ça arrive";
                break;
            case 2:
                message = "Une petite tendance à éclater les autres";
                break;
            case 3:
                message = "Carrément un psychopathe";
                break;
        }
        gamePanel.EndGame(message);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
