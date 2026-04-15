using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class UI_InGameScreen : UI_ScreenBase
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
        if (UIManager.ClaimGetUI(UIType.Inventory).isActiveAndEnabled)
        {
            UIManager.ClaimCloseUI(UIType.Inventory);
        }
        else
        {
            UIManager.ClaimCloseUI(UIType.Menu);
        }
    }

}
