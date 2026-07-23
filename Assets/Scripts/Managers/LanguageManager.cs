using System;
using System.Collections;
using System.Collections.Generic;

public delegate void LanguageTextChange();

public class LanguageManager : ManagerBase
{
    public const int Korean = 0;
    public const int English = 1;
    public const int Japanese = 2;
    public const int SimplifiedChinese = 3; // 간체 = 중국
    public const int TraditionalChinese = 4; // 번체 = 대만,홍콩

    public static event LanguageTextChange OnLanguageTextChange;

    public static string[] text;

    public static Dictionary<string, string> languageDictionary;

    protected override IEnumerator OnConnected(GameManager newManager)
    {
         // SettingManager.LanguageChanged += SetLanguage;

         yield return null;
    }


    protected override void OnDisconnected()
    {

    }

    public static void SetLanguage(SettingManager.Language index)
    {
        switch(index)
        {
            case SettingManager.Language.Korean             : LanguageChange(Korean); break;
            case SettingManager.Language.English            : LanguageChange(English); break;
            case SettingManager.Language.Japanese           : LanguageChange(Japanese); break;
            case SettingManager.Language.TraditionalChinese : LanguageChange(SimplifiedChinese); break;
            case SettingManager.Language.SimplifiedChinese  : LanguageChange(TraditionalChinese); break;
        }
    }

    public static void LanguageChange(int index)
    {
        GameSetDisplayNameText(index);
        // Resetbutton();
        // AllResetButton();
    }

    public static string GetText(string key) => languageDictionary[key];

