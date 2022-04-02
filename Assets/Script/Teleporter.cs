using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private GameObject linkedTeleporter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerTeleport>().Teleport(linkedTeleporter);
            StartCoroutine(SyncAnimPlayer(collision.gameObject));
        }
    }

    // Lancement de l'animation de teleportation
    private IEnumerator SyncAnimPlayer(GameObject player)
    {
        player.GetComponent<Animator>().SetBool("isTeleport", true);
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<Animator>().SetBool("isTeleport", false);
        yield break;
    }
}
