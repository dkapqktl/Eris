using System;
using UnityEngine;

public class ManaPointModule : CharacterModule
{
    public event Action OnDeath;

    [SerializeField] private bool invincibility;
    [SerializeField] private float _maxMP = 100f;

    public float MaxMP => _maxMP;
    public float MinMP => 0f;


    private float _curMP;
    public float CurMP => _curMP;


    public bool IsDead => _curMP <= MinMP;

    private float recoverTimer = 0f;

    [SerializeField] private float recoverInterval;
    [SerializeField] private float recoverPercent;


    public sealed override Type RegistrationType => typeof(HitPointModule);

    public override void OnRegistration(CharacterBase newOwner)
    {
        base.OnRegistration(newOwner);
        _curMP = _maxMP;
        GameManager.OnUpdateCharacter -= RecoverMPUpdate;
        GameManager.OnUpdateCharacter += RecoverMPUpdate;
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnRegistration(oldOwner);
        GameManager.OnUpdateCharacter -= RecoverMPUpdate;
    }

    public void UseMP(float value)
    {
        if (_curMP >= MinMP) return;
        if (_curMP < value) return;
        _curMP -= value;
    }

    public void RegenerationMP()
    {
        if (IsDead) return;

        _curMP += _maxMP * recoverPercent;
        _curMP = Mathf.Min(_curMP, _maxMP);
    }

    public void Recover(float recover)
    {
        if (IsDead) return;

        _curMP += recover;
        _curMP = Mathf.Clamp(_curMP, MinMP, MaxMP);
    }

    // Increase : 증가하다
    public float IncreaseMP(float value)
    {
        _maxMP += value;
        return _maxMP;
    }

    // Decrease : 감소하다
    public float DecreaseMP(float value)
    {
        _maxMP -= value;
        if (_maxMP <= 1f) _maxMP = 1f;
        if (_curMP > _maxMP) _curMP = _maxMP;
        return _maxMP;
    }

    public void RecoverMPUpdate(float deltaTime)
    {
        if (IsDead) return;
        if (_curMP >= _maxMP) return;

        recoverTimer += deltaTime;

        if (recoverTimer >= recoverInterval)
        {
            recoverTimer = 0f;
            RegenerationMP();
        }

    }

}
