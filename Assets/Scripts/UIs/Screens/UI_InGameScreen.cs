using UnityEngine;

public class UI_InGameScreen : UI_ScreenBase
{
    private void OnEnable()
    {
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnCancel += CancelMenu;

        InputManager.OnShowInventoryButton -= InventoruMenu;
        InputManager.OnShowInventoryButton += InventoruMenu;
    }

    private void OnDisable()
    {
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnShowInventoryButton -= InventoruMenu;
    }

    void CancelMenu(bool value)
    {
        foreach (UIType type in System.Enum.GetValues(typeof(UIType)))
        {
            // 닫으면 안되는 UI 제외
            if (type == UIType.None) continue;
            if (type == UIType.Loading) continue;
            if (type == UIType.Title) continue;
            if (type == UIType.LoadingText) continue;
            if (type == UIType.Movable) continue;
            if (type == UIType.Target) continue;
            if (type == UIType.Ingame) continue;
            if (type == UIType.GameQuit) continue;

            var ui = UIManager.ClaimGetUI(type);

            if (ui != null && ui.isActiveAndEnabled)
            {
                UIManager.ClaimCloseUI(type);
                return;
            }
        }

        if (UIManager.ClaimGetUI(UIType.GameQuit).isActiveAndEnabled)
        {
            UIManager.ClaimCloseUI(UIType.GameQuit);
        }
        else
        {
            UIManager.ClaimOpenUI(UIType.GameQuit);
        }
    }

    void InventoruMenu(bool value)
    {
        if (UIManager.ClaimGetUI(UIType.Inventory).isActiveAndEnabled)
        {
            UIManager.ClaimCloseUI(UIType.Inventory);
        }
        else
        {
            UIManager.ClaimOpenUI(UIType.Inventory);
        }
    }

}
