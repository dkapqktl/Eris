using UnityEngine;

public class AttackModule : CharacterModule
{
    [SerializeField] private float _attackDamage = 1f;
    public float AttackDamage => _attackDamage;

    [SerializeField] private float _avilityPower = 1f;
    public float AvilityPower => _avilityPower;

    [SerializeField] private float _attackSpeed = 10f;
    public float AttackSpeed => _attackSpeed;

    [SerializeField] private float _criticalMultiple = 1.25f;
    public float CriticalMultiple => _criticalMultiple;

    [SerializeField] private float _criticalChance = 0f;
    public float CriticalChance => _criticalChance;

    [SerializeField] private float _defense = 0f;
    public float Defense => _defense;

    [SerializeField] private float _adPenetration = 0f;
    public float ADPenetration => _adPenetration;

    [SerializeField] private float _apPenetration = 0f;
    public float APPenetration => _apPenetration;

    [SerializeField] private float _buff = 0f;
    public float Buff => _buff;

    public float AD(float adAddBuff, float adMultiplierBuff)
    {
        float adDamage;

        adDamage = _attackDamage + adAddBuff * adMultiplierBuff;

        return adDamage;
    }

    public float APDeal(buff)
    {

    }

    public bool CriticalRandom()
    {
        int criticalvalue = Random.Range(1, 100);

        if (_criticalChance <= criticalvalue) return true;
        else return false;
    }

    public float CriticalDamage()
    {
        float criticalDeal;
        if (_criticalChance == 0f) return 1f;

        if (CriticalRandom()) criticalDeal = (_attackDamage + _avilityPower) * _criticalMultiple;
        else return criticalDeal = (_attackDamage + _avilityPower);

        return criticalDeal;
    }

    public float FinalDamage()
    {
        float finalDamage;
        float Criticalsuccess;

        if (CriticalRandom()) Criticalsuccess = (_attackPower + _magicPower) * _criticalMultiple;
        else Criticalsuccess = (_attackPower + _magicPower);

        finalDamage = Criticalsuccess;

        return finalDamage;
    }

    public void Attack()
    {
        HitPointModule target = null;
        target.TakeDamage(gameObject, Owner.Controller, FinalDamage());
    }

    public float Defence()
    {
        float def = (150f / (150f + _defense));
        return def;
    }

}
