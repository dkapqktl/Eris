using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleModule : CharacterModule
{
    public sealed override Type RegistrationType => typeof(BattleModule);

    private float inBattleTime;
    private const float inBattleDuration = 10f;

    public bool isInBattle => inBattleTime > 0;

    public override void OnRegistration(CharacterBase newOwner)
    {
        base.OnRegistration(newOwner);
        Owner.OnDamage += InBattle;
        GameManager.OnUpdateCharacter -= BattleUpdate;
        GameManager.OnUpdateCharacter += BattleUpdate;
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnRegistration(oldOwner);
        if (!oldOwner) Owner.OnDamage -= InBattle;
        GameManager.OnUpdateCharacter -= BattleUpdate;
    }

    public void InBattle(GameObject damageCauser, ControllerBase instigator, float damage)
    {
        inBattleTime = inBattleDuration;
    }

    public void BattleUpdate(float deltaTime)
    {
        if (isInBattle)
        {
            inBattleTime -= deltaTime;
            inBattleTime = Mathf.Max(inBattleTime, 0);
        }
    }
}
