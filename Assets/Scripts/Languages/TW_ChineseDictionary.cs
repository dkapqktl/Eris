using System.Collections.Generic;

public partial class LanguageDictionaries
{
    public static Dictionary<string, string> chineseDictionary_TW = new()
    {
        // Option UI
        { "GraphicButton","圖形" },
        { "ControllerButton","控制器" },
        { "GameplayButton","遊戲設定" },
        { "SoundButton","聲音" },

        { "Reset","重設" },

        { "All Reset Graphic Button", "重設圖形設定" },
        { "All Reset Controller Button", "重設控制器設定" },
        { "All Reset Gameplay Button",   "重設遊戲設定" },
        { "All Reset Sound Button",      "重設音效設定" },
        { "Initialize Graphic Confirm", "是否要重設圖形設定？" },
        { "Initialize Controller Confirm", "是否要重設控制器設定？" },
        { "Initialize Gameplay Confirm",   "是否要重設遊戲設定？" },
        { "Initialize Sound Confirm",      "是否要重設音效設定？" },

        { "On", "開啟" },
        { "Off", "關閉" },

        { "Yes", "是" },
        { "No", "否" },

        // Graphic Display
        { "ResolutionDisplay", "解析度" },
        { "ScreenModeDisplay", "顯示模式" },

        // Graphic_ScreenMode Dropdown Text
        { "ExclusiveFullScreen", "獨占全螢幕" },
        { "FullScreenWindow", "無邊框視窗" },
        { "Windowed", "視窗模式" },

        // Graphic_FPS Dropdown Text 
        { "Unlimited","無限制" },

        // Controller Display
        { "displayUp", "上" },
        { "displayDown", "下" },
        { "displayLeft", "左" },
        { "displayRight", "右" },
        { "displayInventory", "背包" },

        // Gameplay Display
        { "LanguageSetting", "語言設定" },
        { "AutoSave", "自動保存" },
        { "AutosaveInterval", "自動保存間隔 (分鐘)" },
        { "ShowMiniMap", "小地圖" },
        { "ShowTime", "顯示時間" },
        { "AutoLooting", "自動拾取" },
        { "CameraShake", "鏡頭震動" },
    };
}