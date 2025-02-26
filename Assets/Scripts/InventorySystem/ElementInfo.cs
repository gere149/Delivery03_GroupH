using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementInfo", menuName = "ElementInfo/Element")]
public class ElementInfo : ScriptableObject
{
    public int Money;
    public int initialMoney = 100;
    public static Action OnMoneyChanged;

    private bool _isPlayer;
    private static PlayerInfo playerInfo;
    private static ElementInfo shopInfo;

    public void SetOwner(bool isPlayer)
    {
        _isPlayer = isPlayer;

        if (isPlayer)
        {
            playerInfo = this as PlayerInfo;
        }
        else
        {
            shopInfo = this;
        }
    }

    private void OnEnable()
    {
        ResetMoney();

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
        Money = initialMoney;
    }

    private void UpdateWhileBuyingMoney(ItemBase item)
    {
        if (playerInfo != null && shopInfo != null)
        {
            playerInfo.Money -= item.Cost;
            shopInfo.Money += item.Cost;

            OnMoneyChanged?.Invoke();
        }
    }

    private void UpdateWhileSellingMoney(ItemBase item)
    {
        if (playerInfo != null && shopInfo != null)
        {
            playerInfo.Money += item.Cost;
            shopInfo.Money -= item.Cost;

            OnMoneyChanged?.Invoke();
        }
    }

    private void UpdateWhileSellingMoneyWhenDrag(ItemBase item, InventoryUI inventory)
    {
        if (playerInfo != null && shopInfo != null)
        {
            GameObject inventoryOwner = inventory.gameObject;

            if (inventoryOwner.CompareTag("Player"))
            {
                playerInfo.Money += item.Cost;
                shopInfo.Money -= item.Cost;
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