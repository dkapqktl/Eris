using UnityEngine;
using UnityEngine.UI;

public class UI_ExpBar : UIBase
{
    HitPointModule HP;
    StatusModule status;
    LevelSystemModule levelSystem;

    [SerializeField] private Slider expSlider;
    // [SerializeField] private TextMeshProUGUI expText;

    private void Awake()
    {
        //매니저가 로딩 끝나고 나서 초기화 할 거 써놓기!
        GameManager.OnInitializeManager += ExpBarStart;
    }

    void ExpBarStart()
    {
        levelSystem = CharacterBase.localPlayer.GetModule<LevelSystemModule>();
        if (levelSystem is null) return;
        levelSystem.OnExpChanged -= ExpUpdateBar;
        levelSystem.OnExpChanged += ExpUpdateBar;
        ExpUpdateBar();
    }

    void OnDestroy()
    {
        if (levelSystem is null) return;
        levelSystem.OnExpChanged -= ExpUpdateBar;
    }

    public void ExpUpdateBar()
    {
        // 현재 경험치 Fill
        expSlider.value = levelSystem.requiredExp > 0 ? levelSystem.currentExp / levelSystem.requiredExp : 0f;

        // EXP 텍스트 수치
        // expText.text = $"{(int)currentExp} / {(int)requiredExp}";
    }
}
