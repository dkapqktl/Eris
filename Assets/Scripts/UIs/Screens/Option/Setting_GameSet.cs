using TMPro;
using UnityEngine;

public class Setting_GameSet : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown    LanguageDropdown;
    [SerializeField] private TextMeshProUGUI LanguageText;
    [SerializeField] private TextMeshProUGUI LanguageResetText;

    [SerializeField] private TMP_Dropdown    AutoSaveDropdown;
    [SerializeField] private TextMeshProUGUI AutoSaveText;
    [SerializeField] private TextMeshProUGUI AutoSaveResetText;

    [SerializeField] private TMP_Dropdown    AutoSaveIntervalDropdown;
    [SerializeField] private TextMeshProUGUI AutoSaveIntervalText;
    [SerializeField] private TextMeshProUGUI AutoSaveIntervalResetText; 

    [SerializeField] private TMP_Dropdown    MiniMapDropdown;
    [SerializeField] private TextMeshProUGUI MiniMapText;
    [SerializeField] private TextMeshProUGUI MiniMapResetText;

    [SerializeField] private TMP_Dropdown    TimeDropdown;
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI TimeResetText;

    [SerializeField] private TMP_Dropdown    AutoLooDropdown;
    [SerializeField] private TextMeshProUGUI AutoLootText;
    [SerializeField] private TextMeshProUGUI AutoLootResetText;

    [SerializeField] private TMP_Dropdown    CameraShakeDropdown;
    [SerializeField] private TextMeshProUGUI CameraShakeText;
    [SerializeField] private TextMeshProUGUI CameraShakeResetText;



    private void Awake()
    {
        GameManager.OnInitializeManager += Initialize;

        SettingManager.LanguageChanged += OnLanguageChange;
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
    public void OnLanguageChange(int index)
    {
        LanguageDropdown.value = index;
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
