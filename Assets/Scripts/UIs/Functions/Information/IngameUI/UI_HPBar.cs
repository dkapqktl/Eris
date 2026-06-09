using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_HPBar : UIBase
{
    private HitPointModule HP;

    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI hpText;

    private void Awake()
    {
        //매니저가 로딩 끝나고 나서 초기화 할 거 써놓기!
        GameManager.OnInitializeManager += HPBarStart;
    }

    void HPBarStart()
    {
        HP = CharacterBase.localPlayer.GetModule<HitPointModule>();
        if (HP is null) return;
        HP.OnChangedHP += UpdateBar;
        UpdateBar();
    }

    void OnDestroy()
    {
        if (HP is null) return;
        HP.OnChangedHP -= UpdateBar;
    }

    public void UpdateBar()
    {
        // Max HP에 따라 바 길이 증가
        Vector2 size = backgroundRect.sizeDelta;
        size.y = Mathf.Min(HP.MaxHP, 980);
        backgroundRect.sizeDelta = size;

        // 현재 체력 Fill
        slider.value = HP.MaxHP > 0 ? HP.curHP / HP.MaxHP : 0f;

        // HP 텍스트 수치
        hpText.text = $"{(int)HP.curHP} / {(int)HP.MaxHP}";
    }
}
