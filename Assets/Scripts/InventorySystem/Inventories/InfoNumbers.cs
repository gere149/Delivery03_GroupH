using UnityEngine;
using TMPro;

public class InfoNumbers : MonoBehaviour
{
    public ElementInfo elementInfo;
    public TextMeshProUGUI textMoney;

    private void OnEnable()
    {
        ElementInfo.OnMoneyChanged += MoneyText;

        if (elementInfo is PlayerInfo)
        {
            textMoney.text = "" + elementInfo.Money;
        }
        else
        {
            textMoney.text = "" + elementInfo.Money;
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
            textMoney.text = "" + elementInfo.Money;
        }
        else 
        {
            textMoney.text = "" + elementInfo.Money;
        }
    }
}