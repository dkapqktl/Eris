using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class HitPointModule : CharacterModule
{
    [SerializeField] private float _maxHP = 100f;
    public float MaxHP => _maxHP;
    public float MinHP => 0f;

    private float _curHP;
    public float CurHP => _curHP;


    [SerializeField] private bool invincibility;

    public bool IsDead => _curHP <= MinHP;
    public sealed override Type RegistrationType =>  typeof(HitPointModule);

    private float regenTimer = 0f;
    [SerializeField] private float regenInterval = 5f;
    [SerializeField] private float regenPercent = 0.05f;

    BattleModule isBattle;


    public override void OnRegistration(CharacterBase newOwner) 
    {
        base.OnRegistration(newOwner);
        _curHP = _maxHP;
        GameManager.OnUpdateCharacter -= RegenHPUpdate;
        GameManager.OnUpdateCharacter += RegenHPUpdate;
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnRegistration(oldOwner);
        GameManager.OnUpdateCharacter -= RegenHPUpdate;
    }



    public void TakeDamage(GameObject causer, ControllerBase instigator, float damage)
    {
        if (IsDead || invincibility) return; // 죽거나 무적 상태가 아니라면 끝

        _curHP -= damage; // 현재 체력에서 데미지만큼 빼
        _curHP = Mathf.Clamp(_curHP, MinHP, MaxHP);
        // 현재 체력이 최소체력 이하면 최소체력 반영
        // 현재 체력이 최대체력 이상이면 최대체력 반영

        Owner.DamageNotify(causer, instigator, damage);

        if (IsDead)
        {
            _curHP = MinHP;
            Owner.DeathNotify(gameObject, damage, _curHP);
        }
    }

    public void Heal(float heal)
    {
        if (IsDead) return;

        _curHP += heal; // 현재 체력에서 힐만큼 더해
        _curHP = Mathf.Clamp(_curHP, MinHP, MaxHP);
    }



    public void RegenerationHP()
    {
        if (IsDead) return;
        if (_curHP >= _maxHP) return;

        _curHP += _maxHP * regenPercent;
        _curHP = Mathf.Min(_curHP, _maxHP);
    }


    // Increase : 증가하다
    public float IncreaseHP(float value)
    {
        _maxHP += value;
        return _maxHP;
    }

    // Decrease : 감소하다
    public float DecreaseHP(float value)
    {
        _maxHP -= value;
        if (_maxHP <= 1f) _maxHP = 1f;
        if (_curHP > _maxHP) _curHP = _maxHP;
        return _maxHP;
    }
    // public float SetHP(float value);

    // public bool OutCheck()



    public void RegenHPUpdate(float deltaTime)
    {
        if (IsDead) return;
        if (_curHP >= _maxHP) return;
        if(isBattle != null && isBattle.isInBattle) return;

        regenTimer += deltaTime;

        if (regenTimer >= regenInterval)
        {
            regenTimer = 0f;
            RegenerationHP();
        }

    }
}
