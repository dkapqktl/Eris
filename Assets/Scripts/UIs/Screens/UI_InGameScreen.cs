using UnityEngine;

public class UI_InGameScreen : UI_ScreenBase
{
    private void OnEnable()
    {
        
        GameManager.UnPause();
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnCancel += CancelMenu;
        
        InputManager.OnShowInfo -= InfoMenu;
        InputManager.OnShowInfo += InfoMenu;

        InputManager.OnShowInventoryButton -= InventoruMenu;
        InputManager.OnShowInventoryButton += InventoruMenu;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.UnPause();
        InputManager.OnCancel -= CancelMenu;
        InputManager.OnShowInfo -= InfoMenu;
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

            var ui = UIManager.ClaimGetUI(type); // 위에 블랙리스트에 해당하지 않는걸 가져와

            if (ui != null && ui.isActiveAndEnabled) // 유아이가 널이 아니고 켜져있다면
            { 
                UIManager.ClaimCloseUI(type); // 그 유아이 닫어
                return; // 그리고 종료
            }
        }

        UIManager.ClaimToggleUI(UIType.InGameMenu); // 해당 키 누르면 게임종료 창 열어
    }

    void InventoruMenu(bool value)
    {
        if(!UIManager.ClaimGetUI(UIType.InGameMenu).isActiveAndEnabled)
        {
            UIManager.ClaimToggleUI(UIType.Inventory); // 해당 키 누르면 인벤토리 열어
        }
    }

    void InfoMenu(bool value)
    {
        UIManager.ClaimToggleUI(UIType.Info); // 해당 키 누르면 인벤토리 열어
    }

}
