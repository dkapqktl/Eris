using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    // Columns   Rows
    //  세로      가로
    //   행       열

    public int columns;
    public int rows;

    ItemSlot[,] slots;

    public void Initialize()
    {
        slots = new ItemSlot[rows, columns];
    }


    public void Sort(System.Comparison<ItemContainer> Method)
    {
        System.Array.Sort(slots);
    }


    public void AutoQuickInsert(Inventory Other)
    {
        
    }
    public bool InsertAll(Inventory Other)
    {
        return default;
    }
    public bool InsertAll(Inventory Other, ItemContainer target)
    {
        return default;
    }


    public void LockSlot(int wantRows, int wantColumns)
    {

    }
    public void UnlockSlot(int wantRows, int wantColumns)
    {

    }


    public ItemSlot CountItem(ItemContainer wantItem)
    {
        return default;
    }
    public int CountItem(ItemContainer wantItem, out List<ItemSlot> returnSlots)
    {
        returnSlots = default;
        return default;
    }


    public ItemSlot FindItem(ItemContainer target)
    {
        return default;
    }
    public ItemSlot FindItem(ItemType wantType)
    {
        return default;
    }
    public ItemSlot FindItem(int wantRows, int wantColumns)
    {
        return default;
    }
    public ItemSlot FindItem(string containWord)
    {
        return default;
    }


    public ItemSlot FindFirstEmpty()
    {
        return default;
    }
    public ItemSlot FindLastEmpty()
    {
        return default;
    }
    public ItemSlot FindFirstItem(ItemContainer target)
    {
        return default;
    }
    public ItemSlot FindLastItem(ItemContainer target)
    {
        return default;
    }



    // 바닥에 아이템이 999개가 있을때 아이템을 5개만 줍는다 하면 나머지는 사라지면 안되고 994개가 남아야함
    // int로 반환값을 받아야하고 리턴은 그 남은 수량을 반환해야함
    public int AddItem(ItemContainer wantItem, int amount = 1)
    {
        return default;
    }
    public int AddItemOnExistSlots(ItemContainer wantItem, int amount)
    {
        return default;
    }
    public int AddItemOnEmptySlots(ItemContainer wantItem, int amount)
    {
        return default;
    }
    public int AddItemToLocation(ItemContainer wantItem, int amount, int row, int column)
    {
        return default;
    }

    public ItemSlot[,] Clear()
    {
        ItemSlot[,] origin = slots;
        Initialize();
        return origin;
    }

    public int RemoveItem(System.Predicate<ItemContainer> condition)
    {
        return default;
    }
    public int RemoveItem(ItemContainer wantItem)
    {
        return default;
    }
    public int RemoveItem(ItemContainer wantItem, int amount)
    {
        return default;
    }
    public int RemoveItemOnExistSlots(ItemContainer wantItem, int amount)
    {
        return default;
    }
    public int RemoveItemToLocation(int row, int column)
    {
        return default;
    }
    public int RemoveItemToLocation(int row, int column, int amount)
    {
        return default;
    }


    public void MoveItem(int startRow, int startColumn, Inventory targetInventory, int ratgetRow, int targetColumn, int amount = -1)
    {

    }


    public bool UseItem(ItemContainer target)
    {
        return default;
    }
    public bool UseItem(int row, int column)
    {
        return default;
    }

}
