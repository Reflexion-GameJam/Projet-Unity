using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource audioSource;

    #region AudioClips
    [Header("Musiques"), Space(5)]

    [SerializeField]
    private AudioClip topMusic;
    [SerializeField]
    private AudioClip bottomMusic;

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

        // Si l'Audio Source n'a pas été assigné
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnEnable()
    {
        // On écoute les events pour déclencher les sons
        PlayerTeleport.OnTeleport += PlayTeleportSound;
        PlayerInteraction.OnAttack += PlayAttackSound;
        Enemy.OnLaugh += PlayLaughSound;
    }

    void OnDisable()
    {
        // On arrête d'écouter les events
        PlayerTeleport.OnTeleport -= PlayTeleportSound;
        PlayerInteraction.OnAttack -= PlayAttackSound;
        Enemy.OnLaugh += PlayLaughSound;
    }

    private void ChangeWorldMusic()
    {
        if (PlayerTeleport.realWorld)
            audioSource.clip = topMusic;
        else
            audioSource.clip = bottomMusic;

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
