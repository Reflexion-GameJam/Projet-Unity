using UnityEngine;

/// <summary>
/// Class used to manage the player's interactions like collisions, triggers and gameplay decisions
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    // Reference to a close enemy
    private GameObject enemy = null;

    void Update()
    {
        // Do nothing if there isn't a close enemy
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
        GameManager.Instance.EnemyAttacked();
        enemy.GetComponent<Enemy>().Attacked();
        enemy = null;
    }

    private void DodgeEnemy()
    {
        GameManager.Instance.EnemyDodged();
        enemy.GetComponent<Enemy>().Dodged();
        enemy = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "End")
        {
            GameManager.Instance.EndGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemy = collision.gameObject;
            if (GameManager.Instance.currentWorld == World.REAL)
            {
                GameManager.Instance.EnemyFound();
            }
            else
            {
                Destroy(enemy);
                GameManager.Instance.EnemyKilled();
            }
        }
    }
}
