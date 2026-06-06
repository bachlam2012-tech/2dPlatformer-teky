using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistent : MonoBehaviour
{
    private static PlayerPersistent instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        if (RoomManager.instance != null)
        {
            RoomManager.instance.setPlayer(transform);
        }
    }
}
