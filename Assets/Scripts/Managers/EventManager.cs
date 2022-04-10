using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // --- Singleton ---
    public static EventManager Instance { get; private set; }

    public static event Action OnLockPlayer;
    public static event Action OnUnlockPlayer;
    public static event Action OnEnemyLaugh;
    public static event Action OnPlayerAttack;
    public static event Action OnTeleportPlayer;

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

    public static void InvokeLockPlayer()
    {
        OnLockPlayer?.Invoke();
    }

    public static void InvokeUnlockPlayer()
    {
        OnUnlockPlayer?.Invoke();
    }
    public static void InvokeEnemyLaugh()
    {
        OnEnemyLaugh?.Invoke();
    }

    public static void InvokePlayerAttack()
    {
        OnPlayerAttack?.Invoke();
    }

    public static void InvokeTeleportPlayer()
    {
        OnTeleportPlayer?.Invoke();
    }
}
