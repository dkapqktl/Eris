using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setting_Graphic : MonoBehaviour
{

    [SerializeField] TMP_Dropdown ResolutionDropdown;
    [SerializeField] TMP_Dropdown ScreenModeDropdown;
    [SerializeField] Toggle VSyncToggle;
    [SerializeField] TextMeshProUGUI VsyncText;
    [SerializeField] Toggle VsyncColor;

    [SerializeField] TextMeshProUGUI ResolutionDisplayText;
    [SerializeField] TextMeshProUGUI ScreenModeDisplayText;

    [SerializeField] TextMeshProUGUI[] ResetText;



    [SerializeField] TMP_Dropdown FPSDropdown;

    [Space]
    [Header("On Color")]
    [SerializeField] public ColorBlock OnColor;
    [Space]
    [Header("Off Color")]
    [SerializeField] public ColorBlock OffColor;

    public void Awake()
    {
        GameManager.OnInitializeManager += Initialize;

        SettingManager.OnResolutionChanged  += OnResolutionChange;
        SettingManager.OnScreenModeChanged  += OnScreenModeChange;
        SettingManager.OnVSyncChanged       += OnVSyncChange;
        SettingManager.OnFPSChanged         += OnFPSLimitChange;

        LanguageManager.OnLanguageTextChange += OnLanguageChange;

    }

    private void Initialize()
    {
        ResolutionDropdown.value = SettingManager.CurrentResolution;
        ScreenModeDropdown.value = SettingManager.CurrentScreenMode;
        VSyncToggle.isOn = SettingManager.CurrentVSync;
        FPSDropdown.value = SettingManager.CurrentFPS;
    }

    public void OnLanguageChange()
    {
        ResolutionDisplayText.text = LanguageManager.GetText("ResolutionDisplay");
        ScreenModeDisplayText.text = LanguageManager.GetText("ScreenModeDisplay");

        for (int i = 0; i < ResetText.Length; i++)
        {
            ResetText[i].text = LanguageManager.GetText("Reset");
        }
    }


    public void ClaimResolutionChange(int index)
    {
        SettingManager.OnSetResolution(index);
    }

    public void OnResolutionChange(int index)
    {
        ResolutionDropdown.value = index;
    }

    public void ClaimResolutionReset()
    {
        SettingManager.ResolutionReset();
    }

    public void ClaimScreenModeReset()
    {
        SettingManager.ScreenModeReset();
    }
    
    public void ClaimVSyncReset()
    {
        SettingManager.VSyncReset();
    }

    public void ClaimFPSReset()
    {
        SettingManager.FPSReset();
    }

    public void ClaimScreenModeChange(int index)
    {
        SettingManager.OnSetScreenMode(index);
    }
    public void OnScreenModeChange(int index)
    {
        ScreenModeDropdown.value = index;
    }



    public void ClaimVSyncChange(bool enabled)
    {
        SettingManager.OnSetVSync(enabled);
    }

    public void OnVSyncChange(bool enabled)
    {
        if (enabled)
        {
            VsyncText.text = "O";
            VsyncColor.colors = OnColor;
        }
        else
        {
            VsyncText.text = "X";
            VsyncColor.colors = OffColor;
        }
    }

    public void ClaimFPSLimitChange(int index)
    {
        SettingManager.OnSetFPSLimit(index);
    }
    public void OnFPSLimitChange(int index)
    {
        FPSDropdown.value = index;
    }
}
