using System;
using UnityEngine;

public class ItemSlot
{
    [SerializeField] ItemContainer item;

    [SerializeField] int currentStack;

    public virtual bool Containable(ItemContainer newItem)
    {
        if (item) return false;
        else return true;

        // return newItem != null ? true : false;
    }

    public ItemContainer GetItem()   => item;

    public int GetStack()            => currentStack;

    public bool GetIsMax()           => item ? currentStack >= item.maxStack : false;

    // internal : 내부적인! => 나랑 같은 프로젝트에 있는 대상은 모두 쓸 수 있다.

    // 반환값 : 추가했는데 못추가 하고 넘겨버린것.
    public int AddItem(ItemContainer wantItem, int amount)
    {
        if (wantItem is null) return 0;
        if (amount < 0) return 0;
        if (item is not null && item != wantItem) return amount; // 이거 나중에 수정해야함 지금은 아이템 칸 하나만 있다는 가정하임

        return amount;


    }
}
