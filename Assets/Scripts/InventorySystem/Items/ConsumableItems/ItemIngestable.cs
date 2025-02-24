using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Ingestible")]
public class ItemIngestible : ConsumableItem
{
    public int HealthPoints;

    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}