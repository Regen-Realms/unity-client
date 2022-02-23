using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/ItemData", fileName = "ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public int quantity;
    public int harvestActionsRequired;
}