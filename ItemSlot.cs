using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    //------------------ ITEM DATA-------------------- //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public string itemDescription;
    public bool isFull;
    // ITEM SLOT------------//
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;
    // --------------ITEM DESCRIPTION----------------//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;
    public int maxNumberOfItem;
    private InventoryManager inventoryManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnRightClick()
    {
        throw new NotImplementedException();
    }

    private void OnLeftClick()
    {
        inventoryManager.DeselectedAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        itemDescriptionImage.sprite = itemSprite;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
    }

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        if (isFull)
            return quantity;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItem)
        {
            quantityText.text = maxNumberOfItem.ToString();
            quantityText.enabled = true;
            isFull = true;

            int extraItems = this.quantity - maxNumberOfItem;
            this.quantity = maxNumberOfItem;
            return extraItems;
        }
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;
    }
}
