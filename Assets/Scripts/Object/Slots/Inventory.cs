using System.Collections.Generic;
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

    // 처음 시작할때 모든 인벤토리를 가져오기. (처음시작시 빈 슬롯들도 가져와야함)
    public ItemSlot[] GetAllSlot() 
    {
        // 2차원 배열에서 Length : 전체 길이
        // GetLength(0) : 0번째 차원의 길이 => 행의 길이(가로)
        // GetLength(1) : 1번째 차원의 길이 => 열의 길이(세로)

        // 배열(R)   x    y    x    y    x    y
        // 0 1 2   ( 0 , 0 ) ( 0 , 1 ) ( 0 , 2 )
        // 3 4 5   ( 1 , 0 ) ( 1 , 1 ) ( 1 , 2 )
        // 6 7 8   ( 2 , 0 ) ( 2 , 1 ) ( 2 , 2 )
        // width(위의 경우 한줄의 길이는 3) * x + y
        
        ItemSlot[] result = new ItemSlot[slots.Length];

        int height = slots.GetLength(0); // GetLength(0) 의 (0)은 (x,y)에서 x의 값임
        int width = slots.GetLength(1); // GetLength(1) 의 (1)은 (x,y)에서 y의 값임

        for (int row = 0; row < height; row++) 
        {

            for (int column = 0; column < width; column++)
            {
                result[width * row + column] = slots[row,column];
            }
        }

        return result;

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
        slots[0, 0].AddItem(wantItem, amount);
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
