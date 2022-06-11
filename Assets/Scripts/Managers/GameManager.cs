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
    public static GameManager Instance { get; private set; } // singleton instance

    /// <summary>
    /// Enum to know which world we're currently in
    /// </summary>
    public World currentWorld { get; private set; } // current world

    /// <summary>
    /// UI panel
    /// </summary>
    [SerializeField]
    private GamePanel gamePanel; // UI panel

    /// <summary>
    /// Sprites to set at the end
    /// </summary>
    [SerializeField]
    public Sprite[] endSprites = { null, null, null }; // sprites to set at the end

    private int aggressiveness = 0; // aggressiveness of the player
    private bool isPaused; // is the game paused?

    void Awake()
    {
        if (Instance != null && Instance != this) // if there is already an instance of GameManager
        {
            Destroy(this); // destroy this instance
            return;
        }
        else
        {
            Instance = this; // set this instance as the active instance
        }
    }

    void Start()
    {
        if (!gamePanel) // if the UI panel is not set
            gamePanel = GameObject.Find("GamePanel").GetComponent<GamePanel>(); // set it

        Unpause(); // unpause the game
        currentWorld = World.REAL; // set the current world to real
    }

    void Update()
    {
        // If the player presses "Escape" button
        if (Input.GetButtonUp("Cancel")) 
        {
            if (isPaused) // if the game is paused
                Unpause(); // unpause the game
            else
                Pause(false); // pause the game
        }
    }

    /// <summary>
    /// Pause the game and unlock mouse's cursor
    /// </summary>
    /// <param name="endGame">If false, shows pause panel</param>
    public void Pause(bool endGame)
    {
        isPaused = true; // set the game as paused
        UnlockMouse(); // unlock the mouse
        if (!endGame) gamePanel.Pause(); // if we're not ending the game, show the pause panel
        Time.timeScale = 0; // set the time scale to 0
    }

    /// <summary>
    /// Unpause the game and lock mouse's cursor
    /// </summary>
    public void Unpause()
    {
        Time.timeScale = 1; // set the time scale to 1
        gamePanel.Unpause(); // hide the pause panel
        LockMouse(); // lock the mouse
        isPaused = false; // set the game as not paused
    }

    /// <summary>
    /// Lock and hide mouse's cursor
    /// </summary>
    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the mouse
        Cursor.visible = false; // hide the mouse
    }

    /// <summary>
    /// Unlock and show mouse's cursor
    /// </summary>
    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None; // unlock the mouse
        Cursor.visible = true; // show the mouse
    }

    /// <summary>
    /// Change current world and invoke teleportation event
    /// </summary>
    public void TeleportPlayer()
    {
        if (currentWorld == World.REAL) // if we're in the real world
            currentWorld = World.ALTERNATIVE; // set the current world to the alternative world
        else
            currentWorld = World.REAL; // set the current world to the real world

        EventManager.InvokeTeleportPlayer(); // invoke the teleportation event
    }

    /// <summary>
    /// Called when the player meets an enemy
    /// </summary>
    public void EnemyFound()
    {
        gamePanel.ShowInteractionText(); // show the interaction text

        EventManager.InvokeLockPlayer(); // invoke the lock player event
    }

    /// <summary>
    /// Called when the player attacks an enemy
    /// </summary>
    public void EnemyAttacked()
    {
        gamePanel.HideInteractionText(); // hide the interaction text

        TeleportPlayer();
        EventManager.InvokeUnlockPlayer(); // invoke the unlock player event
    }

    /// <summary>
    /// Called when the player hides from the enemy
    /// </summary>
    public void EnemyDodged()
    {
        gamePanel.HideInteractionText(); // hide the interaction text

        EventManager.InvokeUnlockPlayer(); // invoke the unlock player event
    }

    /// <summary>
    /// Called when the player attacks an enemy in the alternative world
    /// </summary>
    public void EnemyKilled()
    {
        // Increase aggressiveness and update the UI (slider)
        aggressiveness++; 
        gamePanel.SetAggressiveness(aggressiveness); // set the slider to the new value

        // Teleport the player back to the real world
        TeleportPlayer(); 
    }

    /// <summary>
    /// Pause the game and show an image depending on the agressiveness (multiple endings)
    /// </summary>
    public void EndGame()
    {
        Pause(true); // pause the game
        Sprite message = null; // message to show
        switch (aggressiveness) // depending on the aggressiveness
        {
            case 0: // if the player is not aggressive
                message = endSprites[0]; 
                break;
            case 1: // if the player is aggressive
                message = endSprites[1];
                break;
            case 2: // if the player is very aggressive
                message = endSprites[1];
                break;
            default: // if the player is extremely aggressive
                message = endSprites[2];
                break;
        }
        gamePanel.EndGame(message); // show the message
    }

    /// <summary>
    /// Load "Menu" scene
    /// </summary>
    public static void BackToMenu()
    {
        SceneManager.LoadScene("Menu"); // load the menu scene
    }
}
