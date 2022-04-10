using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // --- Singleton ---
    public static GameManager Instance { get; private set; }

    // --- Enum to know which world we're in ---
    public static World currentWorld { get; private set; }

    // --- UI panel ---
    [SerializeField]
    private static GamePanel gamePanel;
    // --- Sprites to set at the end ---
    [SerializeField]
    public static Sprite[] endSprites = { null, null, null };

    [SerializeField]
    private static int aggressiveness = 0;
    private static bool isPaused;

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

    public static void Pause(bool endGame)
    {
        isPaused = true;
        UnlockMouse();
        if (!endGame) gamePanel.Pause();
        Time.timeScale = 0;
    }

    public static void Unpause()
    {
        Time.timeScale = 1;
        gamePanel.Unpause();
        LockMouse();
        isPaused = false;
    }

    private static void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private static void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void TeleportPlayer()
    {
        if (currentWorld == World.REAL)
            currentWorld = World.ALTERNATIVE;
        else
            currentWorld = World.REAL;

        EventManager.InvokeTeleportPlayer();
    }

    public static void EnemyFound()
    {
        gamePanel.ShowInteractionText();

        EventManager.InvokeLockPlayer();
    }

    public static void EnemyAttacked()
    {
        gamePanel.HideInteractionText();

        TeleportPlayer();
        EventManager.InvokeUnlockPlayer();
    }

    public static void EnemyDodged()
    {
        gamePanel.HideInteractionText();

        EventManager.InvokeUnlockPlayer();
    }

    public static void EnemyKilled()
    {
        aggressiveness++;
        gamePanel.SetAggressiveness(aggressiveness);

        TeleportPlayer();
    }

    public static void EndGame()
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

    public static void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
