using System;
using System.Collections;
using System.Collections.Generic;

public delegate void LanguageTextChange();

public class LanguageManager : ManagerBase
{
    public const int Korean = 0;
    public const int English = 1;
    public const int Japanese = 2;
    public const int SimplifiedChinese = 3; // 간체 = 중국
    public const int TraditionalChinese = 4; // 번체 = 대만,홍콩

    public static event LanguageTextChange OnLanguageTextChange;

    public static Dictionary<string, string> languageDictionary;

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        // SettingManager.LanguageChanged += SetLanguage;
        LanguageChange(SettingManager.CurrentLanguage);
        yield return null;
    }


    protected override void OnDisconnected()
    {

    }

    public static void SetLanguage(SettingManager.Language index)
    {
        switch(index)
        {
            case SettingManager.Language.Korean             : LanguageChange(Korean); break;
            case SettingManager.Language.English            : LanguageChange(English); break;
            case SettingManager.Language.Japanese           : LanguageChange(Japanese); break;
            case SettingManager.Language.TraditionalChinese : LanguageChange(SimplifiedChinese); break;
            case SettingManager.Language.SimplifiedChinese  : LanguageChange(TraditionalChinese); break;
        }
    }

    public static void LanguageChange(int index)
    {
        GameSetDisplayNameText(index);
        // Resetbutton();
        // AllResetButton();
    }

    public static string GetText(string key)
    {
        if (languageDictionary is null) return key;
        if (languageDictionary.TryGetValue(key, out string result))
        {
            return result;
        }
        else
        {
            return key;
        }
    }

    public static void GameSetDisplayNameText(int index)
    {
        switch (index)
        {
            case 0: // 한국어
                {
                    languageDictionary = LanguageDictionaries.koreanDictionary;
                }
                break;

            case 1: // 영어
                {
                    languageDictionary = LanguageDictionaries.englishDictionary;
                }
                break;

            case 2: // 일본어  
                {
                    languageDictionary = LanguageDictionaries.japaneseDictionary;
                }
                break;

            case 3: // 중국어 간체         
                {
                    languageDictionary = LanguageDictionaries.chineseDictionary_CH;
                }
                break;

            case 4: // 중국어 번체
                {
                    languageDictionary = LanguageDictionaries.chineseDictionary_TW;
                }
                break;
        }
        OnLanguageTextChange?.Invoke();
    }
}
