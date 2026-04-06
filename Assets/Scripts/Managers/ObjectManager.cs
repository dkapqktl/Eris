using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class ObjectManager : ManagerBase
{

    readonly string[] globalpoolSettings =
    {
        "GlobalCharacterPool",
        "GlobalControllerPool",
        "GlobalEffectPool",
        "GlobalObjectPool",
        "GlobalUIPool"
    };

    // public 과 상관없이 직렬화가 되면 유니티에서 볼 수 있음
    // [SerializeField] PoolSetting[] testSettings;

    // 리스트 : 추가제거 쉬운 대신 용량↑ / 찾는 속도 느림 / 추가제거 많고 전체를 도는일이 적을때
    // 배열 : 추가제거 어려운 대신 용량↓ / 찾는 속도 빠름 / 추가제거 적고 전체를 도는일이 많을때

    List<PoolRequest> loadedPoolRequest = new();

    static Dictionary<string, ObjectPoolModule> poolDictionary = new();


    // IEnumerator 에는 반환값이 필요함, 그냥 return 이 아니라 yield return 을 써야함
    protected override IEnumerator OnConnected(GameManager newManager)
    {
        RegistrationPool(globalpoolSettings);
        InitializePool();

        yield return null;
    }
    
    // OnConnected => 연결이 되었을 때 => 시작할 때 => 초기화 할 때 => 코루틴으로 만들어야함 => IEnumerator
    // OnDisconnected => 연결이 끊겼을 때 => 끝날 때 => 정리할 때
    // 코루틴이란? => 중간에 멈췄다가 다시 시작할 수 있는 함수 => IEnumerator => yield return => 멈추는 지점
    // 코루틴을 쓰는 이유? => 시간이 걸리는 작업을 나눠서 처리할 수 있음 => 프레임이 끊기는 것을 방지할 수 있음
    // 프레임이란 => 1초에 몇번 화면이 갱신되는지 => 60프레임 => 1초에 60번 화면이 갱신됨 => 1프레임 = 1/60초 => 16.67ms => 16.67ms 이상 걸리는 작업이 있으면 프레임이 끊김 => 프레임 드랍
    // 프레임 드랍 => 게임이 끊기는 것 => 플레이어가 불편함 => 프레임 드랍을 방지하기 위해 코루틴을 사용함


    protected override void OnDisconnected()
    {
        
    }

    /*
    public static GameObject GetObject(string wantName)
    {
        if(poolDictionary.TryGetValue(wantName, out ObjectPoolModule pool))
        {
            return pool.CreateObject();
        }
        
        GameObject prefab = DataManager.LoadDataFile<GameObject>(wantName);
        
        if (prefab)
        {
            return Instantiate(DataManager.LoadDataFile<GameObject>(wantName));
        }

        return null;
    }
    */

    public static GameObject CreateObject(string wantName, Transform parent = null)
    {
        GameObject result = null;

        wantName = wantName.ToLower(); // 싹다 소문자화 시키기

        if (poolDictionary.TryGetValue(wantName, out ObjectPoolModule pool))
        {
            result = pool.CreateObject(parent);
        }

        else
        {
            if (DataManager.TryLoadDataFile<GameObject>(wantName, out GameObject prefab))
            {
                if (prefab) result = Instantiate(prefab, parent);
            }
      
        }

        if (!result) UIManager.ClaimErrorMessage(SystemMessage.ObjectNameNotFound(wantName));

        RegistrationObject(result);

        return result;
    }
    public static GameObject CreateObject(GameObject prefab, Transform parent = null)
    {
        if (prefab == null) return null;

        GameObject result = Instantiate(prefab, parent); // 만들고
        RegistrationObject(result); // 등록함
        return result;
    }

    public static GameObject CreateObject(string wantName, Vector3 position)
    {
        GameObject result = CreateObject(wantName);
        if (result) result.transform.position = position;
        return result;
    }
    public static GameObject CreateObject(GameObject prefab, Vector3 position)
    {
        GameObject result = CreateObject(prefab);
        if (result) result.transform.position = position;
        return result;
    }


    public static GameObject CreateObject(string wantName, Vector3 position, Quaternion rotation)
    {
        GameObject result = CreateObject(wantName);
        if (result)
        {
            result.transform.position = position;
            result.transform.rotation = rotation;
        }

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


    public static GameObject CreateObject(string wantName, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject result = CreateObject(wantName);
        if (result)
        {
            result.transform.position = position;
            result.transform.rotation = rotation;
            result.transform.localScale = scale;
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


    public static GameObject CreateObject(string wantName, Transform parent, Vector3 position, Space space = Space.Self)
    {
        GameObject result = CreateObject(wantName, parent);
        if (result)
        {
            switch (space)
            {
                case Space.World:
                    result.transform.position = position; // 절대값을 기준으로
                    break;

                case Space.Self:
                    result.transform.localPosition = position; // 부모를 기준으로
                    break;
            }
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



    public static GameObject CreateObject(string wantName, Transform parent, Vector3 position, Quaternion rotation, Space space = Space.Self)
    {
        GameObject result = CreateObject(wantName, parent);
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



    public static GameObject CreateObject(string wantName, Transform parent, Vector3 position, Quaternion rotation, Vector3 scale, Space space = Space.Self)
    {
        GameObject result = CreateObject(wantName, parent);
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

    public static void RegistrationObject(GameObject target) // 실제로 등록하는 기능
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
        UnRegistrationObject(target);
        if (target.TryGetComponent(out PooledObject pool))
        {
            pool.OnEnqueue();
        }

        else
        {
            Destroy(target);
        }

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

    public void RegistrationPool(string poolName)
    {
        poolName = poolName.ToLower();

        PoolRequest currentRequest = DataManager.LoadDataFile<PoolRequest>(poolName);
        if (currentRequest == null) return;
        if (currentRequest.settings == null) return;

        loadedPoolRequest.Add(currentRequest);
        //        학생          다음학생    in     3학년 1반
        foreach (PoolSetting currentSetting in currentRequest.settings)
        {
            string currentName = currentSetting.poolName.ToLower();
            GameObject currentPrefab = currentSetting.target;
            if (currentPrefab == null) continue; // 없다면 건너뛰기
            if (poolDictionary.ContainsKey(currentName)) continue; // 이름이 같다면 건너뛰기
            poolDictionary.Add(currentName, new(currentSetting));
        }
    }

    public void RegistrationPool(params string[] poolNames)
    {

        foreach (string poolName in poolNames)
        {
            RegistrationPool(poolName);
        }
    }

    public void InitializePool()
    {
        foreach(ObjectPoolModule currentPool in poolDictionary.Values)
        {
            currentPool.Initialize();
        }
    }
}
