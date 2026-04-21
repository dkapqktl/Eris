using UnityEngine;

public class ControllerBase : MonoBehaviour, IFunctionable
{
    CharacterBase _character;
    public CharacterBase Character => _character;

    public void Start()
    {
        GameManager.OnInitializeController += RegistrationFunctions;
    }

    public virtual void RegistrationFunctions()
    {
        // 나랑 같은 오브젝트에 들어있는 캐릭터에 빙의하고 싶다.
        Possess(GetComponent<CharacterBase>());
    }

    public virtual void UnRegistrationFunctions()
    {
        Unpossess();
    }


    protected virtual void OnPossess(CharacterBase newCharacter) { }
    public void Possess(CharacterBase target)
    {
        if (!target) return; // 타겟이 없을땐 아무일 없다
        //  빙의된 컨트롤러                빙의      나
        ControllerBase result = target.Possessed(this); // 
        // 리절트가 나라면 빙의를 시켜라
        if (result == this)
        {
            _character = target;
            OnPossess(target);
        }
    }

    protected virtual void OnUnpossess(CharacterBase oldCharacter) { }
    public void Unpossess()
    {
        if (Character)
        {

            if (Character.Unpossessed(this))
            {
                OnPossess(Character);
            }
        }
        _character = null;
    }



    public void CommandMoveToDirection(Vector3 direction)
    {
        if (Character is IRunnable target) target.MoveToDirection(direction);
    }
    public void CommandMoveToDestination(Vector3 destination, float tolerance)
    {
        if (Character is IRunnable target) target.MoveToDestination(destination, tolerance);
    }
    public void CommandStop()
    {

    }


}
