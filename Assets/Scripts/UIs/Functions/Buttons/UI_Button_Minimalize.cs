using UnityEngine;

public class UI_Button_OpenUI : MonoBehaviour
{
    [SerializeField] UIType wantType;
    public void Open(UIType wantType)
    {
        UIManager.ClaimOpenScreen(wantType);
    }
}
