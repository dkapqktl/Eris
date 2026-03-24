using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// delegate 대리자는 누구나 등록하고 시전할 수 있다.
public delegate void MouseDownEvent (Vector3 position);
public delegate void MouseUpEvent   (Vector3 position);
public delegate void MouseMoveEvent (Vector2 screenPosition, Vector3 worldPosition);


[RequireComponent(typeof(PlayerInput))] // 인풋매니저랑 플레이어인풋은 항상 같이 두겠다.
// 이걸 해두면 유니티에서 (script)인풋매니저를 추가하면 player input 도 자동으로 추가됨
//      대리자


public class InputManager : ManagerBase
{
    // event 대리자는 누구나 등록하지만 나만 시전 가능
    public static event MouseDownEvent   OnLeftMouseDown;
    public static event MouseDownEvent   OnRightMouseDown;
    public static event MouseUpEvent     OnLeftMouseUp;
    public static event MouseUpEvent     OnRightMouseUp;
    public static event MouseMoveEvent   OnMouseMove;

    public bool is2D = true;

    PlayerInput targetInput;
    Dictionary<string, InputAction> actionDictionary = new(); // 인풋액션을 찾어라잉


    protected override IEnumerator OnConnected(GameManager newManager)
    {
        targetInput = GetComponent<PlayerInput>();

        LoadAllActions();
        InitializeSetAllActions();

        yield return null;
    }

    protected override void OnDisconnected()
    {

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

        if (actionDictionary.TryGetValue("CursorPositionChanged", out InputAction cursorPositionChange))
        {
            cursorPositionChange.performed += CursorPositionChanged;
        }
    }

    void CursorPositionChanged(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = context.ReadValue<Vector2>();

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

        // OnMouseMove가 ?없을수도 있는데 있다면 Invoke를 실행해라
        OnMouseMove?.Invoke(screenPosition, worldPosition);
    }

    void OnKeyZ(Vector3 worldPosition)
    {
        worldPosition.z += 1;
    }

    void OnKeyC(Vector3 worldPosition)
    {
        worldPosition.z -= 1;
    }

}