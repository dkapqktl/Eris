using System.Linq.Expressions;
using UnityEngine;

public class DefenceModule : CharacterModule
{
    public sealed override System.Type RegistrationType => typeof(DefenceModule);

    private float _defence = 0f;
    public float Defence => _defence;

    private float _evasion;
    public float Evasion => _evasion;

    AttackModule isAttack;

    public float DamageReduction => Defence == 0 ? 0 : Defence / (Defence + 100);

    public bool EvasionChance()
    {
        if (Evasion == 0) return false;
        int evasionChance = Random.Range(1, 100);
        if (Evasion >= evasionChance) return true;
        else return false;
    }

    public float Defensive()
    {
        if (EvasionChance()) return 0f;
        return 1 - DamageReduction;
    }
}