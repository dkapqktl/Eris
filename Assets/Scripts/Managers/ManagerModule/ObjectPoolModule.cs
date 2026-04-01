using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

            result.SetActive(true);

            Transform currentTransform = result.transform;
            Transform originTransform = Setting.target.transform;

            currentTransform.SetParent(parent); // 위치 크기 회전을 모두 부모기준으로
            
            if(currentTransform is RectTransform asRectTransform && originTransform is RectTransform originRectTransform)
            {
                // 앵커 복사해오기
                asRectTransform.anchorMin = originRectTransform.anchorMin;
                asRectTransform.anchorMax = originRectTransform.anchorMax;

                // 피벗도 복사해오기
                asRectTransform.pivot = originRectTransform.pivot;
                if (parent)
                {
                    LayoutRebuilder.ForceRebuildLayoutImmediate(parent.transform as RectTransform);
                }

                // stretch인것을 확인하는 법
                bool stretchX = asRectTransform.anchorMin.x != asRectTransform.anchorMax.x;
                bool stretchY = asRectTransform.anchorMin.y != asRectTransform.anchorMax.y;

                if(stretchX || stretchY)
                {
                    asRectTransform.offsetMax = originRectTransform.offsetMax;
                    asRectTransform.offsetMin = originRectTransform.offsetMin;

                    /* 여기는 잘 안쓰인다고 함 터질 수도 있기 때문에 상황에 따라 씀
                    if (stretchX)
                    {
                        asRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, originRectTransform.offsetMin.x, 0);
                        asRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, -originRectTransform.offsetMax.x, 0);
                    }
                    if (stretchY)
                    {
                        asRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, originRectTransform.offsetMin.y, 0);
                        asRectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, -originRectTransform.offsetMax.y, 0);
                    }
                    */
                }
                else
                {
                    asRectTransform.anchoredPosition = originRectTransform.anchoredPosition; 
                    asRectTransform.sizeDelta = originRectTransform.sizeDelta;
                }
            }
            else
            {
                currentTransform.localPosition = originTransform.localPosition;
            }

            currentTransform.localRotation = originTransform.localRotation;
            currentTransform.localScale = originTransform.localScale;

            
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
