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

        SettingManager.OnGraphicChanged += OnGraphicChangedEvent;
    }

    private void Initialize()
    {
        ResolutionDropdown.value = SettingManager.CurrentResolution;
        ScreenModeDropdown.value = SettingManager.CurrentScreenMode;
        VSyncToggle.isOn = SettingManager.CurrentVSync;
        FPSDropdown.value = SettingManager.CurrentFPS;
    }

    public void OnGraphicChangedEvent(int index)
    {
        switch (index)
        {
            case 0: OnResolutionChange(SettingManager.CurrentResolution); break;
            case 1: OnScreenModeChange(SettingManager.CurrentScreenMode); break;
            case 2: OnVSyncChange(SettingManager.CurrentVSync); break;
            case 3: OnFPSLimitChange(SettingManager.CurrentFPS); break;
        }
    }

    public void OnResolutionChange(int index)
    {
        SettingManager.resolutionDropdownCount = ResolutionDropdown.options.Count;
        SettingManager.OnSetResolution(index);
        ResolutionDropdown.value = index;
    }

    public void OnScreenModeChange(int index)
    {
        SettingManager.OnSetScreenMode(index);
        ScreenModeDropdown.value = index;
    }

    public void OnVSyncChange(bool enabled)
    {
        SettingManager.OnSetVSync(enabled);

        if (enabled)
        {
            VsyncText.text = "ÄÑÁü";
            VsyncColor.colors = OnColor;
        }
        else
        {
            VsyncText.text = "²¨Áü";
            VsyncColor.colors = OffColor;
        }
    }

    public void OnFPSLimitChange(int index)
    {
        SettingManager.OnSetFPSLimit(index);
        FPSDropdown.value = index;
    }
}
