using System.Collections;
using UnityEngine;

public class UI_DevMode : MonoBehaviour
{

    private HitPointModule HP;

    [SerializeField] private float Amount;

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
        HP.CurIncreaseHP(Amount);
    }

    public void CurHPMinus()
    {
        HP.CurDecreaseHP(Amount);
    }

    public void MaxHPPlus()
    {
        HP.MaxIncreaseHP(Amount);
    }

    public void MaxHPMinus()
    {
        HP.MaxDecreaseHP(Amount);
    }
}
