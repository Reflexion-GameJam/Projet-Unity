using System.Collections;
using UnityEngine;

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
        EventManager.OnLockPlayer += StopWalkingAnim;
    }

    void Update()
    {
        if (!PlayerController.canMove)
        {
            return;
        }

        // If the player is moving
        if (Input.GetButton("Horizontal"))
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
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

    private IEnumerator StartTeleportAnimCoroutine()
    {
        playerAnim.SetBool("isTeleporting", true);
        yield return new WaitForSeconds(0.2f);
        playerAnim.SetBool("isTeleporting", false);

        playerAnim.SetBool("isAlternative", (GameManager.currentWorld == World.ALTERNATIVE));
    }
}
