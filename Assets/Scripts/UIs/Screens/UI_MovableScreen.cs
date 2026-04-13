using System;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public class UI_MovableScreen : UI_ScreenBase
{
    Vector3 popupPosition = Vector3.zero;
    Vector3 popupShift = new(20.0f, -20.0f);

    [SerializeField] List<UIBase> popupList = new();

    UI_DraggableWindow currentDragTarget = null;

    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        InputManager.OnCancel += (value) => UIManager.ClaimToggleUI(UIType.Menu);
        InputManager.OnMouseMove -= MouseMove;
        InputManager.OnMouseMove += MouseMove;
        InputManager.OnMouseLeftButton -= MouseLeft;
        InputManager.OnMouseLeftButton += MouseLeft;
        UIManager.OnPopUp -= PopUp;
        UIManager.OnPopUp += PopUp;
    }



    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        InputManager.OnMouseMove -= MouseMove;
        InputManager.OnMouseLeftButton -= MouseLeft;
        UIManager.OnPopUp -= PopUp;
    }

    protected override GameObject OnSetChild(GameObject newChild)
    {
        UIManager.ClaimSetUI(newChild);

        if(newChild)
        {
            UI_DraggableWindow asDraggable = newChild.GetComponentInChildren<UI_DraggableWindow>();
            if(asDraggable)
            {
                asDraggable.OnDragStart -= SetDragTarget;
                asDraggable.OnDragStart += SetDragTarget;
            }
        }

        return base.OnSetChild(newChild);
    }

    protected override void OnUnsetChild(GameObject oldChild)
    {
        UIManager.ClaimSetUI(oldChild);

        if (oldChild)
        {
            UI_DraggableWindow asDraggable = oldChild.GetComponentInChildren<UI_DraggableWindow>();
            if (asDraggable)
            {
                asDraggable.OnDragStart -= SetDragTarget;
            }
        }

        base.OnUnsetChild(oldChild);
    }

    void SetDragTarget(UI_DraggableWindow dragTarget, Vector2 startPosition)
    {
        currentDragTarget = dragTarget;
        if(currentDragTarget)
        {
            currentDragTarget.SetMouseStartPosition(startPosition);
        }
        Debug.Log(currentDragTarget);

    }

    private void MouseLeft(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (!value) currentDragTarget = null; // 클릭을 때면 이동 안시키기
        // 이거 하기 전에는 클릭 한번하면 팝업창이 붙어서 안떨어졌음
    }
    private void MouseMove(Vector2 screenPosition, Vector3 worldPosition)
    {
        if(currentDragTarget)
        {
            currentDragTarget.SetMouseCurrentPosition(screenPosition);
        }
    }
    private void PopUp(string tilte, string context, string confirm)
    {

        GameObject newChild = SetChild(ObjectManager.CreateObject("PopUp"));

        if (newChild)
        {
            newChild.transform.localPosition = GetNextPopUpPosition();

            if (newChild.TryGetComponent(out UIBase newUI))
            {
                // 팝업창에 포함시킴
                if(!popupList.Contains(newUI))
                {
                    popupList.Add(newUI);
                }
            }


            if (newChild.TryGetComponent(out ISystemMessagePossible target))
            {
                target.SetSystemMessage(tilte, context, confirm);
            }

            if (newChild.TryGetComponent(out IConfirmable confirmTarget))
            {
                confirmTarget.SetConfirmAction(() =>
                {
                    if (newUI) popupList.Remove(newUI);
                    UnSetChild(newChild);
                    ObjectManager.DestroyObject(newChild);
                });
            }
        }
    }

    public Vector3 GetNextPopUpPosition()
    {
        Vector3 bestScore = Vector3.zero;

        if (popupList.Count == 0) return bestScore;

        foreach (UIBase currntPopUp in popupList)
        {
            Vector3 currentScore = currntPopUp.transform.localPosition;

            if (bestScore.x < currentScore.x) bestScore.x = currentScore.x;
            if (bestScore.y > currentScore.y) bestScore.y = currentScore.y;
        }

        return bestScore + popupShift;
    }

    
}
