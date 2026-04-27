using UnityEngine;

public class CharacterModule : MonoBehaviour
{
    public virtual System.Type RegistrationType => typeof(CharacterModule);

    public CharacterBase _owner;
    public CharacterBase Owner => _owner;

    public virtual void OnRegistration(CharacterBase newOwner) { _owner = newOwner; }
    public virtual void OnUnRegistration(CharacterBase oldOwner) { _owner = null; }

}
