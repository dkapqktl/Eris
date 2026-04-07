using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// delegate 대리자는 누구나 등록하고 시전할 수 있다.
// public delegate void MouseUpEvent   (Vector2 screenPosition, Vector3 worldPosition);
public delegate void MouseMoveEvent (Vector2 screenPosition, Vector3 worldPosition);
public delegate void MouseButtonEvent (bool value, Vector2 screenPosition, Vector3 worldPosition);
public delegate void ButtonEvent(bool value);
public delegate void VectorEvent (Vector2 value);
public delegate void AxisEvent(Vector2 value);


[RequireComponent(typeof(PlayerInput))] // 인풋매니저랑 플레이어인풋은 항상 같이 두겠다.
// 이걸 해두면 유니티에서 (script)인풋매니저를 추가하면 player input 도 자동으로 추가됨
//      대리자


public class InputManager : ManagerBase
{
    // event 대리자는 누구나 등록하지만 나만 시전 가능
    public static event MouseButtonEvent OnMouseLeftButton;
    public static event MouseButtonEvent OnMouseRightButton;
    // public static event MouseUpEvent OnMouseLeftUp;
    // public static event MouseUpEvent OnMouseRightUp;
    public static event MouseMoveEvent OnMouseMove;
    public static event ButtonEvent OnCancel;
    public static event ButtonEvent OnShowStatus;
    public static event ButtonEvent OnShowInventory;
    public static event AxisEvent OnMove;

    PlayerInput targetInput;
    Dictionary<string, InputAction> actionDictionary = new(); // 인풋액션을 찾어라잉
    List<RaycastResult> cursorHitList = new();

    Vector2 cursorScreenPosition;
    Vector3 cursorWorldPosition;

    public bool is2D = true;

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        targetInput = GetComponent<PlayerInput>();

        LoadAllActions();
        InitializeSetAllActions();

        // 있으면 빼고 추가, 없으면 그냥 추가만 일어남
        // 그래서 있으면 무조건 빼고 넣으니 내용은 계속 1개만 있게됨
        GameManager.OnUpdateManager -= UpdateEvent;
        GameManager.OnUpdateManager += UpdateEvent;

        yield return null;
    }

    protected override void OnDisconnected()
    {
        GameManager.OnUpdateManager -= UpdateEvent;
    }

    public void UpdateEvent(float deltaTime)
    {
        RefreshGameObjectUnderCursor();
    }

    void RefreshGameObjectUnderCursor()
    {
        cursorHitList.Clear();
        if(is2D)
        { 
            GameManager.Instance.Camera.GetRaycastResult2D(cursorScreenPosition, cursorHitList);
        }
        else
        {
            GameManager.Instance.Camera.GetRaycastResult3D(cursorScreenPosition, cursorHitList);
        }
    }

    public GameObject GetGameObjectUnderCursor()
    {
        // 마우스에 닿은것의 개수가 0이라면 없는것이니 null 을 반환하여 돌아가라
        if (cursorHitList.Count == 0) return null;

        return cursorHitList[0].gameObject; // 첫번째 오브젝트를 돌려주기
    }

    void LoadAllActions()
    {
        foreach (var currentAction in targetInput.actions)
        {
            actionDictionary.TryAdd(currentAction.name, currentAction);
            // currentAction.performed += (InputAction.CallbackContext context) => { Debug.Log(currentAction); };
        }
    }

    void InitializeSetAllActions()
    {
        if (actionDictionary == null || actionDictionary.Count == 0) return;

        InitializeAction("CursorPositionChanged", (context) => CursorPositionChanged(GetVector2Value(context)));
        InitializeAction("Move", (context) => OnMove?.Invoke(GetVector2Value(context)));
        InitializeAction("MouseLeftButtonDown",  (context) => OnMouseLeftButton?.Invoke(true, cursorScreenPosition, cursorWorldPosition));
        InitializeAction("MouseRightButtonDown", (context) => OnMouseRightButton?.Invoke(true, cursorScreenPosition, cursorWorldPosition)); 
        InitializeAction("MouseLeftButtonUp",    (context) => OnMouseLeftButton?.Invoke(false, cursorScreenPosition, cursorWorldPosition));
        InitializeAction("MouseRightButtonUp",   (context) => OnMouseRightButton?.Invoke(false, cursorScreenPosition, cursorWorldPosition));

        InitializeAction("Cancel", (context) => OnCancel?.Invoke(true));
        InitializeAction("ShowStatus", (context) => OnShowStatus?.Invoke(true));
        InitializeAction("ShowInventoryDown", (context) => OnShowInventory?.Invoke(true));
        InitializeAction("ShowInventoryUp", (context) => OnShowInventory?.Invoke(false));
    }

    void InitializeAction(string actionName, Action<InputAction.CallbackContext> actionMeThod)
    {
        if (actionDictionary == null) return;
        if (actionDictionary.TryGetValue(actionName, out InputAction cursorPositionChange))
        {
            cursorPositionChange.performed += actionMeThod;
        }
    }

    T GetInputValue<T>(InputAction.CallbackContext context) where T : struct
    {
        if(context.valueType != typeof(T)) return default;
        return context.ReadValue<T>();
    }

    Vector3 GetVector2Value(InputAction.CallbackContext context) => GetInputValue<Vector2>(context);

    void CursorPositionChanged(Vector2 screenPosition)
    {
        // Vector2 screenPosition = context.ReadValue<Vector2>();

        Vector3 worldPosition;

        if (is2D)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;
        }
        else
        {
            worldPosition = Vector3.zero;
        }

        cursorScreenPosition = screenPosition;
        cursorWorldPosition = worldPosition;


        // OnMouseMove가 ?없을수도 있는데 있다면 Invoke를 실행해라
        OnMouseMove?.Invoke(screenPosition, worldPosition);
    }

}