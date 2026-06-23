using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DevMode : MonoBehaviour
{
    
    private HitPointModule HP;
    private Inventory Inven;
    private LevelSystemModule Level;

    public TMP_InputField hpInputAmount;
    public TMP_InputField inventoryInputAmount;
    public TMP_InputField potionInputAmount;
    public TMP_InputField ExpInputAmount;
    public TMP_InputField LevelInputAmount;

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
        Level = CharacterBase.localPlayer.GetComponent<LevelSystemModule>();
    }

    public void ResetHP()
    {
        if (devMaxHP > 0)
        {
            HP.MaxDecreaseHP(devMaxHP);
        }
        else
        {
            HP.MaxIncreaseHP(-devMaxHP);
        }

        devMaxHP = 0;
    }

    public void DevFullHP()
    {
        HP.FullHP();
    }


    public void MaxHPPlus()
    {
        float originMaxHP = HP.MaxHP;

        if (float.TryParse(hpInputAmount.text, out float asFloat))
        {
            HP.MaxIncreaseHP(asFloat);
        }

        float afterMaxHP = HP.MaxHP;
        devMaxHP += afterMaxHP - originMaxHP;
    }

    public void MaxHPMinus()
    {
        float originMaxHP = HP.MaxHP;

        if (float.TryParse(hpInputAmount.text, out float asFloat))
        {
            HP.MaxDecreaseHP(asFloat);
        }

        float afterMaxHP = HP.MaxHP;
        devMaxHP += afterMaxHP - originMaxHP;
    }

    public void HealPotionPlus()
    {
        ItemContainer potion = DataManager.LoadDataFile<ItemContainer>("LesserHealPotion");

        if (int.TryParse(potionInputAmount.text, out int asInteger))
        {
            Inven.AddItem(potion, asInteger);
        }
    }

    public void HealPotionMinus()
    {
        ItemContainer potion = DataManager.LoadDataFile<ItemContainer>("LesserHealPotion");

        if (int.TryParse(potionInputAmount.text, out int asInteger))
        {
            Inven.RemoveItem(potion, asInteger);
        }
    }

    public void InvenAdd()
    {
        if (Inven == null) return;

        if (int.TryParse(inventoryInputAmount.text, out int asInteger))
        {
            Inven.IncreaseInventory(asInteger);
        }
    }
    public void InvenRemove()
    {
        if (Inven == null || Inven.currentInventorySize <= 1) return;

        if (int.TryParse(inventoryInputAmount.text, out int asInteger))
        {
            Inven.DecreaseInventory(asInteger);
        }
    }

    public void AddExp()
    {
        if (int.TryParse(potionInputAmount.text, out int asInteger))
        {
            Level.AddExp(asInteger);
        }
    }
    public void MinusExp()
    {
        if (int.TryParse(potionInputAmount.text, out int asInteger))
        {
            Level.AddExp(-asInteger);
        }
    }

    public void LevelUpButton()
    {
        Level.LevelUpUpdate();
    }

    public void LevelDownButton()
    {
        Level.LevelDownUpdate();
    }
}
