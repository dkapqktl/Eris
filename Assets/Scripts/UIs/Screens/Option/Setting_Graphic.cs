using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setting_Graphic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI VsyncText;
    [SerializeField] Toggle VsyncColor;

    [Space]
    [Header("On Color")]
    public ColorBlock OnColor;
    [Space]
    [Header("Off Color")]
    public ColorBlock OffColor;


    public void OnResolutionChanged(int index)
    {
        switch (index)
        {
            case 0: SetResolution(5120, 1400); break;
            case 1: SetResolution(3440, 1440); break;
            case 2: SetResolution(2560, 1080); break;
            case 3: SetResolution(1920, 1200); break;
            case 4: SetResolution(1680, 1050); break;
            case 5: SetResolution(1440, 900); break;
            case 6: SetResolution(1280, 800); break;
            case 7: SetResolution(3840, 2160); break;
            case 8: SetResolution(2560, 1440); break;
            case 9: SetResolution(1920, 1080); break;
            case 10: SetResolution(1600, 900); break;
            case 11: SetResolution(1366, 768); break;
            case 12: SetResolution(1280, 720); break;
            case 13: SetResolution(1600, 1200); break;
            case 14: SetResolution(1280, 960); break;
            case 15: SetResolution(1024, 768); break;
            case 16: SetResolution(800, 600); break;
        }
    }

    private void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreenMode);
    }

    private int GetResolution()
    {
        switch (Screen.currentResolution.width)
        {
            case 5120: return 0;
            default :
        }
    }

    public void OnSetScreenMode(int index)
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

    public void OnSetVSync(bool enabled)
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

    public void OnSetFPSLimit(int index)
    {
        switch (index)
        {
            case 0 : SetFPSLimit(30); break;
            case 1 : SetFPSLimit(60); break;
            case 2 : SetFPSLimit(120); break;
            case 3 : SetFPSLimit(144); break;
            case 4 : SetFPSLimit(240); break;
            case 5 : SetFPSLimit(-1); break;
        }
    }

    private void SetFPSLimit(int fps)
    {
        QualitySettings.vSyncCount = 0; // FPS 제한 사용 시 VSync 끄기 권장
        Application.targetFrameRate = fps;
    }
}
