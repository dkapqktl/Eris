using UnityEngine;

public class UI_InventoryUI : UI_ScreenBase
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
        if (UIManager.ClaimGetUI(UIType.LoadList).isActiveAndEnabled)
        {
            UIManager.ClaimCloseUI(UIType.LoadList);
        }
    }
}
