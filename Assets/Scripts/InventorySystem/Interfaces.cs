// SOLID: Interfaces

// Interface for pickable item, implements PickUp()
public interface IPickUp 
{
    void PickUp(ItemBase item);
}

// Interface for consumable item, implements Use()
public interface IConsume
{
    void Use(ConsumableItem item);
}