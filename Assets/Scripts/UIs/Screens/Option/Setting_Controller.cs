using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Setting_Controller : MonoBehaviour
{
    public InputActionReference showInventoryAction;
    public TMP_Text keyText;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    public void ChangeInventoryKey()
    {
        keyText.text = "Press any key...";

        showInventoryAction.action.Disable();

        rebindOperation = showInventoryAction.action
            .PerformInteractiveRebinding()
            .OnComplete(operation =>
            {
                operation.Dispose();

                showInventoryAction.action.Enable();

                keyText.text = showInventoryAction.action.GetBindingDisplayString();
            });

        rebindOperation.Start();
    }
}

