using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool canTeleport = true;

    public static bool playerIsTop = true;

    public static event Action OnTeleport;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Teleport(GameObject linkedTeleporter)
    {
        if (!canTeleport)
            return;

        // Déplacement + rotation du personnage
        transform.position = linkedTeleporter.transform.position;
        transform.Rotate(new Vector3(180, 0, 0));

        // Changement de la gravité
        rb.gravityScale = -rb.gravityScale;

        playerIsTop = !playerIsTop;
        canTeleport = false;

        OnTeleport?.Invoke();
        Invoke("CanTeleport", 1f);
    }

    private void CanTeleport()
    {
        canTeleport = true;
    }
}
