using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    private int _requiredExp => 100 + (level * 25); // 요구 경험치

    public int requiredExp => _requiredExp;


    public override void OnRegistration(CharacterBase newOwner)
    {
        base.OnRegistration(newOwner); // 캐릭터 생성시 아래것도 해줘

        if (newOwner)
        {
            status = newOwner.GetComponent<StatusModule>();
            HP = newOwner.GetComponent<HitPointModule>();
        }
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnRegistration(oldOwner); // 캐릭터 없앨때 아래것도 해줘
    }

    public void AddExp(int exp)
    {
        if (maxLevel <= _level) return;

        // if (exp <= 0) return;

        _currentExp += exp;

        while (_currentExp >= _requiredExp) // 혹시나 한번에 여러번 업 할수도 있으니
        {
            _currentExp -= _requiredExp;
            LevelUp();
        }

        OnExpChanged?.Invoke();
    }

    public void LevelUpUpdate() { LevelUp(); }
    private void LevelUp()
    {
        if (maxLevel <= _level) return;
        
        _level++;

        HP.FullHP();
        
        status.AddStatusPoint(5);

        OnLevelChanged?.Invoke();
    }

    public void LevelDownUpdate() { LevelDown(); }
    private void LevelDown()
    {
        if (maxLevel <= _level) return;

        _level--;

        HP.FullHP();

        status.AddStatusPoint(-5);

        OnLevelChanged?.Invoke();
    }

    private int GetRequiredExp(int level)
    {
        return level * 100;
    }
}