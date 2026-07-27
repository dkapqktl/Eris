using System.Collections.Generic;

public partial class LanguageDictionaries
{
    public static Dictionary<string, string> chineseDictionary_CH = new()
    {
        // Option UI
        { "GraphicButton","图形" },
        { "ControllerButton","控制器" },
        { "GameplayButton","游戏设置" },
        { "SoundButton","声音" },

        { "Reset","重置" },

        { "All Reset Graphic Button", "重置图形设置" },
        { "All Reset Controller Button", "重置控制器设置" },
        { "All Reset Gameplay Button",   "重置游戏设置" },
        { "All Reset Sound Button",      "重置声音设置" },
        { "Initialize Graphic Confirm", "是否要重置图形设置？" },
        { "Initialize Controller Confirm", "是否要重置控制器设置？" },
        { "Initialize Gameplay Confirm",   "是否要重置游戏设置？" },
        { "Initialize Sound Confirm",      "是否要重置声音设置？" },

        { "On", "开启" },
        { "Off", "关闭" },

        { "Yes", "是" },
        { "No", "否" },

        // Graphic Display
        { "Resolution","分辨率" },
        { "ScreenMode","显示模式" },

        // Graphic_ScreenMode Dropdown Text
        { "ExclusiveFullScreen", "独占全屏" },
        { "FullScreenWindow", "无边框窗口" },
        { "Windowed", "窗口模式" },

        // Graphic_FPS Dropdown Text 
        { "Unlimited","无限制" },

        // Controller Display
        { "displayUp", "上" },
        { "displayDown", "下" },
        { "displayLeft", "左" },
        { "displayRight", "右" },
        { "displayInventory", "背包" },

        // Gameplay Display
        { "LanguageSetting", "语言设置" },
        { "AutoSave", "自动保存" },
        { "AutosaveInterval", "自动保存间隔 (分钟)" },
        { "ShowMiniMap", "小地图" },
        { "ShowTime", "显示时间" },
        { "AutoLooting", "自动拾取" },
        { "CameraShake", "镜头震动" },
    };
}
