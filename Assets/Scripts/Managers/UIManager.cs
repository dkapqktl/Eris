using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum UIType
{
    None, Loading, Title, Option, LoadingText, Movable, Menu, Info, GameQuit, Target, Inventory, Ingame,
    LoadList,
    _Length
}

public enum ScreenChangeType
{
    None,
    ScreenChanger, FadeChanger,
    _Length
}

// 팝업이 일어나는 이벤트 발생
// 델리게이트 => 스킬을 무한이 배운다
// A 스킬과 B스킬을 쓰면 맨마지막 결과인 B스킬만 나옴
public delegate void PopUpEvent(string tilte, string context, string confirm);

public class UIManager : ManagerBase
{
    // event => 등록할 수 있는데 실행은 여기서만
    public static event PopUpEvent OnPopUp;

    readonly KeyValuePair<UIType, string>[] glovalScreenArray =
    {
        new (UIType.Title, "TitleScreen"),
        new (UIType.Ingame, "IngameScreen"),
        new (UIType.Option, "OptionUI"),
        new (UIType.GameQuit, "Exit_UI"),
        new (UIType.LoadList, "LoadUI"),
        new (UIType.Inventory, "InventoryUI")
    };

    Canvas _mainCanvas;
    public Canvas MainCanvas => _mainCanvas;

    UIBase _movableScreen;

    RectTransform switcherTransform;
    RectTransform createdTransform;
    RectTransform changerTransform;

    GraphicRaycaster _raycaster;
    public GraphicRaycaster Raycaster => _raycaster;

    // 찾아주세요! 이 타입이면 이 오브젝트 입니다.
    Dictionary<UIType, UIBase> uiDictionary = new();

    Dictionary<ScreenChangeType, UI_ScreenChanger> screenChangerDictionary = new();

    Rect _uiBoundary;
    public static Rect UIBoundary => GameManager.Instance?.UI?._uiBoundary ?? Rect.zero;

    UI_ScreenChanger currentScreenChanger;

    UIType _currentScreenType = UIType.None;
    public static UIType CurrentScreen => GameManager.Instance?.UI?._currentScreenType ?? UIType.None;



    float _uiScale = 1.0f;
    public static float UIScale => GameManager.Instance?.UI?._uiScale ?? 1.0f;

    public IEnumerator Initialize(GameManager newManager)
    {
        SetMainCanvas(GetComponentInChildren<Canvas>());
        // UIBase.FindUIBaseWithTag("MainCanvas"); => 글자로 찾는 방법인데 위에서부터 쭉 찾는거라 오래걸림
        SetUI(UIType.Loading, GetComponentInChildren<UI_LoadingScreen>()); // <> => 어떤 타입을 원하는지? => 자료형!
        // Debug.Log(MainCanvas);
        yield return null;
    }

    public RectTransform CreateFullScreen(string wantName)
    {
        GameObject instance = new GameObject(wantName);
        RectTransform result = instance.AddComponent<RectTransform>();
        result.SetParent(MainCanvas.transform);
        result.SetAsFirstSibling();

        result.anchorMin = Vector3.zero;
        result.anchorMax = Vector3.one;
        result.offsetMin = Vector3.zero;
        result.offsetMax = Vector3.zero;
        result.localScale = Vector3.one;

        return result;
    }

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        createdTransform = CreateFullScreen("CreateUI");
        _movableScreen = CreateUI(UIType.Movable, "MovableScreen", MainCanvas?.transform);

        switcherTransform = CreateFullScreen("ScreenSwitcher");

        foreach (var currentPair in glovalScreenArray)
        {
            UIBase created = CreateUI(currentPair.Key, currentPair.Value, switcherTransform);
            if (created is IOpenable asOpenable) asOpenable.Close();
        }

        changerTransform = CreateFullScreen("ScreenChanger");
        changerTransform.SetAsLastSibling();

