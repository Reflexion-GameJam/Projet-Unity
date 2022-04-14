using System.Collections;
using UnityEngine;

/// <summary>
/// Class used to manage the animations of the player
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnim;

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();
    }

    void OnEnable()
    {
        EventManager.OnLockPlayer += StopWalkingAnim;
        EventManager.OnTeleportPlayer += StartTeleportAnim;
    }

    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= StartTeleportAnim;
        EventManager.OnLockPlayer -= StopWalkingAnim;
    }

    void Update()
    {
        // Do nothing if the player can't move
        if (!PlayerController.canMove)
        {
            return;
        }

        // If the player is moving
        if (Input.GetButtonDown("Horizontal"))
        {
            playerAnim.SetBool("isWalking", true);
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            StopWalkingAnim();
        }
    }

    private void StopWalkingAnim()
    {
        playerAnim.SetBool("isWalking", false);
    }

    private void StartTeleportAnim()
    {
        StartCoroutine(StartTeleportAnimCoroutine());
    }

    /// <summary>
    /// Coroutine to enable teleportation animation for a certain duration
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartTeleportAnimCoroutine()
    {
        playerAnim.SetBool("isTeleporting", true);
        yield return new WaitForSeconds(0.2f);
        playerAnim.SetBool("isTeleporting", false);

        playerAnim.SetBool("isAlternative", (GameManager.Instance.currentWorld == World.ALTERNATIVE));
    }
}
