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
        if (collision.tag == "Player")
        {
            Debug.Log("teleporter");
            StartCoroutine(SyncAnimPlayer(collision.gameObject));
        }
    }

    // Lancement de l'animation de teleportation
    private IEnumerator SyncAnimPlayer(GameObject player)
    {
        player.GetComponent<Animator>().SetBool("isTeleport", true);
        yield return new WaitForSeconds(0.2f);
        player.GetComponent<Animator>().SetBool("isTeleport", false);
        player.GetComponent<PlayerTeleport>().Teleport();
        Destroy(gameObject);
        yield break;
    }

    public void Activate()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