        for (ScreenChangeType currentChanger = (ScreenChangeType)1; // int i = 0
            currentChanger < ScreenChangeType._Length;             // i < 3
            currentChanger++)                                      // i++
        {

            GameObject instance = ObjectManager.CreateObject("ScreenChanger", changerTransform);
            if (instance?.TryGetComponent(out UI_ScreenChanger asChanger) ?? false)
            {
                screenChangerDictionary.Add(currentChanger, asChanger);
            }

            instance?.SetActive(false);
        }

        yield return null;
    }

    protected override void OnDisconnected()
    {
        UnSetAllUI();
    }

    protected void SetMainCanvas(Canvas newCanvas)
    {
        _mainCanvas = newCanvas;
        if (_mainCanvas)
        {
            _raycaster = _mainCanvas.GetComponent<GraphicRaycaster>();

            if (MainCanvas.transform is RectTransform mainRectTransform)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(mainRectTransform);
                _uiScale = mainRectTransform.lossyScale.x;
                _uiBoundary = mainRectTransform.rect;
                _uiBoundary.size *= _uiScale;
            }
        }
        else
        {
            _raycaster = null;
        }
    }

    protected UIBase CreateUI(UIType wantType, string wantName, Transform parent)
    {
        GameObject instance = ObjectManager.CreateObject(wantName, parent);
        UIBase result = instance?.GetComponent<UIBase>();
        return SetUI(wantType, result);
    }
    protected UIBase CreateUI(UIType wantType, string wantName)
    {
        UIBase result = CreateUI(wantType, wantName, createdTransform ?? MainCanvas?.transform);

        if (result?.GetComponentInChildren<UI_DraggableWindow>())
        {
            _movableScreen?.SetChild(result.gameObject);
        }

        return result;
    }

    public static UIBase ClaimCreateUI(UIType wantType, string wantName) => GameManager.Instance?.UI?.CreateUI(wantType, wantName);


    protected void UnSetAllUI()
    {
        foreach (UIBase ui in uiDictionary.Values)
        {
            UnsetUI(ui);
        }

        uiDictionary.Clear();
    }
    protected void UnsetUI(UIType wantType)
    {
        if (uiDictionary.TryGetValue(wantType, out UIBase found))
        {
            UnsetUI(found);
            uiDictionary.Remove(wantType);
        }
    }
    protected void UnsetUI(UIBase wantUI)
    {
        if (!wantUI) return;

        wantUI.Unregistration(this);
    }
    public static void ClaimUnsetUI(UIBase wantUI) => GameManager.Instance?.UI?.UnsetUI(wantUI);
    public static void ClaimUnsetUI(GameObject wantObject) => ClaimUnsetUI(wantObject?.GetComponent<UIBase>());


    protected UIBase SetUI(UIBase wantUI)
    {
        wantUI?.Registration(this);
        return wantUI;
    }
    protected UIBase SetUI(UIType wantType, UIBase wantUI)
    {
        if (wantUI == null) return null; // null 이면 null

        if (uiDictionary.TryGetValue(wantType, out UIBase origin)) return origin; // 입력한게 2가지라면 origin을 쓴다.

        uiDictionary.Add(wantType, wantUI); // 위 두가지에 해당하지 않는다면 입력된 값 그대로 출력

        return SetUI(wantUI);
    }

    public static UIBase ClaimSetUI(UIBase wantUI) => GameManager.Instance?.UI?.SetUI(wantUI);
    public static UIBase ClaimSetUI(GameObject wantObject) => ClaimSetUI(wantObject?.GetComponent<UIBase>());
    public static UIBase ClaimSetUI(UIType wantType, UIBase wantUI) => GameManager.Instance?.UI?.SetUI(wantType, wantUI);


    protected UIBase GetUI(UIType wantType)
    {
        if (uiDictionary.TryGetValue(wantType, out UIBase result)) return result; // 있으면 result 반환
        else return null; // 없으면 null
    }
    public static UIBase ClaimGetUI(UIType wantType) => GameManager.Instance?.UI?.GetUI(wantType);


    protected UIBase OpenUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        // result 는 IOpenable 인 opener 인가
        if (result is IOpenable asOpenable) asOpenable.Open();
        if (result) EventSystem.current.SetSelectedGameObject(result.gameObject);
        return result;
    }
    public static UIBase ClaimOpenUI(UIType wantType) => GameManager.Instance?.UI?.OpenUI(wantType);


    protected UIBase CloseUI(UIType wantType)
    {
        // CloseUI(UIType wantType) => 이런방식은 매개변수 생성
        // UIBase result => 이런식이 지역변수
        // asOpenable.Close(); => asOpenable 의(.) 닫기(Close()) 기능

        UIBase result = GetUI(wantType);
        //              자료형    이름     => 변수생성
        if (result is IOpenable asOpenable) asOpenable.Close();
        return result;
    }
    public static UIBase ClaimCloseUI(UIType wantType) => GameManager.Instance?.UI?.CloseUI(wantType);


    protected UIBase ToggleUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        if (result is IOpenable asOpenable) asOpenable.Toggle();
        // result?.SetActive(!result.activeSelf); // activeSelf 지금 상태가 result 상태이면(on/off) ! 반대값을 출력하고 그 값으로 SetActive 작동하라
        return result;
    }
    public static UIBase ClaimToggleUI(UIType wantType) => GameManager.Instance?.UI?.ToggleUI(wantType);


    protected UIBase OpenScreen(UIType wantType)
    {
        CloseUI(CurrentScreen);
        _currentScreenType = wantType;
        return OpenUI(wantType);
    }
    public static UIBase ClaimOpenScreen(UIType wantType) => GameManager.Instance?.UI?.OpenScreen(wantType);

    protected void OpenScreen(UIType wantScreen, ScreenChangeType changeType)
    {
        // 람다 : 프로잭트 내에서 한번만 사용 할 함수
        ClaimScreenChangeEffect(changeType, () => OpenScreen(wantScreen));
    }
    public static void ClaimOpenScreen(UIType wantScreen, ScreenChangeType changeType) => GameManager.Instance?.UI?.OpenScreen(wantScreen, changeType);


    // System.Action endFunction = null => 없으면 아무것도 안하겠다
    protected void ScreenChangeEffectStart(ScreenChangeType wantType, System.Action endFunction = null)
    {
        if(screenChangerDictionary.TryGetValue(wantType, out UI_ScreenChanger result))
        {
            if (currentScreenChanger) return;

            if (!result) { endFunction?.Invoke(); return; }

            result.gameObject.SetActive(true);
            result?.ChangeStart(endFunction);
            currentScreenChanger = result;
        }

        else
        {
            endFunction?.Invoke();
        }
    }
    public static void ClaimScreenChangeEffectStart(ScreenChangeType wantType, System.Action endFunction = null) => GameManager.Instance?.UI?.ScreenChangeEffectStart(wantType, endFunction);
    public static void ClaimScreenChangeEffect(ScreenChangeType wantType, System.Action endFunction = null) => GameManager.Instance?.UI?.ScreenChangeEffectStart(wantType, endFunction + ClaimScreenChangeEffectEnd);


    protected void ScreenChangeEffectEnd()
    {
        if (!currentScreenChanger) return;

        GameObject targetobject = currentScreenChanger.gameObject;
        currentScreenChanger.ChangeEnd(() => targetobject.SetActive(false)); // 끝났으면 꺼라
        currentScreenChanger = null;
    }
    public static void ClaimScreenChangeEffectEnd() => GameManager.Instance?.UI?.ScreenChangeEffectEnd();
    public static void ClainPopUp(string title, string context, string confirm)
    {
        OnPopUp?.Invoke(title, context, confirm);
    }
    
    public static void ClaimErrorMessage(string context)
    {
        OnPopUp?.Invoke("Error", context, "Confirm");
    }
}
