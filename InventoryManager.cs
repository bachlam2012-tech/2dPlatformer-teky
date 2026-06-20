using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool CanOpen;
    public GameObject UI;
    public ItemSlot[] itemSlots;
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

    public void DeselectedAllSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].selectedShader.SetActive(false);
            itemSlots[i].thisItemSelected = false;
        }
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if ((itemSlots[i].isFull == false && itemSlots[i].itemName == itemName) || itemSlots[i].quantity == 0)
            {
                int leftOverItems = itemSlots[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                return leftOverItems;
            }
        }
        return quantity;
    }
}
