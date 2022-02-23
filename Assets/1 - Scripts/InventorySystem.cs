using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySystem", menuName = "Inventory", order = 0)]
public class InventorySystem : ScriptableObject
{
    private List<GridEntity> Items { get; set; }

    public void Awake()
    {
        Items = new List<GridEntity>();
    }

    public void AddItem(GridEntity item)
    {
        Items.Add(item);
    }
    
    public IList<GridEntity> GetItems()
    {
        return Items;
    }
}