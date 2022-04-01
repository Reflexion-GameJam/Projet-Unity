using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public PlayerControler plControl;
    public Vector3 offset;
    public float Smooth;
    private void Start()
    {
        // Si le joueur n'est pas assigné
        if (plControl == null)
        {
            plControl = GameObject.FindWithTag("Player").GetComponent<PlayerControler>();
        }
    }

    private void Update()
    {
        // La caméra suit le joueur avec la position de décalage spécifiée
        this.transform.position = new Vector3 (Mathf.Lerp(this.transform.position.x, plControl.transform.position.x, Time.deltaTime*Smooth), Mathf.Lerp(this.transform.position.y, plControl.transform.position.y, Time.deltaTime*Smooth),offset.z);
    }
}
