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

    public Sprite[] SpriteFin = {null,null,null};

    [SerializeField] private int aggressiveness = 0;
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
                Pause(false);
        }
    }

    // Mise en pause
    public void Pause(bool endGame)
    {
        isPaused = true;
        UnlockMouse();
        if (!endGame) gamePanel.Pause();
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

    public void EnemyFound()
    {
        gamePanel.ShowInteractionText();
    }

    public void EnemyAttacked()
    {
        gamePanel.HideInteractionText();
    }

    public void EndGame()
    {
        Pause(true);
        Sprite message = null;
        switch (aggressiveness)
        {
            case 0:
                message = SpriteFin[0];
                break;
            case 1:
                message = SpriteFin[1];
                break;
            case 2:
                message = SpriteFin[1];
                break;
            default:
                message = SpriteFin[2];
                break;
        }
        gamePanel.EndGame(message);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
