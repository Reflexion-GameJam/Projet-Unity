using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Reference d'un ennemi s'il est proche
    private GameObject enemy = null;

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
            DodgeEnemy();
        }
    }

    private void AttackEnemy()
    {
        GameManager.EnemyAttacked();
        enemy.GetComponent<Enemy>().Attacked();
        enemy = null;
    }

    private void DodgeEnemy()
    {
        GameManager.EnemyDodged();
        enemy.GetComponent<Enemy>().Dodged();
        enemy = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "End")
        {
            GameManager.EndGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = collision.gameObject;
            if (GameManager.currentWorld == World.REAL)
            {
                GameManager.EnemyFound();
            }
            else
            {
                Destroy(enemy);
                GameManager.EnemyKilled();
            }
        }
    }
}
