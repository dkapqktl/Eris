using System;
using UnityEngine;

[Serializable]
public struct UIClaim
{
    public string prefabName;
    public UIType uiType;
    public bool isOpen;
    public bool isOverlay;


    public UIBase Execute()
    {
        UIBase result = UIManager.ClaimGetUI(uiType);
        if (!result) result = UIManager.ClaimCreateUI(uiType, prefabName);
        if (!result) return result;
        
        if(!result)
        {
            if(isOverlay) result = UIManager.ClaimOverlay(uiType, prefabName);
            else result = UIManager.ClaimCreateUI(uiType, prefabName);
        }

        if(result is IOpenable openTarget)
        {
            if(isOpen) openTarget.Open();
            else openTarget.Close();
        }

        return result;
    }
}

public class UI_ScreenBase : UIBase, IOpenable
{
    [SerializeField] UIClaim[] requiredUI;
    [SerializeField] protected UIType[] closeWithScreen;

    public bool IsOpen => gameObject.activeSelf;
    public void Open() => gameObject.SetActive(true);
    public void Close() => gameObject.SetActive(false);
    public void Toggle() => gameObject.SetActive(!IsOpen);



    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        if (requiredUI is null) return;
        foreach (UIClaim currentClaim in requiredUI)
        {
            currentClaim.Execute();
        }
    }

    protected virtual void OnDisable()
    {
        if (closeWithScreen != null) // 함께 꺼질 스크린이 널이 아니라면
        {
            foreach (UIType currentUI in closeWithScreen) // 설정해논게 켜져 있을 경우
            {
                UIManager.ClaimCloseUI(currentUI); // 다 꺼라
            }
        }
    }

}
