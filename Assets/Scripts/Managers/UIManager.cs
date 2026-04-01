using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum UIType
{
    None, Loading, Title, LoadingText, Movable,
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

    Canvas _mainCanvas;

    public Canvas MainCanvas => _mainCanvas;

    // 찾아주세요! 이 타입이면 이 오브젝트 입니다.
    Dictionary<UIType, UIBase> uiDictionary = new();


     public IEnumerator Initialize(GameManager newManager)
    {
        _mainCanvas = GetComponentInChildren<Canvas>();
        // UIBase.FindUIBaseWithTag("MainCanvas"); => 글자로 찾는 방법인데 위에서부터 쭉 찾는거라 오래걸림
        SetUI(UIType.Loading, GetComponentInChildren<UI_LoadingScreen>()); // <> => 어떤 타입을 원하는지? => 자료형!
        // Debug.Log(MainCanvas);
        yield return null;
    }

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        UIBase movableUI = CreateUI(UIType.Movable, "MovableScreen");
        yield return null;

        movableUI.SetChild(ObjectManager.CreateObject("PopUp"));
        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    protected UIBase CreateUI(UIType wantType, string wantName)
    {
        GameObject instance = ObjectManager.CreateObject(wantName, _mainCanvas.transform);
        UIBase result = instance?.GetComponent<UIBase>();
        return SetUI(wantType, result);
    }
    protected UIBase SetUI(UIType wantType, UIBase wantUI)
    {
        if (wantUI == null) return null; // null 이면 null

        if (uiDictionary.TryGetValue(wantType, out UIBase origin)) return origin; // 입력한게 2가지라면 origin을 쓴다.

        uiDictionary.Add(wantType, wantUI); // 위 두가지에 해당하지 않는다면 입력된 값 그대로 출력

        return wantUI;
    }
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
        // 아래걸 한줄로 만들면 위가 됨
        // IOpenable opener = result as IOpenable;
        // if(opener != null) opener.Open();
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

    public static void ClainPopUp(string title, string context, string confirm)
    {
        OnPopUp?.Invoke(title, context, confirm);
    }
    
    public static void ClainErrorMessage(string context)
    {
        OnPopUp?.Invoke("Error", context, "Confirm");
    }
}
