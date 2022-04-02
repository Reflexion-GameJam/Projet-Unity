using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject linkedTeleporter;

    public static event Action OnTeleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerTeleport>().Teleport(linkedTeleporter);
            OnTeleport?.Invoke();
        }
    }
}
