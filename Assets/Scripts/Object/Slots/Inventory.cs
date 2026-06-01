using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

        for (int row = 0; row < rows; row++)
        {

            for (int column = 0; column < columns; column++)
            {
                slots[row, column] = new ItemSlot();
            }
        }
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

    // IEnumerable => 원하는 자료형을 반복적으로 내보내는 자료형, < > 안에 들어있는 타입을 요구할 때마다 하나씩 나오는 구조
    // 처음 시작할때 모든 인벤토리를 가져오기. (처음시작시 빈 슬롯들도 가져와야함)
    public IEnumerable<ItemSlot> GetAllSlot() 
    {
        // 2차원 배열에서 Length : 전체 길이
        // GetLength(0) : 0번째 차원의 길이 => 행의 길이(가로)
        // GetLength(1) : 1번째 차원의 길이 => 열의 길이(세로)

        // 배열(R)   x    y    x    y    x    y
        // 0 1 2   ( 0 , 0 ) ( 0 , 1 ) ( 0 , 2 )
        // 3 4 5   ( 1 , 0 ) ( 1 , 1 ) ( 1 , 2 )
        // 6 7 8   ( 2 , 0 ) ( 2 , 1 ) ( 2 , 2 )
        // width(위의 경우 한줄의 길이는 3) * x + y

        // ItemSlot[] result = new ItemSlot[slots.Length]; // IEnumerable와 yield return를 쓰면 저장할 필요없어 주석처리함

        int height = slots.GetLength(0); // GetLength(0) 의 (0)은 (x,y)에서 x의 값임
        int width = slots.GetLength(1); // GetLength(1) 의 (1)은 (x,y)에서 y의 값임

        for (int row = 0; row < height; row++) 
        {

            for (int column = 0; column < width; column++)
            {
                // 널이라면 다음애 하기
                if (slots[row, column] is null) continue;
                // yield return => 결과를 내보내고 나서 기다리기
                yield return slots[row,column];
            }
        }
    }

    public IEnumerable<ItemSlot> GetAllSlotReverse()
    {
        int height = slots.GetLength(0);
        int width = slots.GetLength(1);

        for (int row = (height - 1); row >= 0; row--)
        {

            for (int column = (width - 1); column >= 0; column--)
            {
                if (slots[row, column] is null) continue;
                yield return slots[row, column];
            }
        }
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
        if (wantRows < 0  ||  wantColumns < 0) return null;
        if (wantRows    >= slots.GetLength(0)) return null; // 배열이 0 1 2 3 4 일때 0~4 까지 5번째 칸이 있는거지 5 라는 칸은 없음 그렇기 때문에 GetLength(0) 는 5를 나타내어 = 까지도 넣어야함
        if (wantColumns >= slots.GetLength(0)) return null;

        return slots[wantRows, wantColumns];
    }
    public ItemSlot FindItem(string containWord)
    {
        return default;
    }


    public IEnumerable<ItemSlot> FindFirstEmptySlot()
    {
        foreach (ItemSlot currentSlot in GetAllSlot())
        {
            if (currentSlot.GetIsEmpty()) yield return currentSlot;
        }
    }

    public IEnumerable<ItemSlot> FindLastEmptySlot()
    { 
        foreach (ItemSlot currentSlot in GetAllSlotReverse())
        {
            if (currentSlot.GetIsEmpty()) yield return currentSlot;
        }
    }
    public IEnumerable<ItemSlot> FindFirstItem(ItemContainer target)
    {
        foreach (ItemSlot currentSlot in GetAllSlot())
        {
            if (currentSlot.GetItem() == target) yield return currentSlot;
        }
    }
    public IEnumerable<ItemSlot> FindLastItem(ItemContainer target)
    {
        foreach (ItemSlot currentSlot in GetAllSlotReverse())
        {
            if (currentSlot.GetItem() == target) yield return currentSlot;
        }
    }



    public int AddItem(ItemContainer wantItem, int amount = 1)
    {
        amount = AddItemOnExistSlots(wantItem, amount);
        if (amount <= 0) return 0;

        return AddItemOnEmptySlots(wantItem, amount);
    }
    public int AddItemOnExistSlots(ItemContainer wantItem, int amount)
    {
        foreach (ItemSlot currenttSlot in FindFirstItem(wantItem))
        {
            if (amount <= 0) return 0;
            amount = currenttSlot.AddItem(wantItem, amount);
            currenttSlot.NoticeChanged();
        }

        return amount;
    }
    public int AddItemOnEmptySlots(ItemContainer wantItem, int amount)
    // 바닥에 아이템이 999개가 있을때 아이템을 5개만 줍는다 하면 나머지는 사라지면 안되고 994개가 남아야함
    // int로 반환값을 받아야하고 리턴은 그 남은 수량을 반환해야함
    {
        foreach (ItemSlot currenttSlot in FindFirstEmptySlot())
        {
            if (amount <= 0) return 0;
            amount = currenttSlot.AddItem(wantItem, amount);
            currenttSlot.NoticeChanged();
        }

        return amount;
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
        return 0;
    }
    public int RemoveItem(ItemContainer wantItem)
    {
        int result = 0;

        foreach (ItemSlot currentSlot in FindLastItem(wantItem))
        {
            result += currentSlot.RemoveItem(wantItem);
            currentSlot.NoticeChanged();
        }

        return result;
    }
    public int RemoveItem(ItemContainer wantItem, int amount)
    {
        foreach (ItemSlot currentSlot in FindLastItem(wantItem))
        {
            if (amount <= 0) return 0;
            amount = currentSlot.RemoveItem(wantItem, amount);
            currentSlot.NoticeChanged();
        }

        return RemoveItemOnExistSlots(wantItem, amount);
    }
    public int RemoveItemOnExistSlots(ItemContainer wantItem, int amount)
    {
        foreach (ItemSlot currenttSlot in FindLastItem(wantItem))
        {
            if (amount <= 0) return 0;
            amount = currenttSlot.RemoveItem(wantItem, amount);
            currenttSlot.NoticeChanged();
        }

        return amount;
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
