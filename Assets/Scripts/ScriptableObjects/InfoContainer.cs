using UnityEngine;

//[CreateAssetMenu(fileName = "InfoCuntainer", menuName = "Scriptable Objects/InfoCuntainer")]

// abstract 추상클래스 : 본인은 객체생성 불가능, 자식이 구현해야함 (본인은 new 못함)
public abstract class InfoContainer : ScriptableObject
{
    public Sprite icon;
    public string displayName;
    public string explain;


}
