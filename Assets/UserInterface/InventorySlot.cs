using UnityEngine.UIElements;

public class InventorySlot : VisualElement
{
    public Image Icon;

    public InventorySlot()
    {
        Icon = new Image();
        Add(Icon);
        
        Icon.AddToClassList("InventorySlotIcon");
        AddToClassList("InventorySlotStyle");
    }
}