    public static void GameSetDisplayNameText(int index)
    {
        switch (index)
        {
            case 0: // 한국어
                {
                    languageDictionary = new()
                    {
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

                        { "displayUp", "위"},
                        { "displayDown", "아래"},
                        { "displayLeft", "좌"},
                        { "displayRight", "우"},
                        { "displayInventory", "인벤토리"},

                        { "LanguageSetting", "언어설정" },
                        { "AutoSave", "자동저장" },
                        { "AutosaveInterval", "자동저장 주기" },
                        { "ShowMiniMap", "미니맵" },
                        { "ShowTime", "시간표시" },
                        { "AutoLooting", "자동줍기" },
                        { "CameraShake", "카메라 흔들림" },
                    };

                    OnLanguageTextChange.Invoke();
                }
                break;

            case 1: // 영어
                {
                    languageDictionary = new()
                    {
                        { "Reset","Reset"},

                        { "All Reset Graphic Button", "Reset Graphics Settings" },
                        { "All Reset Controller Button", "Reset Controller Settings" },
                        { "All Reset Gameplay Button",   "Reset Gameplay Settings" },
                        { "All Reset Sound Button",      "Reset Sound Settings" },
                        { "Initialize Graphic Confirm", "Do you want to reset the graphics settings?" },
                        { "Initialize Controller Confirm", "Do you want to reset the controller settings?" },
                        { "Initialize Gameplay Confirm",   "Do you want to reset the gameplay settings?" },
                        { "Initialize Sound Confirm",      "Do you want to reset the sound settings?" },

                        { "displayUp", "Up"},
                        { "displayDown", "Down"},
                        { "displayLeft", "Left"},
                        { "displayRight", "Right"},
                        { "displayInventory", "Inventory"},

                        { "LanguageSetting", "Language" },
                        { "AutoSave", "Auto Save" },
                        { "AutosaveInterval", "Auto Save Interval" },
                        { "ShowMiniMap", "Mini Map" },
                        { "ShowTime", "Show Time" },
                        { "AutoLooting", "Auto Loot" },
                        { "CameraShake", "Camera Shake" },
                    };

                    OnLanguageTextChange.Invoke();
                }
                break;

            case 2: // 일본어  
                {
                    languageDictionary = new()
                    {
                        { "Reset","リセット"},

                        { "All Reset Graphic Button", "グラフィック設定のリセット" },
                        { "All Reset Controller Button", "コントローラー設定のリセット" },
                        { "All Reset Gameplay Button",   "ゲームプレイ設定のリセット" },
                        { "All Reset Sound Button",      "サウンド設定のリセット" },
                        { "Initialize Graphic Confirm", "グラフィック設定をリセットしますか？" },
                        { "Initialize Controller Confirm", "コントローラー設定をリセットしますか？" },
                        { "Initialize Gameplay Confirm",   "ゲームプレイ設定をリセットしますか？" },
                        { "Initialize Sound Confirm",      "サウンド設定をリセットしますか？" },

                        { "displayUp", "上"},
                        { "displayDown", "下"},
                        { "displayLeft", "左"},
                        { "displayRight", "右"},
                        { "displayInventory", "インベントリ"},

                        { "LanguageSetting", "言語設定" },
                        { "AutoSave", "オートセーブ" },
                        { "AutosaveInterval", "オートセーブ間隔" },
                        { "ShowMiniMap", "ミニマップ" },
                        { "ShowTime", "時間表示" },
                        { "AutoLooting", "自動拾取" },
                        { "CameraShake", "カメラシェイク" },
                    };

                    OnLanguageTextChange.Invoke();
                }
                break;

            case 3: // 중국어 간체         
                {
                    languageDictionary = new()
                    {
                        { "Reset","重置"},

                        { "All Reset Graphic Button", "重置图形设置" },
                        { "All Reset Controller Button", "重置控制器设置" },
                        { "All Reset Gameplay Button",   "重置游戏设置" },
                        { "All Reset Sound Button",      "重置声音设置" },
                        { "Initialize Graphic Confirm", "是否要重置图形设置？" },
                        { "Initialize Controller Confirm", "是否要重置控制器设置？" },
                        { "Initialize Gameplay Confirm",   "是否要重置游戏设置？" },
                        { "Initialize Sound Confirm",      "是否要重置声音设置？" },

                        { "displayUp", "上"},
                        { "displayDown", "下"},
                        { "displayLeft", "左"},
                        { "displayRight", "右"},
                        { "displayInventory", "背包"},

                        { "LanguageSetting", "语言设置" },
                        { "AutoSave", "自动保存" },
                        { "AutosaveInterval", "自动保存间隔" },
                        { "ShowMiniMap", "小地图" },
                        { "ShowTime", "显示时间" },
                        { "AutoLooting", "自动拾取" },
                        { "CameraShake", "镜头震动" },
                    };

                    OnLanguageTextChange.Invoke();
                }
                break;

            case 4: // 중국어 번체
                {
                    languageDictionary = new()
                    {
                        { "Reset","重設"},

                        { "All Reset Graphic Button", "重設圖形設定" },
                        { "All Reset Controller Button", "重設控制器設定" },
                        { "All Reset Gameplay Button",   "重設遊戲設定" },
                        { "All Reset Sound Button",      "重設音效設定" },
                        { "Initialize Graphic Confirm", "是否要重設圖形設定？" },
                        { "Initialize Controller Confirm", "是否要重設控制器設定？" },
                        { "Initialize Gameplay Confirm",   "是否要重設遊戲設定？" },
                        { "Initialize Sound Confirm",      "是否要重設音效設定？" },

                        { "displayUp", "上"},
                        { "displayDown", "下"},
                        { "displayLeft", "左"},
                        { "displayRight", "右"},
                        { "displayInventory", "背包"},

                        { "LanguageSetting", "語言設定" },
                        { "AutoSave", "自動保存" },
                        { "AutosaveInterval", "自動保存間隔" },
                        { "ShowMiniMap", "小地圖" },
                        { "ShowTime", "顯示時間" },
                        { "AutoLooting", "自動拾取" },
                        { "CameraShake", "鏡頭震動" },
                    };

                    OnLanguageTextChange.Invoke();
                }
                break;
        }
    } // 게임셋 디스플레이네임


    // 영어

    public static void EnglishLanguage()
    {
        EnglishResetbutton();
    }
    public static void EnglishResetbutton()
    {
        // OnKoreanTextChange.Invoke("Reset");
    }
    



    // 일본어

    public static void JapaneseLanguage()
    {

    }



    // 중국어 간체

    public static void TraditionalChineseLanguage()
    {

    }





    // 중국어 번체

    public static void SimplifiedChineseLanguage()
    {

    }
}
