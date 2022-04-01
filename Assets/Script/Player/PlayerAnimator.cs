using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerControler))]
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
            plControl = this.gameObject.GetComponent<PlayerControler>();
        }
    }

    private void Update()
    {
        // Si le joueur saute
        if (plControl.canJump)
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
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
            {
                plAnim.SetBool("isJump", true);
            }
            else
            {
                plAnim.SetBool("isJump", false);
            }
            
            plAnim.SetBool("isFall", false);
        }
        else
        {
            // Si le joueur ne touche pas le sol
            plAnim.SetBool("isWalk", false);
            plAnim.SetBool("isJump", false);
            plAnim.SetBool("isFall", true);
        }
    }
}