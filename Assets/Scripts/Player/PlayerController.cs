using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Basic Information Player"), Space(5)]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    public const float minSpeed = 2.0f;
    [SerializeField]
    public const float maxSpeed = 4.0f;
    private float currentSpeed = minSpeed;

    private const float realWorldSpawnHeight = 7.0f;
    private const float alternativeWorldSpawnHeight = -7.0f;
    private float spawnHeight = realWorldSpawnHeight;

    public static bool canMove { get; private set; }

    [Header("Effect Particle"), Space(5)]
    public GameObject particleWalk;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (particleWalk == null)
        {
            particleWalk = GameObject.Find("WalkEffectPlayer");
        }

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
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = maxSpeed;
        }
        else
        {
            currentSpeed = minSpeed;
        }

        // If the player is moving
        if (Input.GetButtonDown("Horizontal"))
        {
            particleWalk.SetActive(true);
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            particleWalk.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float dirX = Input.GetAxis("Horizontal");
        FlipImageX(dirX);

        // Move the player
        rb.velocity = new Vector2(dirX * currentSpeed, rb.velocity.y);
    }
    

    // Flip sprite orientation depending on the direction that the player is facing
    private void FlipImageX(float inputX)
    {
        if (inputX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Teleport()
    {
        if (GameManager.currentWorld == World.REAL)
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
    }

    private void UnlockMovement()
    {
        canMove = true;
    }
}
