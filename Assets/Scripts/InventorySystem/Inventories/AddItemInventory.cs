using UnityEngine;

public class AddItemInventory : MonoBehaviour, IPickUp
{
    public Inventory Inventory;

    public void PickUp(ItemBase item)
    {
        Inventory.AddItem(item);
    }
}