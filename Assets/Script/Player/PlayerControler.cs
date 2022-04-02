using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    [Header("Basic Information Player"), Space(5)]
    [SerializeField] private Rigidbody2D rb;

    public float PowerMinSpeed = 2.0f;
    public float PowerMaxSpeed = 4.0f;
    public float CurrentSpeed = 2.0f;
    public bool isAlterne = false;

    [Header("Effect Particle"), Space(5)]
    public GameObject ParticleWalk;

    // Event pour déclencher un son de saut
    public static event Action OnJump;

    private void Start()
    {
        // Si Rigidbody2D vide récupération auto
        if (rb == null)
        {
            rb = this.gameObject.GetComponent<Rigidbody2D>();
        }
        
        // Si Particule Walk non assigné
        if (ParticleWalk == null)
        {
            ParticleWalk = GameObject.Find("WalkEffectPlayer");
        }
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrentSpeed = PowerMaxSpeed;
        }
        else
        {
            CurrentSpeed = PowerMinSpeed;
        }

        // Si le joueur bouge
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetButton("Horizontal"))
        {
            ParticleWalkUpdate(true);
        }
        else
        {
            ParticleWalkUpdate(false);
        }

        // Détection si le joueur passe sur l'autre monde
        isAlterne = rb.gravityScale == 1 ? false : true;
    }

    private void FixedUpdate()
    {
        float dirX = Input.GetAxis("Horizontal");
        FlipImage(dirX);

        // Déplacement du joueur
        rb.velocity = new Vector2(dirX * CurrentSpeed, rb.velocity.y);
    }
    

    // Fonction qui permet de faire tourner le sprite pour lui faire changer de direction
    private void FlipImage(float inputX)
    {
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Détection si le joueur bouge la fonction est appelé
    private void ParticleWalkUpdate(bool i)
    {
        if (i)
        {
            ParticleWalk.SetActive(true);
        }
        else
        {
            ParticleWalk.SetActive(false);
        }
    }
}
