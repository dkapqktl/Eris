using UnityEngine;

public class UI_CharacterHoverInfo : OpenableUIBase
{
    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        InputManager.OnMouseHover -= HoverInfoChange;
        InputManager.OnMouseHover += HoverInfoChange;
    }

    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        InputManager.OnMouseHover -= HoverInfoChange;

    }

    void HoverInfoChange(GameObject newTarget, GameObject oldTarget)
    {
        CharacterBase asCharacter = newTarget?.GetComponent<CharacterBase>();

        if (asCharacter) Open(); // 새로운 오브잭트 들어오면 게임오브잭트 트루
        else Close(); // 아니라면 실패
    }
}
