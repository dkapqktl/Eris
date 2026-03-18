using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum UIType
{
    None, Loading, Title,
    _Length
}

public class UIManager : ManagerBase
{
    Canvas _mainCanvas;

    public Canvas MainCanvas => _mainCanvas;

    // 찾아주세요! 이 타입이면 이 오브젝트 입니다.
    Dictionary<UIType, UIBase> uiDictionary = new();


    protected override IEnumerator OnConnected(GameManager newManager)
    {
        _mainCanvas = GetComponentInChildren<Canvas>();
        // UIBase.FindUIBaseWithTag("MainCanvas"); => 글자로 찾는 방법인데 위에서부터 쭉 찾는거라 오래걸림
        Debug.Log(MainCanvas);
        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    public UIBase SetUI(UIType wnatType, UIBase wantUI)
    {
        if (wantUI == null) return null; // null 이면 null

        if (uiDictionary.TryGetValue(wnatType, out UIBase origin)) return origin; // 입력한게 2가지라면 origin을 쓴다.

        uiDictionary.Add(wnatType, wantUI); // 위 두가지에 해당하지 않는다면 입력된 값 그대로 출력

        return wantUI;
    }

    public UIBase GetUI(UIType wantType)
    {
        if (uiDictionary.TryGetValue(wantType, out UIBase result)) return result; // 있으면 result 반환
        else return null; // 없으면 null
    }

    public UIBase OpenUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        // result 는 IOpenable 인 opener 인가
        if (result is IOpenable asOpenable) asOpenable.Open();
        // 아래걸 한줄로 만들면 위가 됨
        // IOpenable opener = result as IOpenable;
        // if(opener != null) opener.Open();
        return result;
    }

    public UIBase CloseUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        if (result is IOpenable asOpenable) asOpenable.Close();
        return result;
    }

    public UIBase ToggleUI(UIType wantType)
    {
        UIBase result = GetUI(wantType);
        if (result is IOpenable asOpenable) asOpenable.Toggle();
        // result?.SetActive(!result.activeSelf); // activeSelf 지금 상태가 result 상태이면(on/off) ! 반대값을 출력하고 그 값으로 SetActive 작동하라
        return result;
    }

}
