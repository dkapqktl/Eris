using UnityEngine;
[System.Serializable] // Serializable : 직렬화할수있는 // serial => 연속적인, 직렬연결되어있다. // ~lize => ~ 화 // ~able => ~할수있는
public struct PoolSetting
{
    public string poolName;
    public GameObject target; // 대상
    public uint countInitial; // 처음 준비할 개수
    public uint countAdditional; // 부족하면 추가 할 개수
}


[CreateAssetMenu(fileName = "PoolRequest", menuName = "PoolRequests/DefaultPoolRequest")]
public class PoolRequest : ScriptableObject // Pool을 요청[Request] 하다
{
    public PoolSetting[] settings;
}
// 없을 수 있다 : class
// 없을 수 없다 : struct
