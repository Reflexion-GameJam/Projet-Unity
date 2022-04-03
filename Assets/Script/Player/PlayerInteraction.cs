using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // R�f�rence d'un ennemi s'il est proche
    private GameObject enemy = null;
    //[SerializeField] private GameObject[] Teleporteur;

    public static event Action OnAttack;

    void Update()
    {
        if (!enemy)
            return;

        if (Input.GetButtonUp("Attack"))
        {
            Debug.Log("attack");
            AttackEnemy();
        }

        if (Input.GetButtonUp("Hide"))
        {
            Debug.Log("hide");
        }
    }

    private void AttackEnemy()
    {
        enemy.GetComponent<Enemy>().Attacked();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = collision.gameObject;
            if (!PlayerTeleport.realWorld)
            {
                enemy.GetComponent<Enemy>().Attacked();
                GameManager.Instance.KilledEnemy();
                OnAttack?.Invoke();
            }
            else
            {
                PlayerControler.canMove = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Ennemy exit");
            enemy = null;
            PlayerControler.canMove = true;
        }
    }
}
