using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Référence d'un ennemi s'il est proche
    private GameObject enemy = null;

    public static event Action OnAttack;

    void Update()
    {
        if (!enemy)
            return;

        if (Input.GetButtonUp("Attack"))
        {
            Debug.Log("attack");
            AttackEnemy();
            GameManager.Instance.EnemyAttacked();
        }

        if (Input.GetButtonUp("Hide"))
        {
            Debug.Log("hide");
        }
    }

    private void AttackEnemy()
    {
        Destroy(enemy);
        enemy = null;
        OnAttack?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("enemy found");
            enemy = collision.gameObject;
            PlayerControler.canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("enemy lost");
            enemy = null;
            PlayerControler.canMove = true;
        }
    }
}
