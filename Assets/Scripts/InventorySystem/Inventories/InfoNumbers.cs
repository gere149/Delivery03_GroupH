using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using System.Data.SqlTypes;

public class InfoNumbers : MonoBehaviour
{
    public ElementInfo elementInfo;
    public TextMeshProUGUI textMoney;

    private void OnEnable()
    {
        ElementInfo.OnMoneyChanged += MoneyText;

        if (elementInfo is PlayerInfo)
        {
            textMoney.text = $"Player Money: {elementInfo.Money}";
        }
        else
        {
            textMoney.text = $"Shop Money: {elementInfo.Money}";
        }
    }

    private void OnDisable()
    {
        ElementInfo.OnMoneyChanged -= MoneyText;
    }

    public void OnGetDamage()
    {
        (elementInfo as PlayerInfo).GetDamage();
    }

    private void MoneyText()
    {
        if (elementInfo is PlayerInfo)
        {
            textMoney.text = $"Player Money: {elementInfo.Money}";
        }
        else 
        {
            textMoney.text = $"Shop Money: {elementInfo.Money}";
        }
    }
}