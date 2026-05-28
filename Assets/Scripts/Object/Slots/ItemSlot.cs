using System;
using UnityEngine;

// 해당 칸 슬롯에 아이탬을 옮기거나 없어졌다면 기존 그 슬롯에 있던 아이템 정보(아이콘 등)가 없어져야 함 그걸 해주는 이벤트
// 해당칸에 아이탬이 사라졌다면 새로운 정보가 들어와야 함, 그래서 ItemSlot changeSlot 는 원래 있던 아이템 정보가 아닌 바꿘 아이템 정보임
public delegate void ItemSlotChangeEvent(ItemSlot changeSlot); 

public class ItemSlot
{
    [SerializeField] ItemContainer item;

    [SerializeField] int currentStack;

    // 아이템 슬롯이 바뀌었을 때 일어날 수 있는 이벤트
    public event ItemSlotChangeEvent OnItemSlotChanged;

    public void NoticeChanged() => OnItemSlotChanged?.Invoke(this);

    public virtual bool Containable(ItemContainer wantItem)
    {
        if (!wantItem)                return false; // 아이템이 없으면 false
        if (item && item != wantItem) return false; // 아이템이 있고 아이템과 들어온 아이템이 다르다면
        if (GetIsMax()) return false;

        return true;
    }

    public ItemContainer GetItem()   => item;

    public int GetStack()            => currentStack;

    public bool GetIsMax()           => item ? currentStack >= item.maxStack : false;
    
    public bool GetIsEmpty()         => item is null || currentStack <= 0;

    // internal : 내부적인! => 나랑 같은 프로젝트에 있는 대상은 모두 쓸 수 있다.

    // 반환값 : 추가했는데 못추가 하고 넘겨버린것.
    public int AddItem(ItemContainer wantItem, int amount)
    {
        if (amount <= 0) return 0; // 어마운트가 0 혹은 음수 라면 0을 반환
        if (!Containable(wantItem)) return amount; // 컨테이너블이 false 라면 어만트 그냥 반환

        item = wantItem;

        // 추가할 양 = 5 개라면?
        // 맥스스택 - 커런트스택 = 남은스택
        //   100        99    =    1
        //                          100 - 99 = 1개가 들어감      5개가 아닌
        int stackable = Mathf.Min(item.maxStack - currentStack, amount);
        currentStack += stackable; // 현재 스택에 1개 넣기가 됨

        return amount - stackable; // 추가하려는 값 - 추가한 값
    }

    public int Clear()
    {
        int removedStack = currentStack; // 몇개 제거됐는지 저장
        item = null; // 아이템 없애고
        currentStack = 0; // 스택 0으로 만들고
        return removedStack; // 저장된 제거 수량 알려주기
    }

    public int RemoveItem(ItemContainer wantItem)
    {
        if (!wantItem) return 0;
        if (GetIsEmpty()) return 0;
        if (item != wantItem) return 0;

        return Clear();
    }
    public int RemoveItem(ItemContainer wantItem, int amount)
    {
        if (amount <=0) return 0;
        if (!wantItem) return 0;
        if (GetIsEmpty()) return amount;
        if (item != wantItem) return amount;

        if (amount >= currentStack) return amount - Clear(); // 빼려고 하는 개수 - 내가 가지고 있던 개수 // 250 250 1 개 있을때 500 을 판다하고 할 경우 사용함

        currentStack -= amount; // 아니라면 요구한 만큼 마이너스

        return 0;
    }
}
