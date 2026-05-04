using UnityEngine;

public class AttackModule : CharacterModule
{
    [SerializeField] private float _attackPower = 0;
    public float AttackPower => _attackPower;

    [SerializeField] private float _magicPower = 0;
    public float MagicPower => _magicPower;

    [SerializeField] private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;

    [SerializeField] private float _criticalMultiple;
    public float CriticalMultiple => _criticalMultiple;

    [SerializeField] private float _criticalChance;
    public float CriticalChance => _criticalChance;

    public bool CriticalRandom()
    {
        int criticalvalue = Random.Range(0, 100);

        if (_criticalChance <= criticalvalue) return true;
        else return false;
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

}
