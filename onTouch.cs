using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTouch : MonoBehaviour
{
    // Start is called before the first frame update
    public int point;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            point = point + 1;
        }

    }
}
