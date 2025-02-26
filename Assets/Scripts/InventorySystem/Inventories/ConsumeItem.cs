using System;
using UnityEngine;

public class ConsumeItem : MonoBehaviour, IConsume
{
    public static Action<ItemIngestible> OnConsumeItem;
    public void Use(ConsumableItem item)
    {
        if (item is ItemIngestible itemIngestible)
        {
            OnConsumeItem?.Invoke(itemIngestible);
        }
    }
}