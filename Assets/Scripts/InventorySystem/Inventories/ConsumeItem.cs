using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItem : MonoBehaviour, IConsume
{
    public void Use(ConsumableItem item)
    {
        if (item is ItemIngestible)
        {
            Debug.Log("Health potion consumed!"); //PONER OBSERVER
        }
    }
}
