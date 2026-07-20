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
public delegate void MouseHoverEvent (GameObject newTarget, GameObject oldTarget);
public delegate void ButtonEvent(bool value);
public delegate void VectorEvent (Vector2 value);
public delegate void AxisEvent(float value);


[RequireComponent(typeof(PlayerInput))] // 인풋매니저랑 플레이어인풋은 항상 같이 두겠다.
// 이걸 해두면 유니티에서 (script)인풋매니저를 추가하면 player input 도 자동으로 추가됨
//      대리자


public class InputManager : ManagerBase
{
    // event 대리자는 누구나 등록하지만 나만 시전 가능
    public static event MouseButtonEvent OnMouseLeftButton;
    public static event MouseButtonEvent OnMouseRightButton;
    public static event MouseHoverEvent OnMouseHover;
    public static event MouseMoveEvent OnMouseMove;
    public static event ButtonEvent OnCancel;
    public static event ButtonEvent OnShowStatus;
    public static event ButtonEvent OnShowInventoryButton;
    public static event ButtonEvent OnShowInfo;
    public static event ButtonEvent OnDev;
    public static event VectorEvent OnMove;
    public static event ButtonEvent OnShift;
    public static event ButtonEvent OnCtrl;


    PlayerInput targetInput;
    Dictionary<string, InputAction> actionDictionary = new(); // 인풋액션을 찾어라잉
    List<RaycastResult> cursorHitList = new();


    // static ISelectable _cursorHoverSelectable;
    // public static ISelectable CursorHoverSelectable => _cursorHoverSelectable;
    
    static Vector2 _cursorScreenPosition;
    public static Vector2 CursorScreenPosition => _cursorScreenPosition;

    static Vector3 _cursorWorldPosition;
    public static Vector3 CursorWorldPosition => _cursorWorldPosition;

    static GameObject _cursorHoverObject;
    public static GameObject CursorHoverObject => _cursorHoverObject;

    static bool _isCursorHoverOnUI;
    public static bool IsCursorHoverOnUI => _isCursorHoverOnUI;

    public static bool IsShift { get; private set; } = false;
    void ShiftInput(bool value)
    {
        IsShift = value;
        OnShift?.Invoke(value);
    }
    
    public static bool IsCtrl { get; private set; } = false;
    void CtrlInput(bool value)
    {
        IsCtrl = value;
        OnShift?.Invoke(value);
    }

    static bool isUp;
    public static bool IsUp => isUp;
    
    
    static bool isDown;
    public static bool IsDown => isDown;
    
    
    static bool isLeft;
    public static bool IsLeft => isLeft;

    
    static bool isRight;
    public static bool IsRight => isRight;


