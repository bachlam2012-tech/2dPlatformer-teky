
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 spawnPoint;
    void Start()
    {
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void respawn()
    {
        transform.position = spawnPoint;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
