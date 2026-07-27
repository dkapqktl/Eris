using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting_Graphic : MonoBehaviour
{
    [Header("Display Name")]
    [SerializeField] TextMeshProUGUI ResolutionDisplayText;
    [SerializeField] TextMeshProUGUI ScreenModeDisplayText;

    [Space]
    [Header("Dropdown")]
    [SerializeField] TMP_Dropdown ResolutionDropdown;
    [SerializeField] TMP_Dropdown ScreenModeDropdown;
    [SerializeField] TMP_Dropdown VSyncDropdown;
    [SerializeField] TMP_Dropdown FPSDropdown;

    [Space]
    [Header("Reset Text")]
    [SerializeField] TextMeshProUGUI[] ResetText;

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
        VSyncDropdown.value = SettingManager.CurrentVSync;
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




    public void ClaimScreenModeChange(int index)
    {
        SettingManager.OnSetScreenMode(index);
    }
    public void OnScreenModeChange(int index)
    {
        ScreenModeDropdown.value = index;
    }
    public void ClaimScreenModeReset()
    {
        SettingManager.ScreenModeReset();
    }



    public void ClaimVSyncChange(int index)
    {
        SettingManager.OnSetVSync(index);
    }
    public void OnVSyncChange(int index)
    {
        ScreenModeDropdown.value = index;
    }
    public void ClaimVSyncReset()
    {
        SettingManager.VSyncReset();
    }




    public void ClaimFPSLimitChange(int index)
    {
        SettingManager.OnSetFPSLimit(index);
    }
    public void OnFPSLimitChange(int index)
    {
        FPSDropdown.value = index;
    }
    public void ClaimFPSReset()
    {
        SettingManager.FPSReset();
    }
}
