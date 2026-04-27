using System.Collections.Generic;
using UnityEngine;

public delegate void MovementEvent(Vector3 move);
public delegate void LookAtEvent(Vector3 direction);
public delegate void DamageEvent(GameObject damageCause, ControllerBase instigator, float damage);

public class CharacterBase : MonoBehaviour
{

    public MovementEvent OnMovement;
    public void MovementNotify(Vector3 move) => OnMovement?.Invoke(move);


    public LookAtEvent OnLookAt;
    public void LookAtNotify(Vector3 direction) => OnMovement?.Invoke(direction);


    public DamageEvent OnDamage;
    public void DamageNotify(GameObject damageCauser, ControllerBase instigator, float damage)
        => OnDamage?.Invoke(damageCauser, instigator, damage);

     
    ControllerBase _controller;
    public ControllerBase Controller => _controller;

    protected Vector3 _lookRotation;
    protected Vector3 LookRotation => _lookRotation;

    public virtual string DisplayName => "character";

    Dictionary<System.Type, CharacterModule> moduleDictionary = new();

    public void AddModule(System.Type wantType, CharacterModule wantModule)
    {
        if(moduleDictionary.TryAdd(wantType, wantModule))
        {
            wantModule.OnRegistration(this);
        }
    }

    public void AddAllModuleFromObject(GameObject target)
    {
        if (!target) return;

        foreach(CharacterModule currentModule in target.GetComponentsInChildren<CharacterModule>())
        {
            AddModule(currentModule.RegistrationType, currentModule);
        }
    }

    public void RemoveModule(System.Type wantType)
    {
        if(moduleDictionary.ContainsKey(wantType))
        {
            moduleDictionary[wantType]?.OnUnRegistration(this);
            moduleDictionary.Remove(wantType);
        }

    }

    public void RemoveAllModule()
    {
        foreach (CharacterModule currentModule in moduleDictionary.Values)
        {
            currentModule.OnUnRegistration(this);
        }
        moduleDictionary.Clear();
    }

    public T GetModule<T>() where T : CharacterModule
    {
        moduleDictionary.TryGetValue(typeof(T), out CharacterModule result);
        return result as T;
    }


    //확장은 가능한데 수정은 불가능한 원칙
    public virtual void OnPossessed(ControllerBase newController) { }
    //                 빙의되다, 소유되다
    public ControllerBase Possessed(ControllerBase form) // Possessed 이 함수는 form을 입력 받는다
    {
        // if(_controller) 컨트롤러가 이미 있었다면
        // Unpossessed(); 해지한다.
        if (Controller) Unpossessed();
        _controller = form; // 컨트롤러는 그 입력받은 프롬이다
        AddAllModuleFromObject(gameObject);
        OnPossessed(Controller);
        return Controller; // 그리고 컨트롤러를 반환해라

    }




    //확장은 가능한데 수정은 불가능한 원칙
    public virtual void OnUnpossessed(ControllerBase oldController) { }
    //           혼이 나가다
    public void Unpossessed()
    {
        if (Controller) OnUnpossessed(Controller);
        RemoveAllModule();
        _controller = null; // 컨트롤러는 없다
    }

    public bool Unpossessed(ControllerBase oldController)
    {
        if (Controller != oldController) return false; // 컨트롤러가 oldController가 아니라면 (즉 newController) 라면 거짓 반환
        Unpossessed(); // 그게 아니라면 언포제시드 함수 실행하고

        return true; //  참을 반환
    }

}