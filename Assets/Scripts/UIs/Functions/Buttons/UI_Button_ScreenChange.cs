using UnityEngine;

public class UI_Button_ScreenChange : MonoBehaviour
{
    [SerializeField] UIType wantType;
    [SerializeField] ScreenChangeType changeType;

    public void Open()
    {
        UIManager.ClaimOpenScreen(wantType, changeType);
    }

}
