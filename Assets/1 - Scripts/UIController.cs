using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Inventory inventory;
    public VisualElement inventoryBar;

    public IList<InventorySlot> inventorySlots = new List<InventorySlot>();
    
    // Start is called before the first frame update
    void Start()
    {
        inventory.OnInventoryChange += OnInventoryChange;
        
        var root = GetComponent<UIDocument>().rootVisualElement;
        inventoryBar = root.Q<VisualElement>("InventoryBar");
        for (var i = 0; i < 10; i++)
        {
            var inventorySlot = new InventorySlot();
            inventorySlots.Add(inventorySlot);
            inventoryBar.Add(inventorySlot);
        }
    }

    void OnInventoryChange()
    {
        foreach (var slot in inventory.GetItems().Select((value, i) => new { i, value}))
        {
            inventorySlots[slot.i].Icon.sprite = slot.value.sprite;
        }     
    }

    // Update is called once per frame
    void Update()
    {

    }
}