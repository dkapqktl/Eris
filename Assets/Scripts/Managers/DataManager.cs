using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets; // UnityEngine에 AddressableAssets 파일 불러오기

// 이름 쓰기 귀찮을때 사용하는게 using => namespace 같은것
/*
namespace 에리니에스
{
  A;
}

A a;

*/

public class DataManager : ManagerBase
{
    // 딕셔너리 안에 딕셔너리
    //         어떤 종류인지
    static Dictionary<System.Type, Dictionary<string, Object>> dataDictionary = new();

    // 프로퍼티는 변수 모양이지만 함수
    //              int GetLoadCount => 100; 이게 실제 모양임
    public override int LoadCount
    {
        get
        {                           // 리소의 위치만 불러온다
            var task = Addressables.LoadResourceLocationsAsync("Global");
            var result = task.WaitForCompletion(); // WaitForCompletion => 이걸 끝낼때 까지 기다린다
            int count = result.Count; // 개수를 찾아오기

            // 위에서 열었다면
            task.Release(); // 닫기까지 해야함

            return count; // 찾은 개수를 돌려줌
        }

    }

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        UIBase loading = UIManager.ClaimGetUI(UIType.Loading);

        IProgress<int> progressUI = loading as IProgress<int>;
        IStatus<string> statusUI = loading as IStatus<string>;

        int loaded = 0;
        int total = LoadCount;
        string loadString = "Load Data";

        System.Action ProgressOnLoad = () =>
        {
            loaded++;
            progressUI?.AddCurrent(1);
            statusUI?.SetCurrentStatus($"{loadString}({loaded} / {total})");
        };


        // 그냥 함수를 실행하는 것이 아니라, 이 작업을 시작할 인원을 모 => 해당 스레드한테 시켜야 함
        // LoadFileFromAssetBundle<GameObject>("Original/Prefabs/Square.prefab");
        loadString = "Load Game Objects";
        yield return LoadAllFromAssetBundle<GameObject>("Global", ProgressOnLoad).WiatForTask(); // WiatForTask => Task가 끝날때 까지 기다리는 함수
        loadString = "Load Pool Requests";
        yield return LoadAllFromAssetBundle<PoolRequest>("Global", ProgressOnLoad).WiatForTask(); // WiatForTask => Task가 끝날때 까지 기다리는 함수


        /*
        GameObject prefad = LoadDataFile<GameObject>("square 17");
        Instantiate(prefad, Random.insideUnitCircle * 5.0f, Random.rotation);
        */


        if (TryGetFileFromResources("Prefabs/Square", out Sprite trash))
        {
            Debug.Log(trash);
        }
        // Interface : 상호작용, 연결고리 => 무엇이 무엇을 사용 할 수 있도록 열어주는 기능
        //             GUI : 그래픽 보여줌, 마우스 움직임, 누르기 떼기, 클릭하기, 드래그
        // 클릭이 있다 =>  GUI는 클릭이 가능하다 => GUI이기만 하면 클릭을 지원한다
        // 어떤 기능이 있을 거다 라는 약속이 Interface
        // IOpenable => 열기, 닫기, 토글, 열려있는지 확인도 가능!

        // 로딩 진행율을 보려면 => 최대 몇개인지, 현재 몇개했는지.
        //                       현재 / 최대 => 1 / 100 = 0.01

        // yield return new WaitForSeconds(0.5f); // 0.5초마다 한칸씩

        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    bool TryGetFileFromResources<T>(string path, out T result) where T : Object
    {
        result = Resources.Load<T>(path);
        return result != null; // result가 null 이 아니라면 ture
    }

    public static void SaveDataFile<T>(T target) where T : Object
    {
        if (target == null) return;
        Dictionary<string, Object> innerDictionary;

        // 
        if(!dataDictionary.TryGetValue(typeof(T), out innerDictionary)) // dataDictionary의 값을 ()에서 얻어오는걸 시도해라, 타입이 T인지 확인해라
        {
            innerDictionary = new();
            dataDictionary.Add(typeof(T), innerDictionary);
        }

        innerDictionary.TryAdd(target.name.ToLower(), target);
     }

    public static T LoadDataFile<T>(string fileName) where T : Object
    {
        if (string.IsNullOrEmpty(fileName) ) return null;


        fileName = fileName.ToLower();

        if (dataDictionary.TryGetValue(typeof(T), out Dictionary<string, Object> innerDictionary))
        {
            if(innerDictionary.TryGetValue(fileName, out Object result))
            {
                return result as T; // Object는 T타입으로 못만듦, result만 적으면 오류생김, as T를 써야함
                // as ~ : result를 T 처럼 봐라
            }
        }

        UIManager.ClaimErrorMessage(SystemMessage.FileNameNotFound(fileName));

        return null;
    }

    // Action 행동은 언제나 함수 => 반환값이 없는 함수
    // Action<int> => void function(int a)
    // Action<float> => void function(float a)
    // Action<int, float> => void function(int a, float b)
    public async Task LoadAllFromAssetBundle<T>(string label, System.Action actionForEachLoad) where T : Object
    {
        var finder = Addressables.LoadAssetsAsync<T>(label, (T loaded) =>
        {
            SaveDataFile(loaded);
            actionForEachLoad();
        }); // 불러오기를 시키고
        await finder.Task; // 파인더를 사용한다.
        finder.WaitForCompletion(); // 끝날때까지 기다린다.
        finder.Release();
    }

    // async 함수는 비동기 함수 => 다른 함수와 같이 돌아갈 수 있는 함수
    // Coroutine과 차이점 => Coroutine은 멀티 스레드가 아님
    // 동시에 하는 것처럼 보일뿐 이거하다 저거하다 이거하다 저거하다 함 => 너무 빨라서 사라지기 전에 돌아오기 때문에 같이하는 것처럼 보일 뿐
    public async void LoadFileFromAssetBundle<T>(string address) where T : Object
    {
        // Async 에서 A는 An => ~이 아닌, 반대되는 접두사
        var finder =  Addressables.LoadAssetAsync<GameObject>(address);
        await finder.Task;
        SaveDataFile(finder.Result);
        finder.Release();


        // Tan => ATan => 비동기, 동기화하지 않는다
        // Thread = 줄, 실 // 멀티스레드 <-> 싱글스레드
        // Thread > 한번에 실행하는 기능의 개수 // ex) 밥먹으면서 게임하면서 유튜브보면서 음악도 튼다
        
    }

}
