using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementInfo", menuName = "ElementInfo/Element")]
public class ElementInfo : ScriptableObject
{
    public int Money;

    public static Action OnMoneyChanged;

    //FALTA ACTIVACIÓN
}