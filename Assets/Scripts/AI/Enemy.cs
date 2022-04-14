using UnityEngine;

/// <summary>
/// Class used to define the enemy behavior
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.Instance.currentWorld == World.REAL)
        {
            bubble.SetActive(true);
            EventManager.InvokeEnemyLaugh();
        }
    }

    /// <summary>
    /// Called when the player attacks
    /// </summary>
    public void Attacked()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Called when the player dodges
    /// </summary>
    public void Dodged()
    {
        // To change
        Destroy(gameObject);
    }
}
