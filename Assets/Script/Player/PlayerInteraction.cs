using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject enemy = null;

    void Update()
    {
        if (!enemy)
            return;

        if (Input.GetKeyUp(KeyCode.E))
        {
            Destroy(enemy);
            enemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("enemy found");
            enemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("enemy lost");
            enemy = null;
        }
    }
}
