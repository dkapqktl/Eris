using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting_GameSet : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown LanguageDropdown;
    [SerializeField] private TextMeshProUGUI LanguageText;
    [SerializeField] private TextMeshProUGUI LanguageResetText;

    [SerializeField] private TMP_Dropdown AutoSaveToggle;
    [SerializeField] private TextMeshProUGUI AutoSaveText;
    [SerializeField] private TextMeshProUGUI AutoSaveResetText;

    [SerializeField] private TMP_Dropdown AutoSaveIntervalDropdown;
    [SerializeField] private TextMeshProUGUI AutoSaveIntervalText;
    [SerializeField] private TextMeshProUGUI AutoSaveIntervalResetText; 

    [SerializeField] private TMP_Dropdown MiniMapToggle;
    [SerializeField] private TextMeshProUGUI MiniMapText;
    [SerializeField] private TextMeshProUGUI MiniMapResetText;

    [SerializeField] private TMP_Dropdown TimeToggle;
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI TimeResetText;

    [SerializeField] private TMP_Dropdown AutoLootToggle;
    [SerializeField] private TextMeshProUGUI AutoLootText;
    [SerializeField] private TextMeshProUGUI AutoLootResetText;

    [SerializeField] private TMP_Dropdown CameraShakeToggle;
    [SerializeField] private TextMeshProUGUI CameraShakeText;
    [SerializeField] private TextMeshProUGUI CameraShakeResetText;



    private void Awake()
    {
        GameManager.OnInitializeManager += Initialize;
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




    public void ClaimMiniMapChange(int value)
    {
        SettingManager.OnSetShowMiniMap(value);
    }




    public void ClaimShowTimeChange(int value)
    {
        SettingManager.OnSetShowTime(value);
    }




    public void ClaimAutoLootChange(int value)
    {
        SettingManager.OnSetAutoLoot(value);
    }




    public void ClaimCameraShakeChange(int value)
    {
        SettingManager.OnSetCameraShake(value);
    }
}