    protected override IEnumerator OnConnected(GameManager newManager)
    {
        targetInput = GetComponent<PlayerInput>();

        string json = PlayerPrefs.GetString("KeyBindings", "");

        if (!string.IsNullOrEmpty(json))
        {
            targetInput.actions.LoadBindingOverridesFromJson(json);
        }

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

    public InputAction GetAction(string wantName)
    {
        if (targetInput == null)
        {
            Debug.LogError("targetInput 이 아직 초기화되지 않았습니다.");
            return null;
        }

        return targetInput.actions.FindAction(wantName);
    }
    public static InputAction ClaimGetAction(string wantName) => GameManager.Input?.GetAction(wantName);


    public void UpdateEvent(float deltaTime)
    {
        RefreshGameObjectUnderCursor(_cursorScreenPosition);
    }

    void RefreshGameObjectUnderCursor(Vector2 screenPosition)
    {
        cursorHitList.Clear();
        GameManager.Camera.GetRaycastResult(screenPosition, cursorHitList);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        GameObject firstObject = null;

        if(cursorHitList.Count > 0 && cursorHitList[0].element != null)
        {
            firstObject = cursorHitList[0].gameObject;
        }

        if (GameManager.is2D)
        {
            worldPosition.z = 0;
            float GetValue(RaycastResult target)
            {
                return target.sortingOrder + target.sortingLayer * 100000;
            }

            RaycastResult nearest = cursorHitList.GetMaximum<RaycastResult>(GetValue);
            firstObject = nearest.gameObject;
            worldPosition = nearest.worldPosition;
        }
        else
        {
            float GetDistance(RaycastResult target)
            {
                return target.distance;
            }

            RaycastResult nearest = cursorHitList.GetMinimum<RaycastResult>(GetDistance);
            firstObject = nearest.gameObject;
            worldPosition = nearest.worldPosition;
        }

        GameObject lastHoverObject = _cursorHoverObject;

        _cursorScreenPosition = screenPosition;
        _cursorWorldPosition = worldPosition;
        _cursorHoverObject = firstObject;
        
        if (lastHoverObject != firstObject)
        {
            OnMouseHover?.Invoke(firstObject, lastHoverObject);
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
        
        InitializeAction("Up",
            (context) => { isUp = true; UpdateMove(); },
            (context) => { isUp = false; UpdateMove(); });

        InitializeAction("Down",
            (context) => { isDown = true; UpdateMove(); },
            (context) => { isDown = false; UpdateMove(); });

        InitializeAction("Left",
            (context) => { isLeft = true; UpdateMove(); },
            (context) => { isLeft = false; UpdateMove(); });

        InitializeAction("Right",
            (context) => { isRight = true; UpdateMove(); },
            (context) => { isRight = false; UpdateMove(); });
        InitializeAction("MouseLeftButton",  (context) => OnMouseLeftButton?.Invoke(true, _cursorScreenPosition, _cursorWorldPosition)
                                              ,  (context) => OnMouseLeftButton?.Invoke(false, _cursorScreenPosition, _cursorWorldPosition));

        InitializeAction("MouseRightButton", (context) => OnMouseRightButton?.Invoke(true, _cursorScreenPosition, _cursorWorldPosition) 
                                               , (context) => OnMouseRightButton?.Invoke(false, _cursorScreenPosition, _cursorWorldPosition));

        InitializeAction("Cancel", (context) => OnCancel?.Invoke(true));
        InitializeAction("ShowStatus", (context) => OnShowStatus?.Invoke(true));
        InitializeAction("ShowInventoryButton", (context) => OnShowInventoryButton?.Invoke(true));
        InitializeAction("ShowInfo", (context) => OnShowInfo?.Invoke(true));
        InitializeAction("DevMode", (context) => OnDev?.Invoke(true));
        InitializeAction("Shift", (context) => ShiftInput(true)
                                , (context) => ShiftInput(false));
        InitializeAction("Ctrl", (context) => CtrlInput(true)
                                , (context) => CtrlInput(false));
    }

    void UpdateMove()
    {
        Vector2 move = Vector2.zero;

        if (isUp)
            move.y += 1;

        if (isDown)
            move.y -= 1;

        if (isLeft)
            move.x -= 1;

        if (isRight)
            move.x += 1;

        move = move.normalized;

        OnMove?.Invoke(move);
    }

    void InitializeAction(string actionName, Action<InputAction.CallbackContext> actionMethod, Action<InputAction.CallbackContext> cancelMethod = null)
    {
        if (actionDictionary == null) return;
        if (actionDictionary.TryGetValue(actionName, out InputAction currentInput))
        {
            if (actionMethod is not null) currentInput.performed += actionMethod;
            if (cancelMethod is not null) currentInput.canceled += cancelMethod;
        }
    }

    void InitializeActionMove(string actionName, Action<InputAction.CallbackContext> performedAction, Action<InputAction.CallbackContext> canceledAction)
    {
        if (actionDictionary == null) return;
        if (actionDictionary.TryGetValue(actionName, out InputAction cursorPositionChange))
        {
            cursorPositionChange.performed += performedAction;
            cursorPositionChange.canceled += canceledAction;
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


        RefreshGameObjectUnderCursor(screenPosition); // 마우스 위치 바꼈으니 새로고침

        OnMouseMove?.Invoke(_cursorScreenPosition, _cursorWorldPosition);
    }

}