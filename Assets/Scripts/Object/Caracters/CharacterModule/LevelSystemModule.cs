using UnityEngine;
using System;

public delegate void LevelUpEvent();
public delegate void ExpChangeEvent();

public class LevelSystemModule : CharacterModule
{
    public sealed override Type RegistrationType => typeof(LevelSystemModule);

    public event LevelUpEvent OnLevelChanged;
    public event ExpChangeEvent OnExpChanged;

    HitPointModule HP;
    StatusModule status;



    private int _level = 1; // 레벨
    public int level => _level;

    private int maxLevel = 60;


    private int _currentExp; // 현재 경험치
    public int currentExp => _currentExp;


    private int requiredExp => 100 + (level * 25); // 요구 경험치

    public void AddExp(int exp)
    {
        if (maxLevel <= _level) return;

        if (exp <= 0) return;

        _currentExp += exp;

        while (_currentExp >= requiredExp) // 혹시나 한번에 여러번 업 할수도 있으니
        {
            _currentExp -= requiredExp;
            LevelUp();
        }

        HP.FullHP();

        OnExpChanged?.Invoke();
    }

    private void LevelUp()
    {
        if (maxLevel <= _level) return;
        
        _level++;

        status.AddStatusPoint(5);

        OnLevelChanged?.Invoke();
    }

    private int GetRequiredExp(int level)
    {
        return level * 100;
    }
}