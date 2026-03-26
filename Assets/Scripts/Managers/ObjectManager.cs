using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

// 없을 수 있다 : class
// 없을 수 없다 : struct
[System.Serializable] // Serializable : 직렬화할수있는 // serial => 연속적인, 직렬연결되어있다. // ~lize => ~ 화 // ~able => ~할수있는
public struct PoolSetting
{
    public string poolName;
    public GameObject target; // 대상
    public int countInitial; // 처음 준비할 개수
    public int countAdditional; // 부족하면 추가 할 개수
}

public class ObjectManager : ManagerBase
{
    // public 과 상관없이 직렬화가 되면 유니티에서 볼 수 있음
    [SerializeField] PoolSetting[] testSettings;

    // IEnumerator 에는 반환값이 필요함, 그냥 return 이 아니라 yield return 을 써야함
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        yield return null;
    }

    protected override void OnDisconnected()
    {
        
    }

    public static GameObject CreateObject(GameObject prefab, Transform parent = null)
    {
        if (prefab == null) return null;

        GameObject result = Instantiate(prefab, parent); // 만들고
        RegistrationObject(result); // 등록함
        return result;
    }

    public static GameObject CreateObject(GameObject prefab, Vector3 position)
    {
        GameObject result = CreateObject(prefab);
        if (result) result.transform.position = position;
        return result;
    }

    public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject result = CreateObject(prefab);
        if (result)
        {
            result.transform.position = position;
            result.transform.rotation = rotation;
        }

        return result;
    }

    public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject result = CreateObject(prefab);
        if (result)
        {
            result.transform.position = position;
            result.transform.rotation = rotation;
            result.transform.localScale = scale;
        }

        return result;
    }


    public static GameObject CreateObject(GameObject prefab, Transform parent, Vector3 position, Space space = Space.Self)
    {
        GameObject result = CreateObject(prefab, parent);
        if (result)
        { 
            switch(space)
            {
                case Space.World : 
                    result.transform.position = position; // 절대값을 기준으로
                    break;
                
                case Space.Self :
                    result.transform.localPosition = position; // 부모를 기준으로
                    break;
            }
        }
        return result;
    }

    // 오브젝트의 크기는 건들면 안됨
    // 부모가 x 200 y 100, scale x 1 y 1 인 직사각형일때 자식들은 정사각형이여도 scale x 0.5 y 1 이 되버리기 때문
    public static GameObject CreateObject(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, Space space = Space.Self)
    {
        GameObject result = CreateObject(prefab, parent);
        if (result)
        {
            switch (space)
            {
                case Space.World:
                    result.transform.position = position; // 절대값을 기준으로
                    result.transform.rotation = rotation;
                    break;
                
                case Space.Self:
                    result.transform.localPosition = position; // 부모를 기준으로
                    result.transform.localRotation = rotation;
                    break;
            }
        }
        return result;
    }

    public static GameObject CreateObject(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, Vector3 scale, Space space = Space.Self)
    {
        GameObject result = CreateObject(prefab, parent);
        if (result)
        {
            switch (space)
            {
                case Space.World:
                    result.transform.position = position; // 부모를 기준으로
                    result.transform.rotation = rotation;
                    result.transform.localScale = scale;
                    // result.transform.position = position.transform.lossyScale; // 절대값을 기준으로
                    // result.transform.rotation = rotation.transform.lossyScale;
                    // float scaledScaleX = scale.x * (originLocalScale.x / originlossyScale.x);
                    // float scaledScaleX = scale.y * (originLocalScale.y / originlossyScale.y);
                    // float scaledScaleX = scale.z * (originLocalScale.z / originlossyScale.z);
                    // result.transform.localScale = scale; // 스케일은 절대값을 따질 수 없음 그래서 로컬스케일을 써야함
                    // lossy
                    break;
               
                case Space.Self:
                    result.transform.localPosition = position; // 부모를 기준으로
                    result.transform.localRotation = rotation;
                    result.transform.localScale = scale;
                    break;
            }
        }
        return result;
    }

    public static void RegistrationObject(GameObject target)
    {
        if (target)
        {
            // GetComponent                           => 컴포넌트를 가져옴(제일 첫번째 컴포넌트)
            // GetComponent<IFunctionable>            => IFunctionable 하나
            // GetComponents<IFunctionable>           => IFunctionable을 상속받는 모든 컴포넌트
            // GetComponentsInChild<IFunctionable>    => 나 포함 자식한테 있는 모든 컴포넌트
            // GetComponentsInChildren<IFunctionable> => 나 포함 자식들 한테 있는 모든 컴포넌트
            foreach (var current in target.GetComponentsInChildren<IFunctionable>())
            {
                current.RegistrationFunctions();
            }
        }
    }

    public static void DestroyObject(GameObject target)
    {
        if (!target) return;

        UnRegistrationObject(target); // 만들고
        Destroy(target);
    }

    public static void UnRegistrationObject(GameObject target)
    {
        if (!target)
        {
            // GetComponent                           => 컴포넌트를 가져옴(제일 첫번째 컴포넌트)
            // GetComponent<IFunctionable>            => IFunctionable 하나
            // GetComponents<IFunctionable>           => IFunctionable을 상속받는 모든 컴포넌트
            // GetComponentsInChild<IFunctionable>    => 나 포함 자식한테 있는 모든 컴포넌트
            // GetComponentsInChildren<IFunctionable> => 나 포함 자식들 한테 있는 모든 컴포넌트
            foreach (var current in target.GetComponentsInChildren<IFunctionable>())
            {
                current.UnRegistrationFunctions();
            }
        }
    }

}
