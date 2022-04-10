using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // --- Singleton ---
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

    void OnEnable()
    {
        // Listen events
        EventManager.OnTeleportPlayer += PlayTeleportSound;
        EventManager.OnPlayerAttack += PlayAttackSound;
        EventManager.OnEnemyLaugh += PlayLaughSound;
    }

    void OnDisable()
    {
        // Stop listening events
        EventManager.OnTeleportPlayer -= PlayTeleportSound;
        EventManager.OnPlayerAttack -= PlayAttackSound;
        EventManager.OnEnemyLaugh -= PlayLaughSound;
    }

    private void ChangeWorldMusic()
    {
        if (GameManager.currentWorld == World.REAL)
            audioSource.clip = realWorldMusic;
        else
            audioSource.clip = alternativeWorldMusic;

        // Attendre la fin du son de téléportation
        audioSource.PlayDelayed(teleport.length - 1f);
    }

    public void PlayTeleportSound()
    {
        audioSource.PlayOneShot(teleport);
        ChangeWorldMusic();
    }

    public void PlayLaughSound()
    {
        audioSource.PlayOneShot(childLaugh);
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attack2);
    }
}
