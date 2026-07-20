using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public delegate void NumeralValueChangeEvent(int index);
public delegate void BoolValueChangeEvent(bool value);

public delegate void LangaugeGhacngeEvent(SettingManager.Langueage index);


public class SettingManager : ManagerBase
{

    public enum Langueage
    {
        Korean, English, Japanese, SimplifiedChinese, TraditionalChinese
    }


    // Graphic 영역 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public static event NumeralValueChangeEvent OnResolutionChanged;
    public static event NumeralValueChangeEvent OnScreenModeChanged;
    public static event BoolValueChangeEvent    OnVSyncChanged;
    public static event NumeralValueChangeEvent OnFPSChanged;

    public const int basicResolution = 9; // 기본 해상도는 1920x1080

    public static int _defaultResolution; // 기본 해상도는 게임 제일 처음시작할때 해상도로
    public const int _defaultScreenMode = 0;
    public const bool _defaultVSync = false;
    public const int _defaultFPS = 1;

    static int _currentResolution; // 변경된 셋팅값 저장
    public static int CurrentResolution => _currentResolution;

    static int _currentScreenMode;
    public static int CurrentScreenMode => _currentScreenMode;

    static bool _currentVSync;
    public static bool CurrentVSync => _currentVSync;

    static int _currentFPS;
    public static int CurrentFPS => _currentFPS;






    // Game Setting 영역!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public static event LangaugeGhacngeEvent KoreanLanguage;

    public static bool AutoSave;
    public static int AutoSaveInterval;

    public static bool ShowMiniMap;
    public static bool ShowTime;
    public static bool AutoLoot;
    public static bool CameraShake;

    public static int Language;


    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        GraphicSettingLoad();

