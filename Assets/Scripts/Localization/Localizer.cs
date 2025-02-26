using System;
using System.Collections.Generic;
using UnityEngine;

public class Localizer : MonoBehaviour
{
    public static Localizer Instance; // Singleton instance

    public TextAsset DataSheet; // CSV file with localization data

    private Dictionary<string, LanguageData> Data; // Stores text data from CSV

    private Language currentLanguage;
    public Language DefaultLanguage;

    public static Action OnLanguageChange; // Event triggered when language changes

    private const string LanguageKey = "SelectedLanguage"; // Key for PlayerPrefs

    private void Awake()
    {
        Instance = this;
        LoadLanguageSheet();
        LoadSavedLanguage();
    }

    public static string GetText(string textKey)
    {
        return Instance.Data.ContainsKey(textKey) ? Instance.Data[textKey].GetText(Instance.currentLanguage) : textKey;
    }

    public static void SetLanguage(Language language)
    {
        Instance.currentLanguage = language;
        PlayerPrefs.SetInt(LanguageKey, (int)language); // Save language selection
        PlayerPrefs.Save();

        OnLanguageChange?.Invoke();
    }

    private void LoadSavedLanguage()
    {
        if (PlayerPrefs.HasKey(LanguageKey))
        {
            currentLanguage = (Language)PlayerPrefs.GetInt(LanguageKey);
        }
        else
        {
            currentLanguage = DefaultLanguage; // Fallback to default
        }
    }

    void LoadLanguageSheet()
    {
        string[] lines = DataSheet.text.Split(new char[] { '\n' });

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines.Length > 1) AddLanguageData(lines[i]);
        }
    }

    void AddLanguageData(string str)
    {
        string[] entry = str.Split(new char[] { ',' });

        var languageData = new LanguageData(entry);

        if (Data == null) Data = new Dictionary<string, LanguageData>();

        Data.Add(entry[0], languageData);
    }

    public static Language GetCurrentLanguage()
    {
        return Instance.currentLanguage;
    }
}