using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float smooth;

    void Start()
    {
        // Si le joueur n'est pas assigné
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    void OnEnable()
    {
        EventManager.OnTeleportPlayer += SwitchOffsetY;
    }

    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= SwitchOffsetY;
    }

    void Update()
    {
        // La caméra suit le joueur avec la position de décalage spécifiée
        transform.position = new Vector3 (Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * smooth),
                                        Mathf.Lerp(transform.position.y, player.position.y + offsetY, Time.deltaTime * smooth),
                                        transform.position.z);
    }

    private void SwitchOffsetY()
    {
        offsetY = -offsetY;
    }
}
