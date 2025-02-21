using UnityEngine;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    private TMP_Dropdown languageDropdown;

    private void Start()
    {
        languageDropdown = GetComponent<TMP_Dropdown>(); 
        languageDropdown.value = (int)Localizer.GetCurrentLanguage() - 1;
        languageDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int selectedIndex)
    {
        Language selectedLanguage = (Language)selectedIndex + 1;
        Localizer.SetLanguage(selectedLanguage);
        Debug.Log("Idioma cambiado a: " + selectedLanguage);
    }
}