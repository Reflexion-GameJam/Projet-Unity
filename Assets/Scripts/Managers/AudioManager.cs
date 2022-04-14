using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class used to manage Audio sounds.
/// Only one instance of this class will be active in the scene
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Single active instance of AudioManager
    /// </summary>
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource audioSource;

    #region AudioClips
    [Header("Musiques"), Space(5)]

    [SerializeField]
    private AudioClip realWorldMusic;
    [SerializeField]
    private AudioClip alternativeWorldMusic;

    [Header("Audio clips"), Space(5)]

    [SerializeField]
    private AudioClip bunnyJump;
    [SerializeField]
    private AudioClip bullyJump1;
    [SerializeField]
    private AudioClip bullyJump2;
    [SerializeField]
    private AudioClip teleport;
    [SerializeField]
    private AudioClip childLaugh;
    [SerializeField]
    private AudioClip threeChildrenLaugh;
    [SerializeField]
    private AudioClip attack1;
    [SerializeField]
    private AudioClip attack2;

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

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// When enabled, listen to events that will be invoked
    /// </summary>
    void OnEnable()
    {
        EventManager.OnTeleportPlayer += PlayTeleportSound;
        EventManager.OnPlayerAttack += PlayAttackSound;
        EventManager.OnEnemyLaugh += PlayLaughSound;
    }

    /// <summary>
    /// When disaabled, stop listening to events
    /// </summary>
    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= PlayTeleportSound;
        EventManager.OnPlayerAttack -= PlayAttackSound;
        EventManager.OnEnemyLaugh -= PlayLaughSound;
    }

    /// <summary>
    /// Play teleport sounds and changes the background music
    /// </summary>
    public void PlayTeleportSound()
    {
        audioSource.PlayOneShot(teleport);
        ChangeWorldMusic();
    }

    /// <summary>
    /// Change the background music depending on the world we'll be teleported in
    /// </summary>
    private void ChangeWorldMusic()
    {
        if (GameManager.Instance.currentWorld == World.REAL)
            audioSource.clip = realWorldMusic;
        else
            audioSource.clip = alternativeWorldMusic;

        // Wait for the end of the "teleportation" sound before playing the new music
        audioSource.PlayDelayed(teleport.length - 1f);
    }

    /// <summary>
    /// To play when the player meets an enemy in the real world
    /// </summary>
    public void PlayLaughSound()
    {
        audioSource.PlayOneShot(childLaugh);
    }

    /// <summary>
    /// To play when the player attacks an enemy
    /// </summary>
    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attack2);
    }
}
