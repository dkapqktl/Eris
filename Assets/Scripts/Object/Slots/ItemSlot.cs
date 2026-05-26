using UnityEngine;

public class ItemSlot
{
    [SerializeField] ItemContainer item;

    [SerializeField] int currentStack;

    public virtual bool Containabl(ItemContainer newItem)
    {
        if (item) return false;
        else return true;

        // return newItem != null ? true : false;
    }

}
