using UnityEngine;

[CreateAssetMenu(fileName = "PoolRequest", menuName = "PoolRequests/DefaultPoolRequest")]
public class PoolRequest : ScriptableObject // Pool을 요청[Request] 하다
{
    public PoolSetting[] settings;
}
