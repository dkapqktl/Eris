using UnityEngine;

public class UI_PlayerInventory : UI_InventoryWindow
{
    public override void Registration(UIManager manager)
    {
        targetInventory = CharacterBase.localPlayer.GetComponent<Inventory>();

        base.Registration(manager);
    }
}
