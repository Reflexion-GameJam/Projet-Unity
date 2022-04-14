using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton class used to manage the gameplay, to manage the play/pause system, and to interact with UI.
/// Only one instance of this class will be active in the scene
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Single active instance of GameManager
    /// </summary>
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Enum to know which world we're currently in
    /// </summary>
    public World currentWorld { get; private set; }

    /// <summary>
    /// UI panel
    /// </summary>
    [SerializeField]
    private GamePanel gamePanel;

    /// <summary>
    /// Sprites to set at the end
    /// </summary>
    [SerializeField]
    public Sprite[] endSprites = { null, null, null };

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
        currentWorld = World.REAL;
    }

    void Update()
    {
        // If the player presses "Escape" button
        if (Input.GetButtonUp("Cancel"))
        {
            if (isPaused)
                Unpause();
            else
                Pause(false);
        }
    }

    /// <summary>
    /// Pause the game and unlock mouse's cursor
    /// </summary>
    /// <param name="endGame">If false, shows pause panel</param>
    public void Pause(bool endGame)
    {
        isPaused = true;
        UnlockMouse();
        if (!endGame) gamePanel.Pause();
        Time.timeScale = 0;
    }

    /// <summary>
    /// Unpause the game and lock mouse's cursor
    /// </summary>
    public void Unpause()
    {
        Time.timeScale = 1;
        gamePanel.Unpause();
        LockMouse();
        isPaused = false;
    }

    /// <summary>
    /// Lock and hide mouse's cursor
    /// </summary>
    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Unlock and show mouse's cursor
    /// </summary>
    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Change current world and invoke teleportation event
    /// </summary>
    public void TeleportPlayer()
    {
        if (currentWorld == World.REAL)
            currentWorld = World.ALTERNATIVE;
        else
            currentWorld = World.REAL;

        EventManager.InvokeTeleportPlayer();
    }

    /// <summary>
    /// Called when the player meets an enemy
    /// </summary>
    public void EnemyFound()
    {
        gamePanel.ShowInteractionText();

        EventManager.InvokeLockPlayer();
    }

    /// <summary>
    /// Called when the player attacks an enemy
    /// </summary>
    public void EnemyAttacked()
    {
        gamePanel.HideInteractionText();

        TeleportPlayer();
        EventManager.InvokeUnlockPlayer();
    }

    /// <summary>
    /// Called when the player hides from the enemy
    /// </summary>
    public void EnemyDodged()
    {
        gamePanel.HideInteractionText();

        EventManager.InvokeUnlockPlayer();
    }

    /// <summary>
    /// Called when the player attacks an enemy in the alternative world
    /// </summary>
    public void EnemyKilled()
    {
        // Increase aggressiveness and update the UI (slider)
        aggressiveness++;
        gamePanel.SetAggressiveness(aggressiveness);

        // Teleport the player back to the real world
        TeleportPlayer();
    }

    /// <summary>
    /// Pause the game and show an image depending on the agressiveness (multiple endings)
    /// </summary>
    public void EndGame()
    {
        Pause(true);
        Sprite message = null;
        switch (aggressiveness)
        {
            case 0:
                message = endSprites[0];
                break;
            case 1:
                message = endSprites[1];
                break;
            case 2:
                message = endSprites[1];
                break;
            default:
                message = endSprites[2];
                break;
        }
        gamePanel.EndGame(message);
    }

    /// <summary>
    /// Load "Menu" scene
    /// </summary>
    public static void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
