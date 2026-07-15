using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static Setting_Controller;

public class KeySetter : MonoBehaviour
{
    InputAction Action;
    public TMP_Text Text;
    public TMP_Text keyText;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    public void Initialized(Setting_Controller.ActionSetter Setter)
    {
        Action = InputManager.ClaimGetAction(Setter.ActionName);
        if (Action is null) return;
        Text.text = Setter.DisplayName;
        keyText.text = PathToKeyName(Action.GetBindingDisplayString());
    }

    public string PathToKeyName(string path)
    {
        string[] directories = path.Split('/'); // 스플릿() 괄호안에 들어있는 것을 기준으로 뭉치를 만든다 ex)1뭉치/2뭉치/3뭉치
        string keyName = directories[^1]; //맨 마지막 칸(뭉치) // ^1 => 맨 마지막칸, ^2... => 맨마지막 칸에서 2번째...
        return keyName;
    }


    public void ChangeKey()
    {
        keyText.text = "<color=red>Press any key...";

        Action.Disable();

        rebindOperation = Action
            .PerformInteractiveRebinding(0) // 바꿀 바인딩 인덱스(0)를 확실히 지정합니다.
            .OnApplyBinding((operation, newBindingString) =>
            {
                // 1. 중복 검사 (수정된 함수 호출)
                if (IsDuplicateBinding(Action, newBindingString))
                {
                    Debug.LogWarning($"중복된 키 발견 ({newBindingString}): 리바인딩을 취소합니다.");
                    operation.Cancel(); // 중복이면 작업을 취소하고 OnCancel 콜백으로 보냅니다.
                    return;
                }

                // 2. 중복이 없으면 안전하게 오버라이드 적용
                operation.action.ApplyBindingOverride(0, newBindingString);
            })
            .OnComplete(operation =>
            {
                // 정상적으로 키 변경이 완료되었을 때
                operation.Dispose();
                Action.Enable();
                keyText.text = PathToKeyName(Action.GetBindingDisplayString()); // 새 키로 UI 갱신
            })
            .OnCancel(operation =>
            {
                // 중복으로 인해 취소(Cancel) 되었을 때
                operation.Dispose();
                Action.Enable();
                keyText.text = PathToKeyName(Action.GetBindingDisplayString()); // 기존 키 유지하며 UI 복구
            });

        rebindOperation.Start();
    }

    private bool IsDuplicateBinding(InputAction currentAction, string newBindingString)
    {
        var asset = currentAction.actionMap?.asset;
        if (asset == null) return false;

        foreach (var map in asset.actionMaps)
        {
            foreach (var action in map.actions)
            {
                // ★ 중요: 현재 바꾸려는 내 액션 '본인' 세팅은 중복 검사에서 완전히 제외합니다.
                // 내가 내 키를 W -> Q -> W로 바꾸는 것은 다른 액션을 침범하지 않으므로 무조건 허용해야 합니다.
                if (action == Action) continue;

                foreach (var binding in action.bindings)
                {
                    // 다른 액션이 '현재 실제로 사용 중인 키(effectivePath)'와 새로 누른 키가 같은지만 비교합니다.
                    if (binding.effectivePath == newBindingString)
                    {
                        return true; // 진짜 다른 기능과 키가 겹칠 때만 중복 처리
                    }
                }
            }
        }
        return false;
    }
}
