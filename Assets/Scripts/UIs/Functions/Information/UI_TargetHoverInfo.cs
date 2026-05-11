using UnityEngine;

public class UI_TargetHoverInfo : OpenableUIBase
{
    [SerializeField] Vector2 shiftedPosition;
    [SerializeField] TMPro.TextMeshPro targetName;
    [SerializeField] UnityEngine.UI.Slider HPBar;

    CharacterBase target;

    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        InputManager.OnMouseHover -= HoverInfoChange;
        InputManager.OnMouseHover += HoverInfoChange;

        GameManager.OnUpdateCharacter -= MoveToTarget;
        GameManager.OnUpdateCharacter += MoveToTarget;
    }

    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        InputManager.OnMouseHover -= HoverInfoChange;

        GameManager.OnUpdateCharacter -= MoveToTarget;

    }

    public void SetTarget(CharacterBase wantTarget)
    {
        target = wantTarget;
    }

    private void MoveToTarget(float deltatime)
    {
        if (target == null) return;
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position) + (Vector3)shiftedPosition;
    }

    void HoverInfoChange(GameObject newTarget, GameObject oldTarget)
    {
        CharacterBase asCharacter = newTarget?.GetComponent<CharacterBase>();

        if (asCharacter) Open(); // 새로운 오브잭트 들어오면 게임오브잭트 트루
        else Close(); // 아니라면 실패
    }
}
