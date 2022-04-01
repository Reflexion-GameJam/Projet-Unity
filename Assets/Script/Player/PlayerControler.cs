using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    [Header("Basic Information Player"), Space(5)]
    [SerializeField] private Rigidbody2D rb;

    public float PowerJump = 1.0f;
    public float PowerSpeed = 7.0f;
    public bool canJump = true;

    private void Start()
    {
        // Si Rigidbody2D vide récupération auto
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ControlPlayer();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            PowerSpeed = 9.0f;
        }
        else
        {
            PowerSpeed = 7.0f;
        }
    }

    private void FixedUpdate()
    {
        float dirX = Input.GetAxis("Horizontal");

        // Déplacement du joueur
        rb.velocity = new Vector2(dirX * PowerSpeed, rb.velocity.y);
    }


    public void ControlPlayer()
    {
        // Faire sauté le joueur
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * PowerJump, ForceMode2D.Impulse);
                canJump = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 directionJump = new Vector2(0.0f, -2.0f);
        Gizmos.DrawRay(transform.position, directionJump);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!canJump)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                canJump = true;
            }
        }
    }
}
