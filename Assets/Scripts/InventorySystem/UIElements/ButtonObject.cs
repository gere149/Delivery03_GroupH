using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonObject : MonoBehaviour
{
    private Button myButton;

    private void Start()
    {
        myButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        InventorySlotUI.OnItemSelected += SetButtonState;
    }

    private void OnDisable()
    {
        InventorySlotUI.OnItemSelected -= SetButtonState;
    }

    public void SetButtonState(bool isAnyItemSlected)
    {
        if (isAnyItemSlected)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }
    }
}