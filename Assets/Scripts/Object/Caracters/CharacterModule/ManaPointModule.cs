using System;
using UnityEngine;

public class ManaPointModule : CharacterModule
{

    HitPointModule deadCheck;

    [SerializeField] private bool invincibility;
    [SerializeField] private float _maxMP = 100f;

    public float MaxMP => _maxMP;
    public float minMP => 0f;


    private float _curMP;
    public float CurMP => _curMP;

    private float recoverTimer = 0f;

    private bool isDead => deadCheck.IsDead;

    [SerializeField] private float recoverInterval = 0.1f;
    [SerializeField] private float recoverPercent = 0.005f;


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
        if (_curMP >= minMP) return;
        if (_curMP < value) return;
        _curMP -= value;
    }

    public float IncreaseMP(float value) => _maxMP += value;    // Increase : 증가하다

    public float DecreaseMP(float value) => Mathf.Max(_maxMP - value, minMP);    // Decrease : 감소하다

    public bool canRecover => !isDead && _curMP >= _maxMP;
    public void Recover(float recover)
    {
        if (!canRecover) return;

        _curMP += recover;
        _curMP = Mathf.Clamp(_curMP += recover, minMP, MaxMP);
    }
    public void RegenerationMP()
    {
        if (!canRecover) return;

        _curMP = Mathf.Min(_curMP + (_maxMP * recoverPercent), _maxMP);
    }
    public void RecoverMPUpdate(float deltaTime)
    {
        if (!canRecover) return;

        recoverTimer += deltaTime;

        if (recoverTimer >= recoverInterval)
        {
            recoverTimer = 0f;
            RegenerationMP();
        }
    }


}
