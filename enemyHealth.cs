using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth;
    public float currenthealth;
    void Start()
    {
        currenthealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
    }
}
