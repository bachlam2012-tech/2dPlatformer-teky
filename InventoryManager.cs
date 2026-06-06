using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool CanOpen;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------------------------------------------
        // if (Input.GetKeyDown(KeyCode.Tab) && CanOpen == true)
        // {
        //     UI.SetActive(true);
        // }
        // -------------------------------------------------------------
        if (Input.GetButtonDown("Inventory") && CanOpen)
        {
            Time.timeScale = 1;
            UI.SetActive(false);
            CanOpen = false;
        }
        else if (Input.GetButtonDown("Inventory") && !CanOpen)
        {
            Time.timeScale = 0;
            UI.SetActive(true);
            CanOpen = true;
        }
    }
    public void AddItem(string itemName, int quantity, Sprite sprite)
    {
        Debug.Log(itemName + quantity + sprite);
    }
}
