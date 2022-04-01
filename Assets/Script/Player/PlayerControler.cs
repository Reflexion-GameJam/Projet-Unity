using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    [Header("Basic Information Player"), Space(5)]
    [SerializeField] private Rigidbody2D rb;

    public float PowerJump = 1.0f;

    private void Start()
    {
        // Si Rigidbody2D vide récupération auto
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ControlPlayer();
    }

    private void FixedUpdate()
    {
        float dirX = Input.GetAxis("Horizontal");

        // Déplacement du joueur
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
    }


    public void ControlPlayer()
    {
        // Faire sauté le joueur
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * PowerJump, ForceMode2D.Impulse);
        }
    }
}
