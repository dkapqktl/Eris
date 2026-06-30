using NUnit.Framework.Constraints;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventoryWindow : OpenableUIBase
{

    protected Inventory targetInventory;

    [SerializeField] protected LayoutGroup layout;
    [SerializeField] protected string itemSlotPrefabName;

    public override void Registration(UIManager manager)
    {
        base.Registration(manager);
        // targetInventory?.Initialize(); // Registration 등록할때 Initialize 초기화 강제로 하기
        ConnectedInventory(targetInventory);
    }
    public override void Unregistration(UIManager manager)
    {
        base.Unregistration(manager);
    }


    public void ConnectedInventory(Inventory newInventory)
    {
        if (!newInventory) return; // 인벤토리가 없다면 리턴
        targetInventory = newInventory; // 있다면 뉴인벤토리는 타겟인벤토리가 된다(인벤토리 등록)

        /* 2차원 배열
        if (!layout) return;
        if(layout is GridLayoutGroup asGridLauout) // 레이아웃이 그리드레이아웃그룹이라고 하면 asGridLauout에 임시저장하고 아래 수행
        {
            // 그리드레이웃의 제한 개수 = 대상 인벤토리의 열(세로) 개수
            asGridLauout.constraintCount = targetInventory.columns;
        }
        */

        // 이후 아이템 슬롯을 전부 가져오기
        // 인벤토리 슬롯을 하나하나 만들어주기

        targetInventory.OnInventoryChanged -= InventoryChanged;
        targetInventory.OnInventoryChanged += InventoryChanged;

        foreach (ItemSlot currentSlot in newInventory.GetAllSlot())
        {
            AddSlot(currentSlot);
        }
    }
    public void DisConnectedInventory()
    {
        if (!layout) return;

        // 아이템 슬롯을 하나하나 가져와서 레이아웃에 들어있는 오브잭트를 디스트로이 해야 한다.
        // foreach 와 for 은 안됨

        targetInventory.OnInventoryChanged -= InventoryChanged;

        while (layout.transform.childCount > 0)
        {
            RemoveSlot(0);
        }

        //    if (!layout) return;
        //    while (layout.transform.childCount > 0)
        //    {
        //        Transform targetChild = layout.transform.GetChild(0);
        //        targetChild.SetParent(null);
        //        ObjectManager.DestroyObject(targetChild.gameObject);
        //    }


        // 이건 아님
        // foreach (layout.GetComponentInChildren(out GameObject currentObject))
        // { ObjectManager.DestroyObject(currentObject); }
    }

    public void ClaimSort()
    {
        if(targetInventory)
        {
            targetInventory.SortByType();
        }
    }

    public void InventoryChanged()
    {
        if (targetInventory == null) return;

        int totalSlot = targetInventory.currentInventorySize;
        int currentSlot = layout.transform.childCount;
        // 사용가능한 이벤토리칸이 추가되었다면 
        // 기존 사용 가능한 인벤토리는 건들지 않고
        // 기존 사용 가능한 인벤토리칸 뒤에 +1칸이 추가 되어야함
        //      15           10
        if (totalSlot > currentSlot)
        {
            //0 ~ 9
            //10 11 12 13 14
            for (int i = currentSlot; i < totalSlot; i++)
            {
                AddSlot(targetInventory.GetSlot(i));
            }
        }

        else if(totalSlot < currentSlot)
        {
            for (int i = currentSlot - 1; i >= totalSlot; i--)
            {
                RemoveSlot(i);
            }
        }



    }

    public void AddSlot(ItemSlot currentSlot)
    {

        if (currentSlot is null) return; // 슬롯이 없다면 다음으로 넘어가기

        // 오브잭트를 만들어라, 아이템슬롯이라는 프리팹으로 위치는 layout.transform 에 해당 배열 위치에 생성 그리고 인스턴스에 저장
        GameObject instance = ObjectManager.CreateObject(itemSlotPrefabName, layout.transform);

        if (instance == null) return; // 인스턴스가 없으면 넘어가라

        if (instance.TryGetComponent(out UI_ItemSlotInfo createdSlot)) // 인스턴스의 컴포넌트를 가져오는데 UI_ItemSlotInfo가 있는지? 있다면 createdSlot에 임시저장하고 아래 수행
        {
        createdSlot.ConnectSlot(currentSlot);
        }
    }

    public void RemoveSlot(int wantSlot)
    {
        if (layout == null) return;
        Transform targetChild = layout.transform.GetChild(wantSlot);
        
        if (targetChild == null) return;
        targetChild.SetParent(null);
        ObjectManager.DestroyObject(targetChild.gameObject);
    }

}
