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
        if (!PlayerControler.canMove)
        {
            plAnim.SetBool("isWalk", false);
            return;
        }

        // Si le joueur se déplace
        if (Input.GetButton("Horizontal"))
        {
            plAnim.SetBool("isWalk", true);
        }
        else
        {
            plAnim.SetBool("isWalk", false);
        }

        if (plControl.isAlterne)
        {
            plAnim.SetBool("isAltern", true);
        }
        else
        {
            plAnim.SetBool("isAltern", false);
        }
    }
}
