using System;
using UnityEngine;

public class ItemGridEntity : GridEntity, IInteractable
{
    public ItemData itemData;
    public Inventory inventory;

    private string itemName;
    private Sprite sprite;
    private int quantity;
    
    private void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.sprite;
        itemName = itemData.name;
        quantity = itemData.quantity;
    }

    public void OnInteract(GridEntity gridEntity)
    {
       // if (--harvestActionsRequired != 0) return;
        Debug.Log($"Picking up {itemName}!");
        Destroy(gridEntity.gameObject);
        inventory.AddItem(new Item() { name = itemName, sprite = itemData.sprite });
    }
}