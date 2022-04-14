using UnityEngine;

/// <summary>
/// Class to set on the Main Camera to follow a target (here the player)
/// </summary>
[RequireComponent(typeof(Camera))]
public class CamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float smooth;

    void Start()
    {
        // If "player" variable hasn't been assigned
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    void OnEnable()
    {
        EventManager.OnTeleportPlayer += SwitchOffsetY;
    }

    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= SwitchOffsetY;
    }

    void Update()
    {
        transform.position = new Vector3 (Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * smooth),
                                        Mathf.Lerp(transform.position.y, player.position.y + offsetY, Time.deltaTime * smooth),
                                        transform.position.z);
    }

    /// <summary>
    /// Called when the player is teleported from a world to another
    /// </summary>
    private void SwitchOffsetY()
    {
        offsetY = -offsetY;
    }
}
