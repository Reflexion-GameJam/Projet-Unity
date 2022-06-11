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
    public static EventManager Instance { get; private set; } // singleton

    #region Events
    public static event Action OnLockPlayer; // event to lock player
    public static event Action OnUnlockPlayer; // event to unlock player
    public static event Action OnEnemyLaugh; // event to play enemy laugh
    public static event Action OnPlayerAttack; // event to play player attack
    public static event Action OnTeleportPlayer; // event to teleport player
    #endregion

    void Awake() 
    {
        if (Instance != null && Instance != this) // if instance already exists and it's not this
        {
            Destroy(this); // destroy this
            return;
        }
        else
        {
            Instance = this; // set instance to this
        }
    }

    public static void InvokeLockPlayer() // invoke lock player event
    {
        OnLockPlayer?.Invoke(); // invoke event
    }

    public static void InvokeUnlockPlayer() // invoke unlock player event
    {
        OnUnlockPlayer?.Invoke(); // invoke event
    }
    public static void InvokeEnemyLaugh() // invoke enemy laugh event
    {
        OnEnemyLaugh?.Invoke();     // invoke event
    }

    public static void InvokePlayerAttack() // invoke player attack event
    {
        OnPlayerAttack?.Invoke(); // invoke event
    }

    public static void InvokeTeleportPlayer() // invoke teleport player event
    {
        OnTeleportPlayer?.Invoke(); // invoke event
    }
}
