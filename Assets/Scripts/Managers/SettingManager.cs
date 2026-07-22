using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public delegate void GraphicSetChangeEvent(int index);
public delegate void TextChangeEvent(string value);
public delegate void GraphicSetBooChangeEvent(bool value);


public delegate void GameplaySetChangeEvent(int index);


public class SettingManager : ManagerBase
{

    public enum Language
    {
        Korean, English, Japanese, SimplifiedChinese, TraditionalChinese
    }


    // Graphic 영역 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public static event GraphicSetChangeEvent OnResolutionChanged;
    public static event GraphicSetChangeEvent OnScreenModeChanged;
    public static event GraphicSetBooChangeEvent OnVSyncChanged;
    public static event GraphicSetChangeEvent OnFPSChanged;

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






    // Gameplay Setting 영역!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public static event GameplaySetChangeEvent LanguageChanged;
    public static event GameplaySetChangeEvent AutoSaveChanged;
    public static event GameplaySetChangeEvent AutoSaveIntervalChanged;
    public static event GameplaySetChangeEvent ShowMiniMapChanged;
    public static event GameplaySetChangeEvent ShowTimeChanged;
    public static event GameplaySetChangeEvent AutoLootChanged;
    public static event GameplaySetChangeEvent CameraShakeChanged;

    public static int defaultLanguage = 0;
    public static int defaultAutoSave = 0;
    public static int defaultAutoSaveInterval = 2;
    public static int defaultShowMiniMap = 0;
    public static int defaultShowTime = 0;
    public static int defaultAutoLoot = 0;
    public static int defaultCameraShake = 0;
    
    static int currentLanguage;
    public static int CurrentLanguage => currentLanguage;
    
    
    static int currentAutoSave;
    public static int CurrentAutoSave => currentAutoSave;
    
    
    static int currentAutoSaveInterval;
    public static int CurrentAutoSaveInterval => currentAutoSaveInterval;
    
    
    static int currentShowMiniMap;
    public static int CurrentShowMiniMap => currentShowMiniMap;
    
    
    static int currentShowTime;
    public static int CurrentShowTime => currentShowTime;
    
    
    static int currentAutoLoot;
    public static int CurrentAutoLoot => currentAutoLoot;


    static int currentCameraShake;
    public static int CurrentCameraShake => currentCameraShake;

    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        GraphicSettingLoad();
        GameplaySettingLoad();

