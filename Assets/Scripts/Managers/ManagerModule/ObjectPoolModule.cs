using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolModule
{
    PoolSetting _setting;
    public PoolSetting Setting => _setting;

    Transform rootTransform; // 풀마다 루트 만들어서 관리하기

    Queue<GameObject> prepareQueue = new();
    
    

    public ObjectPoolModule(PoolSetting newSetting)
    {
        _setting = newSetting;
    }

    public void Initialize()
    {
        // _setting.countInitial 만큼 만들어서 준비
        
        rootTransform = new GameObject(Setting.poolName).transform; // 풀마다 루트 만들어서 관리하기
        Setting.target?.TryAddComponent<PooledObject>();


        PrepareObjects(Setting.countInitial);
    }

    GameObject PrepareObject()
    {
        if(!Setting.target) return null;
        GameObject result = CreateFormPrefab();

        EnqueueObject(result);

        return result;
    }

    void PrepareObjects(uint count)
    {
        if (!Setting.target) return;
        for(uint i = 0; i < count; i++)
        {
            GameObject result = CreateFormPrefab();
            EnqueueObject(result);

        }
    }

    void PrepareObjects(uint count, out GameObject activeObject)
    {
        if (!Setting.target)
        {
            activeObject = null;
            return;
        }

        activeObject = CreateFormPrefab();

        for (uint i = 0; i < count; i++)
        {
            GameObject result = CreateFormPrefab();
            EnqueueObject(result);

        }
    }

    public GameObject CreateFormPrefab()
    {
        GameObject result = ObjectManager.CreateObject(Setting.target, rootTransform);
        if(result)
        {
            result.name = Setting.poolName;

            if (result.TryGetComponent(out PooledObject pool))
            {
                pool.OnEnqueueEvent -= DestroyObject;
                pool.OnEnqueueEvent += DestroyObject;
            }
        }
        return result;
    }

    private void DestroyObject(PooledObject target)
    {
        throw new NotImplementedException();
    }

    public GameObject CreateObject(Transform parent = null)
    {
        GameObject result;
        if(!prepareQueue.TryDequeue(out result))
        {
            PrepareObjects(Setting.countAdditional, out result);
        }

        if(result)
        {
            if(result.TryGetComponent(out PooledObject pool))
            {
                pool.OnDequeue();
            }
            result.transform.SetParent(parent);
            result.SetActive(true);
        }

        return result;
    }



    public void DestroyObject(GameObject target)
    {
        EnqueueObject(target);
        if(target)
        {
            target.transform.SetParent(rootTransform);
        }
    }

    public void EnqueueObject(GameObject target)
    {

        if (!target) return;
        
        target.SetActive(false);
        prepareQueue.Enqueue(target);
        
    }

}
