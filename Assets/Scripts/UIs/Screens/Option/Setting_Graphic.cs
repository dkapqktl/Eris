using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class Setting_Graphic : MonoBehaviour
{

    [SerializeField] static TextMeshProUGUI VsyncText;
    [SerializeField] static Toggle VsyncColor;
    [SerializeField] public static TMP_Dropdown ResolutionDropdown;

    [Space]
    [Header("On Color")]
    public static ColorBlock OnColor;
    [Space]
    [Header("Off Color")]
    public static ColorBlock OffColor;

    public void Awake()
    {
        GameManager.OnInitializeManager += Initialize;
    }

    private void Initialize()
    {
        ResolutionDropdown.value = SettingManager.CurrentResolution;
    }

    public void OnResolutionChanged(int index)
    {
        SettingManager.SetResolution(index);
    }

    public static void OnSetScreenMode(int index)
    {
        switch (index)
        {
            // 전체화면
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;

            // 테두리 없는 창모드 (Windowed Borderless)
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;

            // 일반 창모드
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }

    public static void OnSetVSync(bool enabled)
    {
        if (enabled)
        {
            QualitySettings.vSyncCount = 1; // 모니터 주사율에 맞춤
            VsyncText.text = "켜짐";
            VsyncColor.colors = OnColor;
        }
        else
        {
            QualitySettings.vSyncCount = 0; // VSync OFF
            VsyncText.text = "꺼짐";
            VsyncColor.colors = OffColor;
        }
    }

    public static void OnSetFPSLimit(int index)
    {
        switch (index)
        {
            case 0 : SetFPSLimit(30); break;
            case 1 : SetFPSLimit(60); break;
            case 2 : SetFPSLimit(120); break;
            case 3 : SetFPSLimit(144); break;
            case 4 : SetFPSLimit(240); break;
            case 5 : SetFPSLimit(-1); break; // FPS 제한 없음
        }
    }

    private static void SetFPSLimit(int fps)
    {
        Application.targetFrameRate = fps;
    }
}
