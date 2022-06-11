using UnityEngine;

/// <summary>
/// Class used to define the enemy behavior
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] 
    private GameObject bubble; // The bubble prefab

    private void OnTriggerEnter2D(Collider2D collision) // When the enemy collides with something
    {
        if (collision.tag == "Player" && GameManager.Instance.currentWorld == World.REAL) // If the player is in the real world
        {
            bubble.SetActive(true); // Activate the bubble
            EventManager.InvokeEnemyLaugh(); // Play the enemy laugh sound
        }
    }

    /// <summary>
    /// Called when the player attacks
    /// </summary>
    public void Attacked() 
    {
        Destroy(gameObject); // Destroy the enemy
    }

    /// <summary>
    /// Called when the player dodges
    /// </summary>
    public void Dodged()
    {
        // To change
        Destroy(gameObject); // Destroy the enemy
    }
}
