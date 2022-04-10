using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject bubble;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.currentWorld == World.REAL)
        {
            bubble.SetActive(true);
            EventManager.InvokeEnemyLaugh();
        }
    }

    public void Attacked()
    {
        Destroy(gameObject);
    }

    public void Dodged()
    {
        Destroy(gameObject);
    }
}
