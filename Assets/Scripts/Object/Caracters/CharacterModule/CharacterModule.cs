using UnityEngine;

public class CharacterModule : MonoBehaviour
{

    //                사람
    //               새
    //              고래
    public virtual System.Type RegistrationType => typeof(CharacterModule);


    CharacterBase _owner;
    public CharacterBase Owner => _owner;


    public virtual void OnRegistration(CharacterBase newOwner) { _owner = newOwner; }
    public virtual void OnUnRegistration(CharacterBase oldOwner) { _owner = null; }


}
