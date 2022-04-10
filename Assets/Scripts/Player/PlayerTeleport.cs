using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField]
    private float spawnHeight = 7.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        EventManager.OnTeleportPlayer += Teleport;
    }

    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= Teleport;
    }

    public void Teleport()
    {
        transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        transform.Rotate(new Vector3(180, 0, 0));

        // Changement de la gravité
        rb.gravityScale = -rb.gravityScale;
        // OnTeleport?.Invoke();

    }
}
