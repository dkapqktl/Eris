using System.Collections;

public delegate string LanguageText(string text);

public class LanguageManager : ManagerBase
{
    public int Korean = 0;
    public int English = 1;
    public int Japanese = 2;
    public int SimplifiedChinese = 3;
    public int TraditionalChinese = 4;

    public static string demo = "그래픽";
    public static string demo2 = "촉기화";

    public static event LanguageText OnResetTextChange;


    protected override IEnumerator OnConnected(GameManager newManager)
    {
        SettingManager.LanguageChanged += SetLanguage;

         yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    public static void SetLanguage(SettingManager.Language index)
    {
        switch(index)
        {
            case SettingManager.Language.Korean: KoreanLanguage(); break;
            case 1: EnglishLanguage(); break;
            case 2: JapaneseLanguage(); break;
            case 3: TraditionalChineseLanguage(); break;
            case 4: SimplifiedChineseLanguage(); break;
        }
    }


    // 한국어

    public static void KoreanLanguage()
    {
        // KoreanResetbutton();
        // KoreanAllResetButton();
    }

    public static string DisplayNameText(int index)
    {
        switch (index)
        {
            case 0: return "초기화";
            default: return "초기화";
        }
    }
    public static string ResetButtonText(int index)
    {
        return default;
    }





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
