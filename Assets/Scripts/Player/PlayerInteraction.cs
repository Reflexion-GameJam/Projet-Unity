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

        if (Input.GetButtonUp("Attack")) // If the player presses the attack button
        {
            Debug.Log("attack");
            AttackEnemy(); // Attack the enemy
        }

        if (Input.GetButtonUp("Hide")) // If the player presses the hide button
        {
            Debug.Log("hide"); 
            DodgeEnemy(); // Dodge the enemy
        }
    }

    private void AttackEnemy() // Attack the enemy
    {
        GameManager.Instance.EnemyAttacked(); // Tell the game manager that the enemy was attacked
        enemy.GetComponent<Enemy>().Attacked(); // Tell the enemy that it was attacked
        enemy = null; // Reset the enemy
    }  

    private void DodgeEnemy() // Dodge the enemy
    {
        GameManager.Instance.EnemyDodged(); // Tell the game manager that the enemy was dodged
        enemy.GetComponent<Enemy>().Dodged(); // Tell the enemy that it was dodged
        enemy = null; // Reset the enemy
    }

    private void OnCollisionEnter2D(Collision2D collision) // When the player collides with an enemy
    {
        if (collision.gameObject.tag == "End") // If the player collides with the end of the level
        {
            GameManager.Instance.EndGame(); // Tell the game manager that the player won
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // When the player enters a trigger
    {
        if (collision.tag == "Enemy") // If the player enters a trigger with an enemy
        {
            enemy = collision.gameObject; // Set the enemy to the enemy that entered the trigger
            if (GameManager.Instance.currentWorld == World.REAL) // If the player is in the real world
            {
                GameManager.Instance.EnemyFound(); // Tell the game manager that the enemy was found
            }
            else
            {
                Destroy(enemy);  // Destroy the enemy
                GameManager.Instance.EnemyKilled(); // Tell the game manager that the enemy was killed
            }
        }
    }
}
