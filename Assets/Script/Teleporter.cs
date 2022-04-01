using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject linkedTeleporter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerTeleport>().Teleport(linkedTeleporter);
        }
    }
}
