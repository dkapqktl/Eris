using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_HPBar : UIBase
{
    HitPointModule HP;

    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private Image fillImage;


    void OnEnable()
    {  
        if(HP is not null) HP.OnChangedHP += UpdateBar;
    }

    void OnDisable()
    {
        if (HP is not null) HP.OnChangedHP -= UpdateBar;
    }

    public void UpdateBar()
    {
        // Max HP에 따라 바 길이 증가
        Vector2 size = backgroundRect.sizeDelta;
        size.x = HP.maxHP;
        if (HP.maxHP >= 980) { size.x = 980; }
        else backgroundRect.sizeDelta = size;

        // 현재 체력 Fill
        fillImage.fillAmount = HP.curHP / HP.maxHP;
    }
}
