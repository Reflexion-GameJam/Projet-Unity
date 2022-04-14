using System;
using UnityEngine;

/// <summary>
/// Singleton class used to manage Events.
/// Only one instance of this class will be active in the scene
/// </summary>
public class EventManager : MonoBehaviour
{
    /// <summary>
    /// Single active instance of EventManager
    /// </summary>
    public static EventManager Instance { get; private set; }

    #region Events
    public static event Action OnLockPlayer;
    public static event Action OnUnlockPlayer;
    public static event Action OnEnemyLaugh;
    public static event Action OnPlayerAttack;
    public static event Action OnTeleportPlayer;
    #endregion

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
