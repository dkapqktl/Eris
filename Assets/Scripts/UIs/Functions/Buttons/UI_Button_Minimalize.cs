using UnityEngine;

public class UI_Button_Minimalize : MonoBehaviour
{
    [SerializeField] UIType wantType;
    public void Open(UIType wantType)
    {
        UIManager.ClaimOpenScreen(wantType);
    }
}
