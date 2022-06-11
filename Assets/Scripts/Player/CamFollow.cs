using UnityEngine;

/// <summary>
/// Class to set on the Main Camera to follow a target (here the player)
/// </summary>
[RequireComponent(typeof(Camera))]
public class CamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player; // The target to follow
    [SerializeField]
    private float offsetY; // The offset on the Y axis
    [SerializeField]
    private float smooth; // The smoothness of the camera movement

    void Start()
    {
        // If "player" variable hasn't been assigned
        if (player == null)
        {
            player = GameObject.Find("Player").transform; // Assign it
        }
    }

    void OnEnable()
    {
        EventManager.OnTeleportPlayer += SwitchOffsetY; // Subscribe to the event
    }

    void OnDisable()
    {
        EventManager.OnTeleportPlayer -= SwitchOffsetY;  // Unsubscribe from the event
    }

    void Update()
    {
        // Move the camera to the player position + the offset Y axis (to follow the player) + the Z axis (to keep the camera at the same height) + the smoothness of the movement (to make the camera move smoothly) + the time since the last frame (to make the camera move smoothly) 
        transform.position = new Vector3 (Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * smooth),
                                        Mathf.Lerp(transform.position.y, player.position.y + offsetY, Time.deltaTime * smooth),
                                        transform.position.z); 
    }

    /// <summary>
    /// Called when the player is teleported from a world to another
    /// </summary>
    private void SwitchOffsetY()
    {
        offsetY = -offsetY; // Switch the offset Y axis
    }
}
