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
    public static AudioManager Instance { get; private set; } // singleton instance

    [SerializeField]
    private AudioSource audioSource; // audio source used to play sounds

    #region AudioClips 
    [Header("Musiques"), Space(5)] // space between header and first element

    [SerializeField] 
    private AudioClip realWorldMusic; // real world music
    [SerializeField]
    private AudioClip alternativeWorldMusic; // alternative world music

    [Header("Audio clips"), Space(5)]

    [SerializeField]
    private AudioClip bunnyJump; // bunny jump sound
    [SerializeField]
    private AudioClip bullyJump1; // bully jump sound 1
    [SerializeField]
    private AudioClip bullyJump2; // bully jump sound 2
    [SerializeField]
    private AudioClip teleport; // teleport sound
    [SerializeField]
    private AudioClip childLaugh; // child laugh sound
    [SerializeField]
    private AudioClip threeChildrenLaugh; // three children laugh sound
    [SerializeField]
    private AudioClip attack1; // attack sound 1
    [SerializeField]
    private AudioClip attack2; // attack sound 2

    #endregion

    void Awake() // called before start
    {
        if (Instance != null && Instance != this) // if an instance of AudioManager already exists
        {
            Destroy(this); // destroy this instance
            return;
        }
        else
        {
            Instance = this; // set this instance as the active instance
        }

        if (audioSource == null) // if audio source is not set
        {
            audioSource = GetComponent<AudioSource>();  // set audio source
        }
    }

    /// <summary>
    /// When enabled, listen to events that will be invoked
    /// </summary>
    void OnEnable()
    {
        EventManager.OnTeleportPlayer += PlayTeleportSound; // listen to teleport event
        EventManager.OnPlayerAttack += PlayAttackSound; // listen to event that will play attack sound
        EventManager.OnEnemyLaugh += PlayLaughSound; // listen to event that will play laugh sound
    }

    /// <summary>
    /// When disaabled, stop listening to events
    /// </summary>
    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= PlayTeleportSound; // stop listening to teleport event
        EventManager.OnPlayerAttack -= PlayAttackSound; // stop listening to event that will play attack sound
        EventManager.OnEnemyLaugh -= PlayLaughSound; // stop listening to event that will play laugh sound
    }

    /// <summary>
    /// Play teleport sounds and changes the background music
    /// </summary>
    public void PlayTeleportSound()
    {
        audioSource.PlayOneShot(teleport); // play teleport sound
        ChangeWorldMusic(); // change background music
    }

    /// <summary>
    /// Change the background music depending on the world we'll be teleported in
    /// </summary>
    private void ChangeWorldMusic()
    {
        if (GameManager.Instance.currentWorld == World.REAL) // if we're in the real world
            audioSource.clip = realWorldMusic; // set the music to real world music
        else
            audioSource.clip = alternativeWorldMusic; // set the music to alternative world music

        // Wait for the end of the "teleportation" sound before playing the new music
        audioSource.PlayDelayed(teleport.length - 1f);
    }

    /// <summary>
    /// To play when the player meets an enemy in the real world
    /// </summary>
    public void PlayLaughSound()
    {
        audioSource.PlayOneShot(childLaugh); // play child laugh sound
    }

    /// <summary>
    /// To play when the player attacks an enemy
    /// </summary>
    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attack2); // play attack sound 2
    }
}