        yield return null;
    }
    protected override void OnDisconnected()
    {

    }






    // Graphic Save & Load 함수들!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

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






    // Gameplay Option Save & Load 함수들!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public void GameplaySettingLoad()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            LoadGameplaySettings();
        }
        else
        {
            currentLanguage = defaultLanguage;
            currentAutoSave = defaultAutoSave;
            currentAutoSaveInterval = defaultAutoSaveInterval;
            currentShowMiniMap = defaultShowMiniMap;
            currentShowTime = defaultShowTime;
            currentAutoLoot = defaultAutoLoot;
            currentCameraShake = defaultCameraShake;
            
            SaveGameplaySettings();
        }

        OnSetLanguage(currentLanguage);
        OnSetAutoSave(currentAutoSave);
        OnSetAutoSaveInterval(currentAutoSaveInterval);
        OnSetShowMiniMap(currentShowMiniMap);
        OnSetShowTime(currentShowTime);
        OnSetAutoLoot(currentAutoLoot);
        OnSetCameraShake(currentCameraShake);
    }
    private static void SaveGameplaySettings()
    {
        PlayerPrefs.SetInt("Language", currentLanguage);
        PlayerPrefs.SetInt("AutoSave", currentAutoSave);
        PlayerPrefs.SetInt("AutoSaveInterval", currentAutoSaveInterval);
        PlayerPrefs.SetInt("ShowMiniMap", currentShowMiniMap);
        PlayerPrefs.SetInt("ShowTime", currentShowTime);
        PlayerPrefs.SetInt("AutoLoot", currentAutoLoot);
        PlayerPrefs.SetInt("CameraShake", currentCameraShake);

        PlayerPrefs.Save();
    }

    private static void LoadGameplaySettings()
    {
        currentLanguage = PlayerPrefs.GetInt("Language", defaultLanguage);
        currentAutoSave = PlayerPrefs.GetInt("AutoSave", defaultAutoSave);
        currentAutoSaveInterval = PlayerPrefs.GetInt("AutoSaveInterval", defaultAutoSaveInterval);
        currentShowMiniMap = PlayerPrefs.GetInt("ShowMiniMap", defaultShowMiniMap);
        currentShowTime = PlayerPrefs.GetInt("ShowTime", defaultShowTime);
        currentAutoLoot = PlayerPrefs.GetInt("AutoLoot", defaultAutoLoot);
        currentCameraShake = PlayerPrefs.GetInt("CameraShake", defaultCameraShake);
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
            case 0:  SetResolution(5120, 1400); break;
            case 1:  SetResolution(3440, 1440); break;
            case 2:  SetResolution(2560, 1080); break;
            case 3:  SetResolution(1920, 1200); break;
            case 4:  SetResolution(1680, 1050); break;
            case 5:  SetResolution(1440, 900); break;
            case 6:  SetResolution(1280, 800); break;
            case 7:  SetResolution(3840, 2160); break;
            case 8:  SetResolution(2560, 1440); break;
            case 9:  SetResolution(1920, 1080); break;
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

    public static void GameplaySettingReset()
    {
        OnSetLanguage(defaultLanguage);
        OnSetAutoSave(defaultAutoSave);
        OnSetAutoSaveInterval(defaultAutoSaveInterval);
        OnSetShowMiniMap(defaultShowMiniMap);
        OnSetShowTime(defaultShowTime);
        OnSetAutoLoot(defaultAutoLoot);
        OnSetCameraShake(defaultCameraShake);
    }

    public static void LanguageReset()
    {
        OnSetLanguage(defaultLanguage);
    }
    public static void AutoSaveReset()
    {
        OnSetAutoSave(defaultAutoSave);
    }
    public static void AutoSaveIntervalReset()
    {
        OnSetAutoSaveInterval(defaultAutoSaveInterval);
    }
    public static void ShowMiniMapReset()
    {
        OnSetShowMiniMap(defaultShowMiniMap);
    }
    public static void ShowTimeReset()
    {
        OnSetShowTime(defaultShowTime);
    }
    public static void AutoLootReset()
    {
        OnSetAutoLoot(defaultAutoLoot);
    }
    public static void CameraShakeReset()
    {
        OnSetCameraShake(defaultCameraShake);
    }


    public static void OnSetLanguage(int index)
    {
        Language targetLanguage;
        switch (index)
        {
            case 0: targetLanguage = Language.Korean; break;
            default: targetLanguage = Language.Korean; break;
        }    

        LanguageManager.SetLanguage(targetLanguage);
        // currentLanguage = index;
        // SaveGameplaySettings();
        // LanguageChanged.Invoke(index);
    }
    public static void OnSetAutoSave(int index)
    {
        // currentAutoSave = index;
        // SaveGameplaySettings();
        // AutoSaveChanged.Invoke(index);
    }
    public static void OnSetAutoSaveInterval(int index)
    {
        // currentAutoSaveInterval = index;
        // SaveGameplaySettings();
        // AutoSaveIntervalChanged.Invoke(index);
    }
    public static void OnSetShowMiniMap(int index)
    {
        //  currentShowMiniMap = index;
        //  SaveGameplaySettings();
        //  ShowMiniMapChanged.Invoke(index);
    }
    public static void OnSetShowTime(int index)
    {
        // currentShowTime = index;
        // SaveGameplaySettings();
        // ShowTimeChanged.Invoke(index);
    }
    public static void OnSetAutoLoot(int index)
    {
        // currentAutoLoot = index;
        // SaveGameplaySettings();
        // AutoLootChanged.Invoke(index);
    }
    public static void OnSetCameraShake(int index)
    {
        // currentCameraShake = index;
        // SaveGameplaySettings();
        // CameraShakeChanged.Invoke(index);
    }






    // Sound Setting 관련 함수들!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public static void SoundSettingReset()
    {

    }
}
