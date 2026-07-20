
using System.Collections;
using UnityEditor;
using UnityEngine;

public class LanguageManager : ManagerBase
{
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        // SettingManager.LanguageChanged += SettingManager_LanguageChanged;
         yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    public void SetLanguage(string languageCode)
    {
        // Set the language in PlayerPrefs
        PlayerPrefs.SetString("Language", languageCode);
        PlayerPrefs.Save();
        // Optionally, you can also trigger a method to update the UI or reload the scene
        UpdateLanguageUI();
    }

    private void UpdateLanguageUI()
    {
        // Implement your UI update logic here

    }

    public static void KoreanLanguage()
    {

    }
}
