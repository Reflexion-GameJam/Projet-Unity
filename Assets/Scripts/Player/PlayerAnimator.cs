using System.Collections;
using UnityEngine;

/// <summary>
/// Class used to manage the animations of the player
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnim; // Reference to the player's animator

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>(); // Get the reference to the player's animator
    }

    void OnEnable() // Called when the object is enabled
    {
        EventManager.OnLockPlayer += StopWalkingAnim; // Subscribe to the event that stops the walking animation
        EventManager.OnTeleportPlayer += StartTeleportAnim; // Subscribe to the event that starts the teleport animation
    }

    void OnDisable() // Called when the object is disabled
    {
        EventManager.OnTeleportPlayer -= StartTeleportAnim; // Unsubscribe from the event that starts the teleport animation
        EventManager.OnLockPlayer -= StopWalkingAnim; // Unsubscribe from the event that stops the walking animation
    }

    void Update()
    {
        // Do nothing if the player can't move
        if (!PlayerController.canMove)
        {
            return; // Exit the function
        }

        // If the player is moving
        if (Input.GetButtonDown("Horizontal")) // If the player is pressing the horizontal button
        {
            playerAnim.SetBool("isWalking", true); // Start the walking animation
        }
        if (Input.GetButtonUp("Horizontal")) // If the player is not pressing the horizontal button
        {
            StopWalkingAnim(); // Stop the walking animation
        }
    }

    private void StopWalkingAnim() // Function used to stop the walking animation
    {
        playerAnim.SetBool("isWalking", false); // Stop the walking animation
    }

    private void StartTeleportAnim() // Function used to start the teleport animation
    {
        StartCoroutine(StartTeleportAnimCoroutine()); // Start the coroutine that starts the teleport animation
    }

    /// <summary>
    /// Coroutine to enable teleportation animation for a certain duration
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartTeleportAnimCoroutine()
    {
        playerAnim.SetBool("isTeleporting", true); // Start the teleport animation
        yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds
        playerAnim.SetBool("isTeleporting", false); // Stop the teleport animation

        playerAnim.SetBool("isAlternative", (GameManager.Instance.currentWorld == World.ALTERNATIVE)); // Set the isAlternative parameter to true if the current world is the alternative world
    }
}
