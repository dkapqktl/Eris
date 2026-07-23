using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Setting_GameSet : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown    LanguageDropdown;
    [SerializeField] private TextMeshProUGUI LanguageText;
    [SerializeField] private TextMeshProUGUI LanguageResetText;

    [SerializeField] private TMP_Dropdown    AutoSaveDropdownKR;
    [SerializeField] private TMP_Dropdown    AutoSaveDropdownEN;
    [SerializeField] private TMP_Dropdown    AutoSaveDropdownJN;
    [SerializeField] private TMP_Dropdown    AutoSaveDropdownCNS;
    [SerializeField] private TMP_Dropdown    AutoSaveDropdownCNT;
    [SerializeField] private TextMeshProUGUI AutoSaveText;
    [SerializeField] private TextMeshProUGUI AutoSaveResetText;

    [SerializeField] private TMP_Dropdown    AutoSaveIntervalDropdownKR;
    [SerializeField] private TMP_Dropdown    AutoSaveIntervalDropdownEN;
    [SerializeField] private TMP_Dropdown    AutoSaveIntervalDropdownJN;
    [SerializeField] private TMP_Dropdown    AutoSaveIntervalDropdownCNS;
    [SerializeField] private TMP_Dropdown    AutoSaveIntervalDropdownCNT;
    [SerializeField] private TextMeshProUGUI AutoSaveIntervalText;
    [SerializeField] private TextMeshProUGUI AutoSaveIntervalResetText; 

    [SerializeField] private TMP_Dropdown    MiniMapDropdownKR;
    [SerializeField] private TMP_Dropdown    MiniMapDropdownEN;
    [SerializeField] private TMP_Dropdown    MiniMapDropdownJN;
    [SerializeField] private TMP_Dropdown    MiniMapDropdownCNS;
    [SerializeField] private TMP_Dropdown    MiniMapDropdownCNT;
    [SerializeField] private TextMeshProUGUI MiniMapText;
    [SerializeField] private TextMeshProUGUI MiniMapResetText;

    [SerializeField] private TMP_Dropdown    TimeDropdownKR;
    [SerializeField] private TMP_Dropdown    TimeDropdownEN;
    [SerializeField] private TMP_Dropdown    TimeDropdownJN;
    [SerializeField] private TMP_Dropdown    TimeDropdownCNS;
    [SerializeField] private TMP_Dropdown    TimeDropdownCNT;
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private TextMeshProUGUI TimeResetText;

    [SerializeField] private TMP_Dropdown    AutoLooDropdownKR;
    [SerializeField] private TMP_Dropdown    AutoLooDropdownEN;
    [SerializeField] private TMP_Dropdown    AutoLooDropdownJN;
    [SerializeField] private TMP_Dropdown    AutoLooDropdownCNS;
    [SerializeField] private TMP_Dropdown    AutoLooDropdownCNT;
    [SerializeField] private TextMeshProUGUI AutoLootText;
    [SerializeField] private TextMeshProUGUI AutoLootResetText;

    [SerializeField] private TMP_Dropdown    CameraShakeDropdownKR;
    [SerializeField] private TMP_Dropdown    CameraShakeDropdownEN;
    [SerializeField] private TMP_Dropdown    CameraShakeDropdownJN;
    [SerializeField] private TMP_Dropdown    CameraShakeDropdownCNS;
    [SerializeField] private TMP_Dropdown    CameraShakeDropdownCNT;
    [SerializeField] private TextMeshProUGUI CameraShakeText;
    [SerializeField] private TextMeshProUGUI CameraShakeResetText;

    public TextMeshProUGUI[] ResetButton;


    public int languageDropdownValue = SettingManager.CurrentLanguage;
    public int currentLanguage = SettingManager.CurrentLanguage;


    private void Awake()
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
        LanguageText.text = LanguageManager.GetText("LanguageSetting");
        AutoLootText.text = LanguageManager.GetText("AutoSave");
        AutoSaveIntervalText.text = LanguageManager.GetText("AutosaveInterval");
        MiniMapText.text = LanguageManager.GetText("ShowMiniMap");
        TimeText.text = LanguageManager.GetText("ShowTime");
        AutoLootText.text = LanguageManager.GetText("AutoLooting");
        CameraShakeText.text = LanguageManager.GetText("CameraShake");

        for(int i = 0; i < ResetButton.Length; i++)
        {
            ResetButton[i].text = LanguageManager.GetText("Reset");
        }

        DropdownChange(currentLanguage);
        // LanguageText
        // LanguageResetText = LanguageManager.ResetButtonText(index);
    }

    public void DropdownChange(int index)
    {
        switch (index)
        {
            case 0: AutoSaveDropdownKR.SetActive(true); ;
            case 1: AutoSaveDropdownEN;
            case 1: AutoSaveDropdownJN;
            case 1: AutoSaveDropdownCNS;
            case 1: AutoSaveDropdownCNT;
        }

        switch (index)
        {
            case 1:AutoSaveIntervalDropdownKR;
        case 1:AutoSaveIntervalDropdownEN;
        case 1:AutoSaveIntervalDropdownJN;
        case 1:AutoSaveIntervalDropdownCNS;
        case 1:AutoSaveIntervalDropdownCNT;

                switch (index)
                {
                    case 1:MiniMapDropdownKR;
        case 1:MiniMapDropdownEN;
        case 1:MiniMapDropdownJN;
        case 1:MiniMapDropdownCNS;
        case 1:MiniMapDropdownCNT;

                        switch (index)
                        {
                            case 1:TimeDropdownKR;
        case 1:TimeDropdownEN;
        case 1:TimeDropdownJN;
        case 1:TimeDropdownCNS;
        case 1:TimeDropdownCNT;

        case 1:AutoLooDropdownKR;
        case 1:AutoLooDropdownEN;
        case 1:AutoLooDropdownJN;
        case 1:AutoLooDropdownCNS;
        case 1:AutoLooDropdownCNT;

        case 1:CameraShakeDropdownKR;
        case 1:CameraShakeDropdownEN;
        case 1:CameraShakeDropdownJN;
        case 1:CameraShakeDropdownCNS;
        case 1: CameraShakeDropdownCNT;

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
