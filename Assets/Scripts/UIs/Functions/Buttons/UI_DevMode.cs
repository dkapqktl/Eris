using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DevMode : MonoBehaviour
{
    
    private HitPointModule HP;
    private Inventory Inven;

    public TMP_InputField inputAmount;

    public float devCurHP;
    public float devMaxHP;

    private void Awake()
    {
        //매니저가 로딩 끝나고 나서 초기화 할 거 써놓기!
        GameManager.OnInitializeManager += CharacterStart;
    }

    void CharacterStart()
    {
        HP = CharacterBase.localPlayer.GetModule<HitPointModule>();
        Inven = CharacterBase.localPlayer.GetComponent<Inventory>();
    }

    public void ResetHP()
    {
        if (devCurHP > 0)
        {
            HP.CurDecreaseHP(devCurHP);
            HP.MaxDecreaseHP(devMaxHP);
        }
        else
        {
            HP.CurIncreaseHP(-devCurHP);
            HP.MaxIncreaseHP(-devMaxHP);
        }

        devCurHP = 0;
    }

    public void DevFullHP()
    {
        HP.FullHP();
    }


    public void MaxHPPlus(int value)
    {
        float originMaxHP = HP.maxHP;
        HP.MaxIncreaseHP(float.Parse(inputAmount.text));
        float afterMaxHP = HP.maxHP;
        devMaxHP += afterMaxHP - originMaxHP;
    }

    public void MaxHPMinus(int value)
    {
        float originCurHP = HP.curHP;
        float originMaxHP = HP.maxHP;
        HP.MaxDecreaseHP(float.Parse(inputAmount.text));
        float afterCurHP = HP.curHP;
        float afterMaxHP = HP.maxHP;
        devCurHP += afterCurHP - originCurHP;
        devMaxHP += afterMaxHP - originMaxHP;
    }

    public void HealPotionPlus()
    {
        ItemContainer potion = DataManager.LoadDataFile<ItemContainer>("LesserHealPotion");
        Inven.AddItem(potion, int.Parse(inputAmount.text));
    }

    public void HealPotionMinus()
    {
        ItemContainer potion = DataManager.LoadDataFile<ItemContainer>("LesserHealPotion");
        Inven.RemoveItem(potion, int.Parse(inputAmount.text));
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
