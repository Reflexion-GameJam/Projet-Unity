using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator plAnim;
    [SerializeField] private PlayerControler plControl;

    private void Start()
    {
        // Si animator non assigné
        if (plAnim == null)
        {
            plAnim = this.gameObject.GetComponent<Animator>();
        }
        
        // Si PlayerControler non assigné
        if (plControl == null)
        {
            plControl = this.gameObject.GetComponentInParent<PlayerControler>();
        }
    }

    private void Update()
    {
        // Si le joueur ce déplace
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetButtonDown("Horizontal"))
        {
            plAnim.SetBool("isWalk", true);
        }
        else
        {
            plAnim.SetBool("isWalk", false);
        }
    }
}
