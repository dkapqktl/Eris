using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Setting_GameSet : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown    LanguageDropdown;
    [SerializeField] private TextMeshProUGUI LanguageDisplayText;

    [SerializeField] private TMP_Dropdown    AutoSaveDropdown;
    [SerializeField] private TextMeshProUGUI AutoSaveDisplayText;

    [SerializeField] private TMP_Dropdown    AutoSaveIntervalDropdown;
    [SerializeField] private TextMeshProUGUI AutoSaveIntervalDisplayText;

    [SerializeField] private TMP_Dropdown    MiniMapDropdown;
    [SerializeField] private TextMeshProUGUI MiniMapDisplayText;

    [SerializeField] private TMP_Dropdown    TimeDropdown;
    [SerializeField] private TextMeshProUGUI TimeDisplayText;

    [SerializeField] private TMP_Dropdown    AutoLooDropdown;
    [SerializeField] private TextMeshProUGUI AutoLootDisplayText;

    [SerializeField] private TMP_Dropdown    CameraShakeDropdown;
    [SerializeField] private TextMeshProUGUI CameraShakeDisplayText;

    public TextMeshProUGUI[] ResetButtonText;


    private int languageDropdownValue = SettingManager.CurrentLanguage;
    private int currentLanguage = SettingManager.CurrentLanguage;


    private void Start()
    {
        GameManager.OnInitializeManager += Initialize;

        LanguageManager.OnLanguageTextChange += OnLanguageChange;
    }

    private void Initialize()
    {
        LanguageDropdown.value = SettingManager.CurrentLanguage;
    }



    public void ClaimLanguageChange(int value)
    {
        SettingManager.OnSetLanguage(value);
    }
    public void ClaimLanguageReset()
    {
        SettingManager.LanguageReset();
    }
    public void OnLanguageChange()
    {
        LanguageDropdown.value = languageDropdownValue;
        
        LanguageDisplayText.text = LanguageManager.GetText("LanguageSetting");
        AutoLootDisplayText.text = LanguageManager.GetText("AutoSave");
        AutoSaveIntervalDisplayText.text = LanguageManager.GetText("AutosaveInterval");
        MiniMapDisplayText.text = LanguageManager.GetText("ShowMiniMap");
        TimeDisplayText.text = LanguageManager.GetText("ShowTime");
        AutoLootDisplayText.text = LanguageManager.GetText("AutoLooting");
        CameraShakeDisplayText.text = LanguageManager.GetText("CameraShake");

        for (int i = 0; i < ResetButtonText.Length; i++)
        {
            ResetButtonText[i].text = LanguageManager.GetText("Reset");
        }

        // LanguageText
        // LanguageResetText = LanguageManager.ResetButtonText(index);
    }




    public void ClaimAutoSaveChange(int value)
    {
        SettingManager.OnSetAutoSave(value);
    }
    public void ClaimAutoSaveReset()
    {
        SettingManager.AutoSaveReset();
    }
    public void OnAutoSaveChange()
    {

    }




    public void ClaimAutoSaveIntervalChange(int value)
    {
        SettingManager.OnSetAutoSaveInterval(value);
    }
    public void ClaimAutoSaveIntervalReset()
    {

    }
    public void OnAutoSaveIntervaChange()
    {

    }




    public void ClaimMiniMapChange(int value)
    {
        SettingManager.OnSetShowMiniMap(value);
    }
    public void ClaimMiniMapReset()
    {

    }
    public void OnMiniMapChange()
    {

    }




    public void ClaimShowTimeChange(int value)
    {
        SettingManager.OnSetShowTime(value);
    }
    public void ClaimShowTimeReset()
    {

    }
    public void OnShowTimeChange()
    {

    }




    public void ClaimAutoLootChange(int value)
    {
        SettingManager.OnSetAutoLoot(value);
    }
    public void ClaimAutoLootReset()
    {

    }
    public void OnAutoLootChange()
    {

    }




    public void ClaimCameraShakeChange(int value)
    {
        SettingManager.OnSetCameraShake(value);
    }
    public void ClaimCameraShakeReset()
    {

    }
    public void OnCameraShakeChange()
    {

    }


}
