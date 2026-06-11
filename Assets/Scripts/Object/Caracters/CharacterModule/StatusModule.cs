using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System;

public delegate void StatusChangeEvent();

public class StatusModule : CharacterModule
{
    HitPointModule HP;
    AttackModule Damage;
    UI_Button_StatusUI StatusButton;

    public sealed override Type RegistrationType => typeof(StatusModule);

    public event StatusChangeEvent OnStatusChanged;

    private int baseStrength;
    private int _strength => baseStrength; // 나중에 아이템 만들면 아이템쪽에 addSTR 같은거 만들어서 baseStrength + item.addSTR 이렇게
    public int Strength => _strength;


    private int baseDexterity;
    private int _dexterity => baseDexterity;
    public int Dexterity => _dexterity;


    private int baseIntelligence;
    private int _intelligence => baseIntelligence;
    public int Intelligence => _intelligence;


    private int _statusPoint;
    public int StatusPoint => _statusPoint;

    public int AllStatus => baseStrength + baseDexterity + baseIntelligence;
    public bool CanUseStatusPoint => _statusPoint > 0;
    public bool CanUseReset => AllStatus > 0;

    public void AddStatusPoint(int addPoint)
    {
        if (addPoint <= 0) return;

        _statusPoint += addPoint;

        OnStatusChanged?.Invoke();
    }

    public bool UseStatusPoint()
    {
        if (_statusPoint <= 0) return false;

        _statusPoint--;

        OnStatusChanged?.Invoke();

        return true;
    }

    public void IncreaseStrength()
    {
        if (!UseStatusPoint()) return;

        baseStrength++;

        OnStatusChanged?.Invoke();
    }

    public void FiveIncreaseStrength()
    {

        int value = Mathf.Min(StatusPoint, 5);
        if (value <= 0) return;
        _statusPoint -= value;
        baseStrength += value;

        OnStatusChanged?.Invoke();
    }

    public void IncreaseDexterity()
    {
        if (!UseStatusPoint()) return;

        baseDexterity++;

        OnStatusChanged?.Invoke();
    }

    public void IncreaseIntelligence()
    {
        if (!UseStatusPoint()) return;

        baseIntelligence++;

        OnStatusChanged?.Invoke();
    }

    public void ResetStatusPoint()
    {
        if (!CanUseReset) return;

        int statusPoint = AllStatus;
        baseStrength = 0;
        baseDexterity = 0;
        baseIntelligence = 0;

        AddStatusPoint(statusPoint);

        OnStatusChanged?.Invoke();
    }

}
