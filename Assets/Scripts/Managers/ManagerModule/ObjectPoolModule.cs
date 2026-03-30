using UnityEngine;

public class ObjectPoolModule
{
    PoolSetting _setting;
    public PoolSetting Setting => _setting;

    Transform rootTransform; // 풀마다 루트 만들어서 관리하기

    Queue<GameObject>> prepareQueue = new();

    public ObjectPoolModule(PoolSetting newSetting)
    {
        _setting = newSetting;
    }

    public void Initialize()
    {
        // _setting.countInitial 만큼 만들어서 준비
        
        rootTransform = new GameObject(Setting.poolName).transform; // 풀마다 루트 만들어서 관리하기

        for (int i = 0; i < _setting.countInitial; i++) // 
        {
            PrepareObject();
        }
    }

    GameObject PreoareObject()
    {
        if(!Setting.target) return null;
        GameObject result = ObhectManager.CreateObject(Setting.target, rootTransform);

        if(result)
        {
            result.SetActive(false);
            result.name = Setting.poolName;
        }

        return result;
    }

}
