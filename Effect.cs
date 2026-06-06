using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject effect;
    public AudioClip sound;
    public AudioSource source;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.currentHealth += 10;
            Instantiate(effect, transform.position, Quaternion.identity);
            // source.PlayOneShot(sound);
            AudioSource.PlayClipAtPoint(sound, transform.position);
            Destroy(gameObject);


        }
    }
}

