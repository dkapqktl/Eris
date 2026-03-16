using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // static : 프로그램에서 딱 하나뿐
    static GameManager _instance;

    public static GameManager Instance => _instance;

    UIManager _ui;
    public UIManager UI => _ui;

    DataManager     _data;
    public DataManager Data => _data;

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
    }

    

    void InitializeManager()
    {

        CreateManager(ref _ui);
        CreateManager(ref _data);
        CreateManager(ref _save);
        CreateManager(ref _setting);
        CreateManager(ref _language);
        CreateManager(ref _audio);
        CreateManager(ref _camera);
        CreateManager(ref _input);

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

    // 반환값 이름<자료형>(매개변수) where 자료형 : 부모
    // 원본 값이랑 연결되는 변수

    // 매니저를 만들어 줄껀데 
    ManagerType CreateManager<ManagerType>(ref ManagerType targetVariable) where ManagerType : ManagerBase
    {
        if (targetVariable == null)
        {
            targetVariable = gameObject.AddComponent<ManagerType>();
            targetVariable.Connect(this);
        }

        return targetVariable;
    }

    void Update()
    {
        
    }
}
