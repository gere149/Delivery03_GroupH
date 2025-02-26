using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementInfo", menuName = "ElementInfo/Element")]
public class ElementInfo : ScriptableObject
{
    public int Money;
    public int initialMoney = 100; // Ahora editable desde el Inspector
    public static Action OnMoneyChanged;

    private bool _isPlayer;
    private static PlayerInfo playerInfo;
    private static ElementInfo shopInfo;

    public void SetOwner(bool isPlayer)
    {
        _isPlayer = isPlayer;

        if (isPlayer)
        {
            playerInfo = this as PlayerInfo; // Asegurar que sea del tipo correcto
        }
        else
        {
            shopInfo = this;
        }
    }

    private void OnEnable()
    {
        ResetMoney(); // Restablecer dinero al valor inicial al habilitarse

        InventorySlotUI.OnBuyingItem += UpdateWhileBuyingMoney;
        InventorySlotUI.OnSellingItem += UpdateWhileSellingMoney;
        InventorySlotUI.OnSellingItemWhenDrag += UpdateWhileSellingMoneyWhenDrag;
    }

    private void OnDisable()
    {
        InventorySlotUI.OnBuyingItem -= UpdateWhileBuyingMoney;
        InventorySlotUI.OnSellingItem -= UpdateWhileSellingMoney;
        InventorySlotUI.OnSellingItemWhenDrag -= UpdateWhileSellingMoneyWhenDrag;
    }

    public void ResetMoney()
    {
        Money = initialMoney; // Usa el valor asignado en el Inspector
    }

    private void UpdateWhileBuyingMoney(ItemBase item)
    {
        if (playerInfo != null && shopInfo != null)
        {
            Debug.Log($"Antes de compra: PlayerMoney={playerInfo.Money}, ShopMoney={shopInfo.Money}");

            playerInfo.Money -= item.Cost;  // Restar al jugador
            shopInfo.Money += item.Cost;    // Sumar a la tienda

            Debug.Log($"Después de compra: PlayerMoney={playerInfo.Money}, ShopMoney={shopInfo.Money}");

            OnMoneyChanged?.Invoke();
        }
    }

    private void UpdateWhileSellingMoney(ItemBase item)
    {
        if (playerInfo != null && shopInfo != null)
        {
            Debug.Log($"Antes de venta: PlayerMoney={playerInfo.Money}, ShopMoney={shopInfo.Money}");

            playerInfo.Money += item.Cost;  // Sumar al jugador
            shopInfo.Money -= item.Cost;    // Restar a la tienda

            Debug.Log($"Después de venta: PlayerMoney={playerInfo.Money}, ShopMoney={shopInfo.Money}");

            OnMoneyChanged?.Invoke();
        }
    }

    private void UpdateWhileSellingMoneyWhenDrag(ItemBase item, InventoryUI inventory)
    {
        if (playerInfo != null && shopInfo != null)
        {
            GameObject inventoryOwner = inventory.gameObject; // Obtenemos el dueño del inventario

            if (inventoryOwner.CompareTag("Player"))
            {
                // El jugador está vendiendo un objeto a la tienda

                playerInfo.Money += item.Cost;  // Sumar al jugador
                shopInfo.Money -= item.Cost;    // Restar a la tienda
            }
            else if (inventoryOwner.CompareTag("Shop"))
            {
                playerInfo.Money -= item.Cost;
                shopInfo.Money += item.Cost;
            }

            OnMoneyChanged?.Invoke();
        }
    }
}
