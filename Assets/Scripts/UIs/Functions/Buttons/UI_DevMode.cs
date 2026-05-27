using System.Collections;
using UnityEngine;

public class UI_DevMode : MonoBehaviour
{

    private HitPointModule HP;
    private Inventory Inven;

    public float amount;
    public int itemAmount;

    private void Awake()
    {
        //매니저가 로딩 끝나고 나서 초기화 할 거 써놓기!
        GameManager.OnInitializeManager += HPBarStart;
    }

    void HPBarStart()
    {
        HP = CharacterBase.localPlayer.GetModule<HitPointModule>();
    }


    public void CurHPPlus()
    {
        HP.CurIncreaseHP(amount);
    }

    public void CurHPMinus()
    {
        HP.CurDecreaseHP(amount);
    }

    public void MaxHPPlus()
    {
        HP.MaxIncreaseHP(amount);
    }

    public void MaxHPMinus()
    {
        HP.MaxDecreaseHP(amount);
    }

    public void HealPotionPlus()
    {
        ItemContainer potion = DataManager.LoadDataFile<ItemContainer>("LesserHealPotion");
        Inven.AddItem(potion, itemAmount);
    }

    public void InvenPlus()
    {
        if (Inven == null) return;
        Inven.rows += 1;
    }
    public void InvenMinus()
    {
        if (Inven == null || Inven.rows <= 1) return;
        // if (Inven.columns == 5 || )
        Inven.rows -= 1;
    }
}
