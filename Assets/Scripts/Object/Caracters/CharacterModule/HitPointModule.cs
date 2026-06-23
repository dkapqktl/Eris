using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public delegate void ChangeHPEvent();

public class HitPointModule : CharacterModule
{
    public ChangeHPEvent OnChangedHP;

    BattleModule isBattle;
    StatusModule isStatus;
    LevelSystemModule isLevel;

    public sealed override Type RegistrationType => typeof(HitPointModule);

    [SerializeField] private bool invincibility;

    [SerializeField] private float baseMaxHP = 10;
    
    public float MaxHP => baseMaxHP + (isStatus.Strength * 5) + (isLevel.level * 10);

    private float _curHP = 30;
    public float curHP => _curHP;

    private float basicHP = 10;

    public float _minhp = 0f;

    public bool IsDead => _curHP <= _minhp;


    private float hpRegenTimer = 0f; // 시간 카운트
    [SerializeField] private float hpRegenInterval = 5f; // 회복시간 주기
    [SerializeField] private float hpRegenPercent = 0.05f; // 회복 퍼센티지

    public override void OnRegistration(CharacterBase newOwner)
    {
        base.OnRegistration(newOwner); // 캐릭터 생성시 아래것도 해줘
        GameManager.OnUpdateCharacter -= RegenHPUpdate; // 게임메니저 업데이트에 리젠HP 업데이트
        GameManager.OnUpdateCharacter += RegenHPUpdate; // 게임메니저 업데이트에 리젠HP 업데이트

        if(newOwner)
        {
            isStatus = newOwner.GetComponent<StatusModule>();
            isLevel = newOwner.GetComponent<LevelSystemModule>();
            isBattle = newOwner.GetComponent<BattleModule>();
            isStatus.OnStatusChanged -= BroadCastChangedHP;
            isStatus.OnStatusChanged += BroadCastChangedHP;
        }
        _curHP = MaxHP; // 시작시 현재체력은 설정해둔 체력으로
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnRegistration(oldOwner); // 캐릭터 없앨때 아래것도 해줘
        GameManager.OnUpdateCharacter -= RegenHPUpdate; // 게임메니저 업데이트에 리젠HP 업데이트 제거
        isStatus.OnStatusChanged -= BroadCastChangedHP;
    }

    public void TakeDamage(GameObject causer, ControllerBase instigator, float damage)
    {
        if (IsDead || invincibility) return; // 죽거나 무적 상태라면 리턴

        _curHP = Mathf.Clamp(_curHP - damage, _minhp, MaxHP);

        Owner.DamageNotify(causer, instigator, damage); // 데미지를 쓰는넘들에게 알림

        if (IsDead)
        {
            Owner.DeathNotify(gameObject, damage, _curHP); // 죽음을 쓰는넘들에게 정보 알림
        }

        BroadCastChangedHP();
    }

    // public float CurIncreaseHP(float value) // 현재 체력 증가
    // {
    //     _curHP = Mathf.Min(MaxHP, _curHP + value);
    //     OnChangedHP?.Invoke();
    //     return _curHP;
    // }
    // 
    // public float CurDecreaseHP(float value)
    // {
    //     if (IsDead) return 0;
    //     _curHP -= Mathf.Min(_curHP, value);
    //     OnChangedHP?.Invoke();
    //     return _curHP;
    // }

    public void BroadCastChangedHP() => OnChangedHP?.Invoke();

    public void MaxIncreaseHP(float value)
    {
        baseMaxHP += value;
        BroadCastChangedHP();
    }    // Increase : 증가하다

    public void MaxDecreaseHP(float value)
    {
        baseMaxHP = Mathf.Max(basicHP, baseMaxHP - value);
        if (_curHP >= MaxHP) _curHP = MaxHP;

        BroadCastChangedHP();
    }    // Decrease : 감소하다

    public bool CanHeal() => !IsDead && _curHP < MaxHP;
    public void Heal(float addHeal, float multiHeal)
    {
        if (!CanHeal()) return; // 나중에 확장성을 이대로 두시오!
        float add = _curHP + addHeal; // 20회복 이런거일때
        float multiply = MaxHP * multiHeal; // 최대체력의 20% 회복일때
        _curHP = Mathf.Min(add + multiply, MaxHP); // 현재체력이 맥스체력을 넘지 않게

        BroadCastChangedHP();
    }


    public float FullHP()
    {
        if (_curHP == MaxHP) return _curHP;
        _curHP = MaxHP;
        BroadCastChangedHP();
        return _curHP;
    }

    public void RegenerationHP()
    {
        if (!CanHeal()) return;
        if (isBattle == null || isBattle.isInBattle) return;

        _curHP = Mathf.Min((_curHP + (MaxHP * hpRegenPercent)), MaxHP);

        BroadCastChangedHP();
    }
    public void RegenHPUpdate(float deltaTime)
    {
        if (!CanHeal()) return;
        if(isBattle == null || isBattle.isInBattle) return;

        // 위 조건 만족시 리젠타이머에 시간을 더한다
        hpRegenTimer += deltaTime;

        if (hpRegenTimer >= hpRegenInterval) // regenInterval 이 5초라 5초마다 한번씩 회복한다.
        {
            hpRegenTimer = 0f;
            RegenerationHP();
        }
    }


}
