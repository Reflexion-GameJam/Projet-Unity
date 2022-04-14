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
    public GameObject walkParticle;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float minSpeed = 3.0f;
    [SerializeField]
    private float maxSpeed = 5.0f;
    private float currentSpeed;

    [SerializeField]
    private float realWorldSpawnHeight = 7.0f;
    private float alternativeWorldSpawnHeight = -7.0f;
    private float spawnHeight;

    public static bool canMove { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (walkParticle == null)
        {
            walkParticle = transform.GetChild(0).gameObject;
        }

        currentSpeed = minSpeed;
        spawnHeight = realWorldSpawnHeight;
        canMove = true;
    }

    void OnEnable()
    {
        EventManager.OnTeleportPlayer += Teleport;
        EventManager.OnLockPlayer += LockMovement;
        EventManager.OnUnlockPlayer += UnlockMovement;
    }

    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= Teleport;
        EventManager.OnLockPlayer -= LockMovement;
        EventManager.OnUnlockPlayer -= UnlockMovement;
    }

    void Update()
    {
        // Do nothing if the player can't move
        if (!canMove)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = maxSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = minSpeed;
        }

        // If the player is moving
        if (Input.GetButtonDown("Horizontal"))
        {
            walkParticle.SetActive(true);
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            walkParticle.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        float dirX = Input.GetAxis("Horizontal");
        FlipImageX(dirX);

        // Move the player
        rb.velocity = new Vector2(dirX * currentSpeed, rb.velocity.y);
    }

    /// <summary>
    /// Flip sprite orientation depending on the direction that the player is facing
    /// </summary>
    /// <param name="inputX">Value of the horizontal axis input</param>
    private void FlipImageX(float inputX)
    {
        // Left
        if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Right
        else if (inputX > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    /// <summary>
    /// Adapt the player's position, rotation and gravity scale to the new world he'll be teleported in
    /// </summary>
    private void Teleport()
    {
        if (GameManager.Instance.currentWorld == World.REAL)
        {
            spawnHeight = realWorldSpawnHeight;
        }
        else
        {
            spawnHeight = alternativeWorldSpawnHeight;
        }

        transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        transform.Rotate(new Vector3(180, 0, 0));

        rb.gravityScale = -rb.gravityScale;
    }

    private void LockMovement()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
        walkParticle.SetActive(false);
    }

    private void UnlockMovement()
    {
        canMove = true;
    }
}
