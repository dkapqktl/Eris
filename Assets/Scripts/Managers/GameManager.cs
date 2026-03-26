using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public delegate void InitializeEvent();
public delegate void UpdateEvent(float deltaTime); // ex) 3km/h 인 속도로 1시간 30분 정도 가면 몇 키로를 가는지? => 4.5km // 즉 변화량을 시간단위로
public delegate void DestroyEvent();
// public delegate void OnEnableEvent();
// public delegate void OnDisableEvent();


public class GameManager : MonoBehaviour
{
    // static : 프로그램에서 딱 하나뿐
    static GameManager _instance;

    public static GameManager Instance => _instance;

    UIManager       _ui;
    public UIManager UI => _ui;

    DataManager     _data;
    public DataManager Data => _data;

    ObjectManager _objectM;
    public ObjectManager ObjectM => _objectM;

    SaveManager     _save;
    public SaveManager Save => _save;
    
    SettingManager  _setting;
    public SettingManager Setting => _setting;

    LanguageManager _language;
    public LanguageManager Language => _language;

    AudioManager    _audio;
    public AudioManager Audio => _audio;

    CameraManager  _camera;
    public CameraManager Camera => _camera;

    InputManager    _input;
    public InputManager Input => _input;

    IEnumerator initializing; // 초기화 중 코루틴

    public static event InitializeEvent OnInitializeManager;      // 매니저 초기화
    public static event InitializeEvent OnInitializeController;   // 컨트롤러 초기화
    public static event InitializeEvent OnInitializeCharacter;    // 캐릭터 초기화
    public static event InitializeEvent OnInitializeObject;       // 오브젝트 초기화

    public static event UpdateEvent OnUpdateManager;          // 매니저 업데이트
    public static event UpdateEvent OnUpdateController;       // 컨트롤러 업데이트
    public static event UpdateEvent OnUpdateCharacter;        // 캐릭터 업데이트
    public static event UpdateEvent OnUpdateObject;           // 오브젝트 업데이트

    public static event DestroyEvent OnDestroyObject;           // 오브젝트 제거
    public static event DestroyEvent OnDestroyCharacter;        // 캐릭터 제거
    public static event DestroyEvent OnDestroyController;       // 컨트롤러 제거
    public static event DestroyEvent OnDestroyManager;          // 매니저 제거

    bool isLoading = true;
    bool isPlaying = true;

    // Awake     : 프로그램이 시작할 때 (아침에 일어나 뇌 부팅중)
    // OnEnabled : 프로그램이 시작할 때 (뇌가 활성화 됨)
    // Reset     : 프로그램 시작을 위한 초기화
    // Start     : 프로그램이 시작할 때 (뇌가 생각을 시작함)
    void Awake()
    {
        if (Instance == null) // 지금 게임메니저가 없음
        {
            _instance = this; // 게임메니저는 나다
        }
        else // 지금 게임메니저가 있다면
        {
            Destroy(this); // 게임메니저를 없애라
            return; // 부서지면 이후 InitializeManager가 실행하지 못하도록 return 을 적음
        }

        // 게임메니저를 많이 넣어놔도 하나만 남기고 다 사라짐

        // 게임에서 관리 되어야 하는것들을의 총관리 : 게임매니저
        // UI
        // 데이터파일
        // 세이브
        // 세팅
        // 언어
        // 오디오
        // 카메라
        // 유저입력 등등
        initializing = InitializeManagers(); // 반환형식은 IEnumerator => 반복자, 반복해서 함수가 실행 => 프레임 단위로 기다렸다가 실행
        // 한번 실행하고 [ yield ] 양보 했다가 다음 프레임에 또 나와서 실행하고 반복

        // Coroutine : Co(함께) routine(루틴) 의 합성어, 루틴을 협력적으로 실행
        StartCoroutine(initializing);
        // StartCoroutine("InitializeManagers"); 이건 안좋다고 함, 그래서 initializing = InitializeManagers(); 처럼 저장해서 쓰는게 좋다고 함
        // initializing을 StartCoroutine 반복해서 실행해줘

        UIManager.ClaimCloseUI(UIType.Loading);

    }

    void OnDestroy()
    {
        if(initializing != null) StopCoroutine(initializing); // 로딩이 진행 중이였다면 끊어버릴 수 있도록
        DeleteManager();
    }

