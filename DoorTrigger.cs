using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Target Room Scene")]
    public string targetRoomScene;
    [Header("Spawn point Name In Target Room")]
    public string spawnPointName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.instance.LoadRoom(targetRoomScene, spawnPointName);
        }
    }
}