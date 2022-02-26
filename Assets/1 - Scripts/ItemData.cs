using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Custom/ItemData", fileName = "ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int quantity;
    public int harvestActionsRequired;
}