        yield return null;
    }
    protected override void OnDisconnected()
    {

    }

    public void GraphicSettingLoad()
    {
        if (PlayerPrefs.HasKey("Resolution"))
        {
            LoadGraphicSettings();
        }
        else
        {
            _defaultResolution = GetResolution();

            _currentResolution = _defaultResolution;
            _currentScreenMode = _defaultScreenMode;
            _currentVSync = _defaultVSync;
            _currentFPS = _defaultFPS;

            SaveGraphicSettings();
        }

        OnSetResolution(_currentResolution);
        OnSetScreenMode(_currentScreenMode);
        OnSetVSync(_currentVSync);
        OnSetFPSLimit(_currentFPS);
    }

    public void KeySettingLoad()
    {

    }


    public static void SaveGraphicSettings()
    {

        PlayerPrefs.SetInt("DefaultResolution", _defaultResolution);
        PlayerPrefs.SetInt("Resolution", _currentResolution);
        PlayerPrefs.SetInt("ScreenMode", _currentScreenMode);
        PlayerPrefs.SetInt("VSync", _currentVSync ? 1 : 0);
        PlayerPrefs.SetInt("FPS", _currentFPS);
        
        PlayerPrefs.Save();
    }

   
    public static void LoadGraphicSettings()
    {
        _defaultResolution = PlayerPrefs.GetInt("DefaultResolution", basicResolution);

        _currentResolution = PlayerPrefs.GetInt("Resolution", basicResolution);

        _currentScreenMode = PlayerPrefs.GetInt("ScreenMode", _defaultScreenMode);

        _currentVSync = PlayerPrefs.GetInt("VSync", _defaultVSync ? 1 : 0) == 1;

        _currentFPS = PlayerPrefs.GetInt("FPS", _defaultFPS);
    }







    // Graphic Setting 관련 함수들!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public static void GraphicSettingReset()
    {
        OnSetResolution(_defaultResolution);
        OnSetScreenMode(_defaultScreenMode);
        OnSetVSync(_defaultVSync);
        OnSetFPSLimit(_defaultFPS);
    }

    public static void ResolutionReset()
    {
        OnSetResolution(_defaultResolution);
        OnResolutionChanged?.Invoke(_defaultResolution);
    }

    public static void ScreenModeReset()
    {
        OnSetScreenMode(_defaultScreenMode);
        OnScreenModeChanged?.Invoke(_defaultScreenMode);
    }

    public static void VSyncReset()
    {
        OnSetVSync(_defaultVSync);
        OnVSyncChanged?.Invoke(_defaultVSync);
    }

    public static void FPSReset()
    {
        OnSetFPSLimit(_defaultFPS);
        OnFPSChanged?.Invoke(_defaultFPS);
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
            default: return basicResolution; // 위 해상도에 맞는게 없다면 기본값임
        }
    }

    public static void OnSetResolution(int index)
    {
        // if (0 > index || resolutionDropdownCount - 1 < index) return;

        _currentResolution = index;

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


        SaveGraphicSettings();
        OnResolutionChanged?.Invoke(_currentResolution);
    }

    public static void OnSetScreenMode(int index)
    {
        _currentScreenMode = index;

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

        SaveGraphicSettings();
        OnScreenModeChanged?.Invoke(index);
    }

    public static void OnSetVSync(bool enabled)
    {
        if (enabled)
        {
            QualitySettings.vSyncCount = 1; // 모니터 주사율에 맞춤
        }
        else
        {
            QualitySettings.vSyncCount = 0; // VSync OFF
        }

        _currentVSync = enabled;
        SaveGraphicSettings();
        OnVSyncChanged?.Invoke(enabled);
    }

    public static void OnSetFPSLimit(int index)
    {
        _currentFPS = index;

        switch (index)
        {
            case 0: SetFPSLimit(30); break;
            case 1: SetFPSLimit(60); break;
            case 2: SetFPSLimit(120); break;
            case 3: SetFPSLimit(144); break;
            case 4: SetFPSLimit(240); break;
            case 5: SetFPSLimit(-1); break; // FPS 제한 없음
        }
    }

    private static void SetFPSLimit(int fps)
    {
        Application.targetFrameRate = fps;

        SaveGraphicSettings();
        OnFPSChanged?.Invoke(_currentFPS);
    }






    // Controller Setting 관련 함수들!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    
    public static void ControllerSettingReset()
    {
        Setting_Controller controller = FindFirstObjectByType<Setting_Controller>();

        if (controller == null)
            return;

        foreach (var current in controller.Setter)
        {
            InputAction action = InputManager.ClaimGetAction(current.ActionName);

            if (action == null)
                continue;

            action.RemoveAllBindingOverrides();
        }

        PlayerPrefs.DeleteKey("KeyBindings");
        PlayerPrefs.Save();

        KeySetter[] setters = FindObjectsByType<KeySetter>(
            FindObjectsSortMode.None);

        foreach (var setter in setters)
        {
            setter.Refresh();
        }
    }




    // Game Option Setting 관련 함수들!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public static void GameOptionSettingReset()
    {
        PlayerPrefs.SetInt("Language", 0);

        PlayerPrefs.SetInt("AutoSave", 1);
        PlayerPrefs.SetInt("AutoSaveInterval", 1);

        PlayerPrefs.SetInt("ShowMiniMap", 1);
        PlayerPrefs.SetInt("ShowTime", 1);
        PlayerPrefs.SetInt("AutoLoot", 1);
        PlayerPrefs.SetInt("CameraShake", 1);

        PlayerPrefs.Save();

        // 변수 다시 로드
        Setting_GameSet.LoadSettings();

        // UI 즉시 갱신
        // Setting_GameSet.LanguageDropdown.value = 0;
        // 
        // Setting_GameSet.AutoSaveToggle.isOn = true;
        // Setting_GameSet.AutoSaveIntervalDropdown.value = 1;
        // 
        // Setting_GameSet.MiniMapToggle.isOn = true;
        // Setting_GameSet.TimeToggle.isOn = true;
        // Setting_GameSet.AutoLootToggle.isOn = true;
        // Setting_GameSet.CameraShakeToggle.isOn = true;
    }

    




    // Sound Setting 관련 함수들!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public static void SoundSettingReset()
    {

    }
}
