using UnityEngine;

public class UI_Buottn_LoadUI : MonoBehaviour
{
    [SerializeField] UIType wantType;
    [SerializeField] bool wantToggle;

    public void Open()
    {
        if (wantToggle) UIManager.ClaimToggleUI(wantType);
        else UIManager.ClaimOpenUI(wantType);
    }

    public void Close()
    {
        UIManager.ClaimCloseUI(wantType);
    }
    public void Exit()
    {
        GameManager.QuitGame();
    }
}
