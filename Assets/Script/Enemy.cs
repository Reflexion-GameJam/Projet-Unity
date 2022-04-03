using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;
    [SerializeField]
    private Teleporter teleporter;

    public static event Action OnLaugh;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PlayerTeleport.realWorld)
        {
            bubble.SetActive(true);
            OnLaugh?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bubble.SetActive(false);
        }
    }

    public void Attacked()
    {
        teleporter.Activate();
        Destroy(gameObject);
    }
}
