using TMPro; 
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    public string TextKey;
    private TMP_Text _textValue;  

    void Start()
    {
        _textValue = GetComponent<TMP_Text>(); 
        _textValue.text = Localizer.GetText(TextKey);
    }

    private void OnEnable()
    {
        Localizer.OnLanguageChange += ChangeLanguage;
    }

    private void OnDisable()
    {
        Localizer.OnLanguageChange -= ChangeLanguage;
    }

    private void ChangeLanguage()
    {
        _textValue.text = Localizer.GetText(TextKey);  
    }
}