using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public delegate void ChangeHPEvent();

public class HitPointModule : CharacterModule
{
    public event ChangeHPEvent OnChangedHP;

    BattleModule isBattle;
    public sealed override Type RegistrationType => typeof(HitPointModule);

    [SerializeField] private bool invincibility;

    [SerializeField] private float _maxhp = 30;
    public float maxHP => _maxhp;


    private float _curhp;
    public float curHP => _curhp;


    private float basicHP = 10;
    public float _minhp => 0f;


    public bool IsDead => _curhp <= _minhp;


    private float hpRegenTimer = 0f; // 시간 카운트
    [SerializeField] private float hpRegenInterval = 5f; // 회복시간 주기
    [SerializeField] private float hpRegenPercent = 0.05f; // 회복 퍼센티지

    public override void OnRegistration(CharacterBase newOwner)
    {
        base.OnRegistration(newOwner); // 캐릭터 생성시 아래것도 해줘
        _curhp = _maxhp; // 시작시 현재체력은 설정해둔 체력으로
        GameManager.OnUpdateCharacter -= RegenHPUpdate; // 게임메니저 업데이트에 리젠HP 업데이트
        GameManager.OnUpdateCharacter += RegenHPUpdate; // 게임메니저 업데이트에 리젠HP 업데이트
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnRegistration(oldOwner); // 캐릭터 없앨때 아래것도 해줘
        GameManager.OnUpdateCharacter -= RegenHPUpdate; // 게임메니저 업데이트에 리젠HP 업데이트 제거
    }

    public void TakeDamage(GameObject causer, ControllerBase instigator, float damage)
    {
        if (IsDead || invincibility) return; // 죽거나 무적 상태라면 리턴

        _curhp = Mathf.Clamp(_curhp - damage, _minhp, _maxhp);

        Owner.DamageNotify(causer, instigator, damage); // 데미지를 쓰는넘들에게 알림

        if (IsDead)
        {
            Owner.DeathNotify(gameObject, damage, _curhp); // 죽음을 쓰는넘들에게 정보 알림
        }
    }

    public float IncreaseHP(float value) => _maxhp += value;    // Increase : 증가하다

    public float DecreaseHP(float value) => Mathf.Max(basicHP, _maxhp - value);    // Decrease : 감소하다
    


    public bool CanHeal() => !IsDead && _curhp < _maxhp;
    public void Heal(float heal)
    {
        if (!CanHeal()) return; // 나중에 확장성을 이대로 두시오!
        _curhp = Mathf.Min((_curhp + heal), _maxhp);
    }
    public void RegenerationHP()
    {
        if (!CanHeal()) return;
        if (isBattle != null && isBattle.isInBattle) return;

        _curhp = Mathf.Min((_curhp + (_maxhp * hpRegenPercent)), _maxhp);
    }
    public void RegenHPUpdate(float deltaTime)
    {
        if (!CanHeal()) return;
        if(isBattle != null && isBattle.isInBattle) return;

        // 위 조건 만족시 리젠타이머에 시간을 더한다
        hpRegenTimer += deltaTime;

        if (hpRegenTimer >= hpRegenInterval) // regenInterval 이 5초라 5초마다 한번씩 회복한다.
        {
            hpRegenTimer = 0f;
            RegenerationHP();
        }
    }


}
