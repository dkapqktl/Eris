using UnityEngine;

public class UI_ExitUI : OpenableUIBase
{
    private void OnEnable()
    {
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnCancel += CancelMenu;
    }

    private void OnDisable()
    {
        InputManager.OnCancel -= CancelMenu;
    }

    void CancelMenu(bool value)
    {
        if (!value) return;

        if (UIManager.ClaimGetUI(UIType.GameQuit).isActiveAndEnabled)
        {
            UIManager.ClaimCloseUI(UIType.GameQuit);
        }
    }
}
