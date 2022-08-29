using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class used to manage the movement of the player by listening to keyboard/controller inputs
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [Header("Effect Particle"), Space(5)]
    public GameObject walkParticle; // Particle effect when the player is walking

    private Rigidbody2D rb; // Rigidbody of the player
    private SpriteRenderer spriteRenderer; // Sprite renderer of the player
 
    [SerializeField]
    private float minSpeed = 3.0f; // Minimum speed of the player
    [SerializeField]
    private float maxSpeed = 5.0f; // Maximum speed of the player
    private float currentSpeed; // Current speed of the player

    [SerializeField]
    private float realWorldSpawnHeight = 7.0f; // Height of the player in the real world
    private float alternativeWorldSpawnHeight = -7.0f; // Height of the player in the alternative world
    private float spawnHeight; // Height of the player in the current world

    public static bool canMove { get; private set; } // Boolean used to know if the player can move or not

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the rigidbody of the player
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer of the player
        
        if (walkParticle == null) // If the particle effect is not set
        {
            walkParticle = transform.GetChild(0).gameObject; // Get the particle effect of the player
        }

        currentSpeed = minSpeed; // Set the current speed of the player to the minimum speed
        spawnHeight = realWorldSpawnHeight; // Set the spawn height of the player to the real world spawn height
        canMove = true; // Set the player can move to true
    }

    void OnEnable()
    {
        EventManager.OnTeleportPlayer += Teleport; // Listen to the event OnTeleportPlayer
        EventManager.OnLockPlayer += LockMovement; // Listen to the event OnLockPlayer
        EventManager.OnUnlockPlayer += UnlockMovement; // Listen to the event OnUnlockPlayer
    }

    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= Teleport; // Stop listening to the event OnTeleportPlayer
        EventManager.OnLockPlayer -= LockMovement; // Stop listening to the event OnLockPlayer
        EventManager.OnUnlockPlayer -= UnlockMovement; // Stop listening to the event OnUnlockPlayer
    }

    void Update()
    {
        // Do nothing if the player can't move
        if (!canMove)   
        {
            return; // Exit the function
        }

        if (Input.GetButtonDown("Run")) // If the player press the left shift key
        {
            currentSpeed = maxSpeed; // Set the current speed of the player to the maximum speed 
        }
        else if (Input.GetButtonUp("Run")) //  If the player release the left shift key
        {
            currentSpeed = minSpeed; // Set the current speed of the player to the minimum speed
        }

        // If the player is moving
        if (Input.GetButtonDown("Horizontal")) // If the player press the left or right arrow key
        {
            walkParticle.SetActive(true); // Activate the particle effect
        }
        else if (Input.GetButtonUp("Horizontal")) // If the player release the left or right arrow key
        {
            walkParticle.SetActive(false); // Deactivate the particle effect
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) // If the player can't move
        {
            return;
        }

        float dirX = Input.GetAxis("Horizontal"); // Get the horizontal axis
        FlipImageX(dirX); // Flip the image of the player depending on the direction of the movement

        // Move the player
        rb.velocity = new Vector2(dirX * currentSpeed, rb.velocity.y); // Move the player in the horizontal axis
    }

    /// <summary>
    /// Flip sprite orientation depending on the direction that the player is facing
    /// </summary>
    /// <param name="inputX">Value of the horizontal axis input</param>
    private void FlipImageX(float inputX)
    {
        // Left
        if (inputX < 0) // If the player is moving left
        {
            spriteRenderer.flipX = true; // Flip the sprite to the left
        }
        // Right
        else if (inputX > 0) // If the player is moving right
        {
            spriteRenderer.flipX = false; // Flip the sprite to the right
        }
    }

    /// <summary>
    /// Adapt the player's position, rotation and gravity scale to the new world he'll be teleported in
    /// </summary>
    private void Teleport()
    {
        if (GameManager.Instance.currentWorld == World.REAL) // If the player is in the real world
        {
            spawnHeight = realWorldSpawnHeight; // Set the spawn height of the player to the real world spawn height
        }
        else
        {
            spawnHeight = alternativeWorldSpawnHeight; // Set the spawn height of the player to the alternative world spawn height
        }

        transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z); // Set the player's position to the new spawn height
        transform.Rotate(new Vector3(180, 0, 0)); // Rotate the player to the new world

        rb.gravityScale = -rb.gravityScale; // Invert the gravity scale of the player
    }

    private void LockMovement() // Lock the player's movement
    {
        canMove = false; // Set the player can move to false
        rb.velocity = Vector2.zero; // Stop the player
        walkParticle.SetActive(false); // Deactivate the particle effect
    }

    private void UnlockMovement() // Unlock the player's movement
    {
        canMove = true; // Set the player can move to true
    }
}
