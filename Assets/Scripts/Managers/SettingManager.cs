using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class SettingManager : ManagerBase
{
    static int currentResolution;
    public static int CurrentResolution => currentResolution;

    [SerializeField] 

    public const int _defaultResolution = 9;
    public const int _defaultScreenMode = 1;
    public const bool _defaultVSync = false;
    public const int _defaultFPS = 1;

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        currentResolution = GetResolution();

        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    public static void GraphicSettingReset()
    {
        SetResolution(_defaultResolution);
        Setting_Graphic.OnSetScreenMode(_defaultScreenMode);
        Setting_Graphic.OnSetVSync(_defaultVSync);
        Setting_Graphic.OnSetFPSLimit(_defaultFPS);
    }

    private int GetResolution()
    {
        int width = Screen.currentResolution.width;
        int height = Screen.currentResolution.height;

        switch (width)
        {
            case 5120: return 0;
            case 3440: return 1;
            case 2560: return height == 1080 ? 2 : 8;
            case 1920: return height == 1200 ? 3 : 9;
            case 1680: return 4;
            case 1440: return 5;
            case 1280: return height == 800 ? 6 : height == 720 ? 12 : 14;
            case 3840: return 7;
            case 1600: return height == 900 ? 10 : 13;
            case 1366: return 11;
            case 1024: return 15;
            case 800: return 16;
            default: return _defaultResolution;
        }
    }

    public static void SetResolution(int index)
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
    private static void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreenMode);
    }
}
