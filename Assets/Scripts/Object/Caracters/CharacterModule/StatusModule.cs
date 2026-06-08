using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StatusModule : CharacterModule
{
    HitPointModule HP;
    AttackModule Damage;

    private float _level;
    public float Level => _level;

    private float _strength;
    public float Strength => _strength;
    GetMaxHP => _maxHP + (_strength* 10)

    private float _agility;
    public float agility => _agility;

    private float _intelligence;
    public float Intelligence => _intelligence;

    public void LevelSystem()
    {
        if (_level >= 50) _level = 50;
        statusPoint();
    }

    public void statusPoint()
    {
    }

    // public void STR(float addSTR)
    // {
    //     if (HP == null) return;
    //     _strength += addSTR;
    //     float addHP = (_strength - beforeSTR) * 10;
    //     HP.MaxIncreaseHP(addHP);
    //     beforeSTR = _strength;
    // }

    public void buttonSTR(float addSTR)
    {
        HP.MaxIncreaseHP(addSTR));
    }
    
    //if (Damage == null) return;
    //    float addDamage = (_strength - beforeSTR) * 10;
    //    Damage.
    //


    }
}
