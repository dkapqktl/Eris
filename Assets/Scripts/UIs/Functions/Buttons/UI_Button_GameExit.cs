using UnityEngine;

public class UI_Button_GameExit : MonoBehaviour
{
    public void OnClickExit()
    {
        GameManager.QuitGame();
    }
}
