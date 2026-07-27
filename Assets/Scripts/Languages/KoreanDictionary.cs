using System.Collections.Generic;

public partial class LanguageDictionaries
{
    public static Dictionary<string, string> koreanDictionary = new()
    {
        // Option UI
        { "GraphicButton","그래픽" },
        { "ControllerButton","컨트롤러" },
        { "GameplayButton","게임플레이" },
        { "SoundButton","사운드" },

        { "Reset","초기화"},

        { "All Reset Graphic Button",    "그래픽 전체 초기화" },
        { "All Reset Controller Button", "컨트롤러 전체 초기화" },
        { "All Reset Gameplay Button",   "게임플레이 전체 초기화" },
        { "All Reset Sound Button",      "사운드 전체 초기화" },
        { "Initialize Graphic Confirm",    "그래픽 설정을 초기화하시겠습니까?" },
        { "Initialize Controller Confirm", "컨트롤러 설정을 초기화하시겠습니까?" },
        { "Initialize Gameplay Confirm",   "게임플레이 설정을 초기화하시겠습니까?" },
        { "Initialize Sound Confirm",      "사운드 설정을 초기화하시겠습니까?" },

        { "On", "켜짐" },
        { "Off", "꺼짐" },

        { "Yes", "예" },
        { "No", "아니오" },

        // Graphic Display
        { "Resolution","해상도" },
        { "ScreenMode","스크린모드" },

        // Graphic_ScreenMode Dropdown Text
        { "ExclusiveFullScreen", "전체화면" },
        { "FullScreenWindow", "테두리 없는 창모드" },
        { "Windowed", "창모드" },

        // Graphic_FPS Dropdown Text 
        { "Unlimited","제한없음" },

        // Controller Display
        { "displayUp", "위" },
        { "displayDown", "아래" },
        { "displayLeft", "좌" },
        { "displayRight", "우" },
        { "displayInventory", "인벤토리" },

        // Gameplay Display
        { "LanguageSetting", "언어설정" },
        { "AutoSave", "자동저장" },
        { "AutosaveInterval", "자동저장 주기 (분단위)" },
        { "ShowMiniMap", "미니맵" },
        { "ShowTime", "시간표시" },
        { "AutoLooting", "자동줍기" },
        { "CameraShake", "카메라 흔들림" },
    };
}