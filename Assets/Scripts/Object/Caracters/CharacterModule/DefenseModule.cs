using System;
using UnityEngine;

public class DefenseModule : CharacterModule
{
    public sealed override Type RegistrationType => typeof(DefenseModule);

    [SerializeField] private float _defense = 0f;
    public float Defense => _defense;
}
