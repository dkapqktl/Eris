using UnityEngine;

public class UI_ItemCursorSlotInfo : UI_ItemSlotInfo
{
    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        ConnectSlot(Inventory.cursorSlot);
        InputManager.OnMouseMove -= MoveToMouse;
        InputManager.OnMouseMove += MoveToMouse;
        InputManager.OnMouseLeftButton -= LeftButton;
        InputManager.OnMouseLeftButton += LeftButton;
        InputManager.OnMouseRightButton -= RightButton;
        InputManager.OnMouseRightButton += RightButton;
    }

    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        DisConnectSlot();
        InputManager.OnMouseMove -= MoveToMouse;
        InputManager.OnMouseLeftButton -= LeftButton;
        InputManager.OnMouseRightButton -= RightButton;

    }

    void LeftButton(bool value, Vector2 screenPosiotion, Vector3 worldPosition)
    {
        if (!value) return;
        GameObject currntHover = InputManager.CursorHoverObject;
        if (!currntHover) return;

        if(currntHover.TryGetComponent(out UI_ItemSlotInfo currentSlotInfo))
        {
            ConnectedSlot?.LeftClick(currentSlotInfo.ConnectedSlot);
        }

    }

    void RightButton(bool value, Vector2 screenPosiotion, Vector3 worldPosition)
    {
        if (!value) return;
        GameObject currntHover = InputManager.CursorHoverObject;
        if (!currntHover) return;

        if (currntHover.TryGetComponent(out UI_ItemSlotInfo currentSlotInfo))
        {
            ConnectedSlot?.RightClick(currentSlotInfo.ConnectedSlot);
        }
    }

    void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        transform.position = screenPosition;
    }
}
