using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool canTeleport = true;

    public static bool playerIsTop = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Teleport(GameObject linkedTeleporter)
    {
        if (!canTeleport)
            return;

        transform.Rotate(new Vector3(180, 0, 0));
        transform.position = linkedTeleporter.transform.position;
        rb.gravityScale = -rb.gravityScale;
        playerIsTop = !playerIsTop;
        canTeleport = false;
        Invoke("CanTeleport", 1f);
    }

    private void CanTeleport()
    {
        canTeleport = true;
    }
}