    IEnumerator InitializeManagers()
    {
        int tatalLoadCount = 0;

        tatalLoadCount += CreateManager(ref _ui).LoadCount; 
        tatalLoadCount += CreateManager(ref _data).LoadCount;
        tatalLoadCount += CreateManager(ref _objectM).LoadCount;
        tatalLoadCount += CreateManager(ref _save).LoadCount;
        tatalLoadCount += CreateManager(ref _setting).LoadCount;
        tatalLoadCount += CreateManager(ref _language).LoadCount;
        tatalLoadCount += CreateManager(ref _audio).LoadCount;
        tatalLoadCount += CreateManager(ref _camera).LoadCount;
        tatalLoadCount += CreateManager(ref _input).LoadCount;


        //             UI를 불러와서 게임매니저(this) 연결한다(connect)
        yield return _ui.Connect(this); // 로딩하려면 UI필요
        UIBase loadingUI = UIManager.ClaimOpenUI(UIType.Loading); // UI System 이 돌아가기 시작했으니 기능 실행해보기!
        IProgress<int> loadingProgress = loadingUI as IProgress<int>; // 이 유아이가 아이프로그래스라면

        loadingProgress?.Set(0, tatalLoadCount);
        yield return _data.Connect(this); // 게임 데이터 불러오기
        loadingProgress?.AddCurrent(1);
        yield return _objectM.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _save.Connect(this); // 저장
        loadingProgress?.AddCurrent(1);
        yield return _setting.Connect(this); // 세팅 이후 아래것들
        loadingProgress?.AddCurrent(1);
        yield return _language.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _audio.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _camera.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return _input.Connect(this);
        loadingProgress?.AddCurrent(1);
        yield return new WaitForSeconds(1.0f);
        UIManager.ClaimCloseUI(UIType.Loading);
        isLoading = false;
        /* 없애도 됨
        if (_ui == null)
        {
            _ui = gameObject.AddComponent<UIManager>();
            _ui.Connect(this);
        }

        if(_data == null)
        {
            _data = gameObject.AddComponent<DataManager>();
            _data.Connect(this);
        }

        if (_save == null)
        {    
            _save = gameObject.AddComponent<SaveManager>();
            _save.Connect(this);
        }

        if (_setting == null)
        {
            _setting = gameObject.AddComponent<SettingManager>();
            _setting.Connect(this);
        }

        if (_language == null)
        {
            _language = gameObject.AddComponent<LanguageManager>();
            _language.Connect(this);
        }

        if (_audio == null)
        {
            _audio = gameObject.AddComponent<AudioManager>();
            _audio.Connect(this);
        }

        if (_camera == null)
        {
            _camera = gameObject.AddComponent<CameraManager>();
            _camera.Connect(this);
        }

        if (_input == null)
        {
            _input = gameObject.AddComponent<InputManager>();
            _input.Connect(this);
        }
        */
    }

    void DeleteManager()
    {
        Input?.Disconnect();
        ObjectM?.Disconnect();
        Audio?.Disconnect();
        Language?.Disconnect();
        Setting?.Disconnect();
        Save.Disconnect();
        Camera?.Disconnect();
        UI?.Disconnect();
        Data?.Disconnect();
    }

    // 반환값 이름<자료형>(매개변수) where 자료형 : 부모
    // 원본 값이랑 연결되는 변수

    // 매니저를 만들어 줄껀데 
    ManagerType CreateManager<ManagerType>(ref ManagerType targetVariable) where ManagerType : ManagerBase
    {
        if (targetVariable == null)
        {
            targetVariable = this.TryAddComponent<ManagerType>();
            targetVariable.Connect(this);
        }

        return targetVariable;
    }

    public static void Pause()
    {
        Instance.isPlaying = false;
    }


    public static void UnPause()
    {
        Instance.isPlaying = true;
    }


    public void InvokeInitializeEvent(ref InitializeEvent OriginEvent)
    {
        if (OriginEvent != null) // 이벤트가 있으면 실행
        {
            InitializeEvent currentEvent = OriginEvent; // 원본은 저장해두고
            OriginEvent = null; // 기존껀 초기화
            currentEvent.Invoke(); // 원본은 실행
        }
    }

    public void InvokeDestroyEvent(ref DestroyEvent OriginEvent)
    {
        if (OriginEvent != null) // 이벤트가 있으면 실행
        {
            DestroyEvent currentEvent = OriginEvent; // 원본은 저장해두고
            OriginEvent = null; // 기존껀 초기화
            currentEvent.Invoke(); // 원본은 실행
        }
    }

    void Update()
    {
        if (isLoading) return;
        // 게임 진행을 할 수 있는지 조정 => 초기화 해야하는지 말아야하는지
        // 퍼지 (Pause), 로딩 (Loading) 등의 업데이트를 하면 안되는 상황에서 업데이트 하지 않도록

        InvokeInitializeEvent(ref OnInitializeManager);
        InvokeInitializeEvent(ref OnInitializeCharacter);
        InvokeInitializeEvent(ref OnInitializeController);
        InvokeInitializeEvent(ref OnInitializeObject);


        float deltaTime = Time.deltaTime;

        if (isPlaying)
        {
            OnUpdateManager?.    Invoke(deltaTime);
            OnUpdateCharacter?.  Invoke(deltaTime);
            OnUpdateController?. Invoke(deltaTime);
            OnUpdateObject?.     Invoke(deltaTime);
        }

        InvokeDestroyEvent(ref OnDestroyObject);
        InvokeDestroyEvent(ref OnDestroyCharacter);
        InvokeDestroyEvent(ref OnDestroyController);
        InvokeDestroyEvent(ref OnDestroyManager);


    }
}
