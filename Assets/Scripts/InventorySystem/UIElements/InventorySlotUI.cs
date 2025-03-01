﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image Image;
    public TextMeshProUGUI AmountText;

    private Canvas _canvas;
    private Transform _parent;
    private ItemBase _item;
    private InventoryUI _inventory;
    private static InventorySlotUI selectedSlot;

    private Color defaultColor;
    private Color selectedColor = new Color(1f, 1f, 0.5f, 1f);

    public static Action<ItemBase> OnBuyingItem;
    public static Action<ItemBase> OnSellingItem;
    public static Action<ItemBase, InventoryUI> OnSellingItemWhenDrag;

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        Image.SetNativeSize();

        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = slot.Amount > 1;

        _item = slot.Item;
        _inventory = inventory;

        defaultColor = Image.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parent = transform.parent;
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);

        if (!_canvas)
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        transform.SetParent(_canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster raycaster = GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        raycaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject otherObject = result.gameObject;

            if (otherObject != gameObject && otherObject.tag != this.gameObject.tag)
            {
                var pickUpComponent = otherObject.GetComponent<IPickUp>();
                if (pickUpComponent != null)
                {
                    OnSellingItemWhenDrag?.Invoke(selectedSlot._item, _inventory);

                    pickUpComponent.PickUp(_item);
                    _inventory.Inventory.RemoveItem(_item);
                }
            }
        }

        transform.SetParent(_parent.transform);
        transform.localPosition = Vector3.zero;
    }

    public void OnBuyItem()
    {
        GameObject shopObject = GameObject.FindGameObjectWithTag("Player");
        if (shopObject != null && shopObject.tag != this.gameObject.tag)
        {
            IPickUp pickUpComponent = shopObject.GetComponent<IPickUp>();

            if (pickUpComponent != null && selectedSlot != null)
            {
                OnBuyingItem?.Invoke(selectedSlot._item);

                pickUpComponent.PickUp(selectedSlot._item);
                selectedSlot._inventory.Inventory.RemoveItem(selectedSlot._item);
            }
        }
    }

    public void OnSellItem()
    {
        GameObject shopObject = GameObject.FindGameObjectWithTag("Shop");
        if (shopObject != null && shopObject.tag != this.gameObject.tag)
        {
            IPickUp pickUpComponent = shopObject.GetComponent<IPickUp>();

            if (pickUpComponent != null && selectedSlot != null)
            {
                OnSellingItem?.Invoke(selectedSlot._item);

                pickUpComponent.PickUp(selectedSlot._item);
                selectedSlot._inventory.Inventory.RemoveItem(selectedSlot._item);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectedSlot != null)
        {
            selectedSlot.Image.color = selectedSlot.defaultColor;
        }

        selectedSlot = this;
        Image.color = selectedColor;
    }

    public static void OnConsume()
    {
        if (selectedSlot != null && selectedSlot._item is ConsumableItem)
        {
            var consumer = selectedSlot._inventory.GetComponent<IConsume>();
            if (consumer != null)
            {
                (selectedSlot._item as ConsumableItem).Use(consumer);
                selectedSlot._inventory.UseItem(selectedSlot._item);

                selectedSlot.Image.color = selectedSlot.defaultColor;
                selectedSlot = null;
            }
        }
    }
}