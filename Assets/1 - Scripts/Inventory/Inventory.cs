using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InventorySystem", menuName = "Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    public List<Item> Items;

    public UnityAction OnInventoryChange;

    public void OnEnable()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
        OnInventoryChange?.Invoke();
    }
    
    public IList<Item> GetItems()
    {
        return Items;
    }
}