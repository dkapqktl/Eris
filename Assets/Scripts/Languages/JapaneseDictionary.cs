using System.Collections.Generic;

public partial class LanguageDictionaries
{
    public static Dictionary<string, string> japaneseDictionary = new()
    {
        // Option UI
        { "GraphicButton","グラフィック" },
        { "ControllerButton","コントローラー" },
        { "GameplayButton","ゲームプレイ" },
        { "SoundButton","サウンド" },

        { "Reset","リセット" },

        { "All Reset Graphic Button", "グラフィック設定のリセット" },
        { "All Reset Controller Button", "コントローラー設定のリセット" },
        { "All Reset Gameplay Button",   "ゲームプレイ設定のリセット" },
        { "All Reset Sound Button",      "サウンド設定のリセット" },
        { "Initialize Graphic Confirm", "グラフィック設定をリセットしますか？" },
        { "Initialize Controller Confirm", "コントローラー設定をリセットしますか？" },
        { "Initialize Gameplay Confirm",   "ゲームプレイ設定をリセットしますか？" },
        { "Initialize Sound Confirm",      "サウンド設定をリセットしますか？" },

        { "On", "オン" },
        { "Off", "オフ" },

        { "Yes", "はい" },
        { "No", "いいえ" },

        // Graphic Display
        { "Resolution","解像度" },
        { "ScreenMode","画面モード" },

        // Graphic_ScreenMode Dropdown Text
        { "ExclusiveFullScreen", "フルスクリーン" },
        { "FullScreenWindow", "ボーダーレスウィンドウ" },
        { "Windowed", "ウィンドウ" },

        // Graphic_FPS Dropdown Text 
        { "Unlimited","制限なし" },

        // Controller Display
        { "displayUp", "上" },
        { "displayDown", "下" },
        { "displayLeft", "左" },
        { "displayRight", "右" },
        { "displayInventory", "インベントリ" },

        // Gameplay Display
        { "LanguageSetting", "言語設定" },
        { "AutoSave", "オートセーブ" },
        { "AutosaveInterval", "オートセーブ間隔 (分)" },
        { "ShowMiniMap", "ミニマップ" },
        { "ShowTime", "時間表示" },
        { "AutoLooting", "自動拾取" },
        { "CameraShake", "カメラシェイク" },
    };
}
