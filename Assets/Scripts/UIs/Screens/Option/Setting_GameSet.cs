using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting_GameSet : MonoBehaviour
{


    public static bool AutoSave { get; private set; }
    public static int AutoSaveInterval { get; private set; }

    public static bool ShowMiniMap { get; private set; }
    public static bool ShowTime { get; private set; }
    public static bool AutoLoot { get; private set; }
    public static bool CameraShake { get; private set; }

    public static int Language { get; private set; }


    [SerializeField] public static TMP_Dropdown LanguageDropdown;

    [SerializeField] public static Toggle AutoSaveToggle;
    [SerializeField] public static TextMeshProUGUI AutoSaveText;

    [SerializeField] public static TMP_Dropdown AutoSaveIntervalDropdown;
    
    [SerializeField] public static Toggle MiniMapToggle;
    [SerializeField] public static TextMeshProUGUI MiniMapText;

    [SerializeField] public static Toggle TimeToggle;
    [SerializeField] public static TextMeshProUGUI TimeText;

    [SerializeField] public static Toggle AutoLootToggle;
    [SerializeField] public static TextMeshProUGUI AutoLootText;

    [SerializeField] public static Toggle CameraShakeToggle;
    [SerializeField] public static TextMeshProUGUI CameraShakeText;

    private void Awake()
    {
        LoadSettings();
    }

    public static void LoadSettings()
    {
        Language = PlayerPrefs.GetInt("Language", 0);

        AutoSave = PlayerPrefs.GetInt("AutoSave", 1) == 1;
        AutoSaveInterval = PlayerPrefs.GetInt("AutoSaveInterval", 5);

        ShowMiniMap = PlayerPrefs.GetInt("ShowMiniMap", 1) == 1;
        ShowTime = PlayerPrefs.GetInt("ShowTime", 1) == 1;
        AutoLoot = PlayerPrefs.GetInt("AutoLoot", 1) == 1;
        CameraShake = PlayerPrefs.GetInt("CameraShake", 1) == 1;
    }

    public void OnSetLanguage(int value)
    {
        Language = value;

        PlayerPrefs.SetInt("Language", value);
        PlayerPrefs.Save();
    }

    public void OnSetAutoSave(bool value)
    {
        AutoSave = value;

        PlayerPrefs.SetInt("AutoSave", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnSetAutoSaveInterval(int value)
    {
        AutoSaveInterval = value;

        PlayerPrefs.SetInt("AutoSaveInterval", value);
        PlayerPrefs.Save();
    }

    public static float GetAutoSaveSeconds()
    {
        return AutoSaveInterval switch
        {
            0 => 60f,
            1 => 300f,
            2 => 600f,
            3 => 900f,
            4 => 1800f,
            _ => 300f
        };
    }

    public void OnSetMiniMap(bool value)
    {
        ShowMiniMap = value;

        PlayerPrefs.SetInt("ShowMiniMap", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnSetShowTime(bool value)
    {
        ShowTime = value;

        PlayerPrefs.SetInt("ShowTime", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnSetAutoLoot(bool value)
    {
        AutoLoot = value;

        PlayerPrefs.SetInt("AutoLoot", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnSetCameraShake(bool value)
    {
        CameraShake = value;

        PlayerPrefs.SetInt("CameraShake", value ? 1 : 0);
        PlayerPrefs.Save();
    }
}
