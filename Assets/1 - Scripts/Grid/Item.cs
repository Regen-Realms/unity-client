using System;
using UnityEngine;

public class Item : GridEntity, IInteractable
{
    public ItemData itemData;

    private string name;
    private Sprite sprite;
    private int harvestActionsRequired;
    private int quantity;
    
    private void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.sprite;
        name = itemData.name;
        quantity = itemData.quantity;
        harvestActionsRequired = itemData.harvestActionsRequired;
    }

    public void OnInteract(GridEntity gridEntity)
    {
        if (--harvestActionsRequired != 0) return;
        Debug.Log($"Picking up {name}!");
        Destroy(gridEntity.gameObject);
    }
}