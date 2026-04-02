using System;
using UnityEngine;

public class UI_MovableScreen : UIBase
{
    Vector3 popupPosition = Vector3.zero;
    Vector3 popupShift = new(20.0f, -20.0f);

    public override void Registration(UIManager manager)
    {
        base.Registration(manager);

        UIManager.OnPopUp -= PopUp;
        UIManager.OnPopUp += PopUp;
    }

    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
        UIManager.OnPopUp -= PopUp;
    }

    protected override GameObject OnSetChild(GameObject newChild)
    {
        return base.OnSetChild(newChild);
    }

    protected override void OnUnsetChild(GameObject oldChild)
    {
        base.OnUnsetChild(oldChild);
    }
    private void PopUp(string tilte, string context, string confirm)
    {
        GameObject newChild = SetChild(ObjectManager.CreateObject("PopUp"));
        newChild.transform.localPosition = popupPosition;
        popupPosition += popupShift;
    }
}
