using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;


public delegate void InventoryEvent();



public class Inventory : MonoBehaviour
{
    readonly string[] itemList = { "LesserHealPotion" };

    public static ItemSlot cursorSlot = new ItemSlot();

    // public void HealPotionPlus(int amount)
    // {
    //     int index = UnityEngine.Random.Range(0, itemList.Length);
    //     ItemContainer potion = DataManager.LoadDataFile<ItemContainer>(itemList[index]);
    //     AddItem(potion, amount);
    // }


    // Columns   Rows
    //  세로      가로
    //   행       열

    /* 2차원 배열
    public int columns;
    public int rows;
    ItemSlot[,] slots;
    */

    public int maxInventorySize = 25;
    public int currentInventorySize = 10;
 
    ItemSlot[] slots; // 1차원 배열

    public event InventoryEvent OnInventoryChanged;

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        /* 2차원 배열
        slots = new ItemSlot[rows, columns];

        for (int row = 0; row < rows; row++)
        {

            for (int column = 0; column < columns; column++)
            {
                slots[row, column] = new ItemSlot();
            }
        }
        */

        slots = new ItemSlot[maxInventorySize];
        for (int array = 0; array < maxInventorySize; array++)
        {
            slots[array] = new ItemSlot();
        }
    }

    // public bool IsEmpty(ItemSlot target) => target?.GetIsEmpty() ?? false;


    // Comparison 는 Comparison(in T)(T x, T y) 이런식으로 되어있음
    // 반환값은 int 임(int Comparison)
    // bool 은 참,거짓 만 알려줘서 같은지는 구분 안해줌
    // Comparison은 x와 y를 비교해서 x가 큰지 y가 큰지 아니면 같은지 3가지를 체크 해야해서
    // x가 작으면 음수, 같으면 0, x가 크면 양수 이렇게 반환해 주기 때문에 Comparison는 int를 반환함
    public void Sort(System.Comparison<ItemSlot> Method)
    {
        MergeAll(); // 정렬 시작할 때 모든 대상을 병합하고 시작
        // System.Array.Sort(slots, Method);
        
        int totalLength = slots.Length;

        if (slots is null || totalLength <= 1) return;

        int lastFinder = totalLength - 1;

        while (lastFinder > 0)
        {
            int currentFinder = -1;
            for (int i = 0; i < lastFinder; i++) // totalLength - 1 => 맨 끝애 칸은 비교 할 필요가 없으니 - 1 가지
            {
                ItemSlot left = GetSlot(i);
                ItemSlot right = GetSlot(i + 1); // 사람

                int comparisonResult = Method(left, right); // 좌우 비교 후 좌가 더 크다면
                
                // if (comparisonResult > 0) // 내림차순, 이거 버그걸림
                if (comparisonResult < 0) // 오름차순
                {
                    currentFinder = i;
                    left.ExchangeItem(right); // 좌는 우와 자리 바꿔라
                    
                }

                /* 내림차순으로 정렬
                if (comparisonResult < 0)
                {
                    currentFinder = i;
                    left.ExchangeItem(right); // 좌는 우와 자리 바꿔라

                }
                */
            }
            lastFinder = currentFinder;
        }
        /* 2차원 배열
        int width = slots.GetLength(1);
        for (int i = 0; i < totalLength - 1; i++) // totalLength - 1 => 맨 끝애 칸은 비교 할 필요가 없으니 - 1 가지
        {
            ItemSlot left = GetSlot(i, width);
            ItemSlot right = GetSlot(i + 1, width);

            int comparisonResult = Method(left, right);
            if (comparisonResult > 0) left.ExchangeItem(right);
        }
        */

        foreach (ItemSlot currentSlot in GetAllSlot())
        {
            currentSlot?.NoticeChanged();
        }

    }

    int ItemTypeComparisom(ItemSlot left, ItemSlot right)
    {
        int result;
        if (ItemExistComparisom(left, right, out result)) return result;

        ItemContainer leftItem = left.GetItem();
        ItemContainer rightItem = right.GetItem();

        result = leftItem.CompareByType(rightItem); // 기본 정보를 가지고 비교만 가능

        return result;
    }

    int? ItemExistComparisom(ItemSlot left, ItemSlot right)
    {
        if (left is null)
        {
            if (right is null) return 0;
            else return -1;
        }
        if (right is null) return 1;

        ItemContainer leftItem = left.GetItem();
        ItemContainer rightItem = right.GetItem();

        if (leftItem is null)
        {
            if (rightItem is null) return 0;
            else return -1;
        }
        if (rightItem is null) return 1;

        return null; 
    }

    bool ItemExistComparisom(ItemSlot left, ItemSlot right, out int result)
    {
        int? calculated = ItemExistComparisom(left, right); // 원래함수(ItemExistComparisom) 를 실행하고 calculated에 저장
        result = calculated ?? 0; // 결과가 있으면 calculated 결과가 없으면 0
        return calculated.HasValue; // calculated가 가지고 있는 값(HasValue) 반환
    }

    public void SortByType() => Sort(ItemTypeComparisom);
    
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

    public void IncreaseInventory(int wantIncrease)
    {
        if (slots == null) return;
        currentInventorySize += wantIncrease;

        /* 맥스인벤토리사이즈가 없다 할 경우 인벤토리 늘리기
        if (slots == null) return;

        ItemSlot[] tempSlot = slots;

        slots = new ItemSlot[slots.Length + wantIncrease];

        for (int i = 0; i < tempSlot.Length; i++)
        {
            slots[i] = tempSlot[i];
        }
        */

        OnInventoryChanged?.Invoke();
    }


    public void DecreaseInventory(int wantDecrease)
    {
        if (slots == null || slots.Length <= 1) return;

        currentInventorySize = Mathf.Max(1, currentInventorySize - wantDecrease);

        /* 
        ItemSlot[] tempSlot = slots;

        slots = new ItemSlot[slots.Length - wantDecrease];

        for (int i = 0; i < tempSlot.Length; i++)
        {
            slots[i] = tempSlot[i];
        }
        */

        OnInventoryChanged?.Invoke();
    }


    public void LockSlot(int wantRows, int wantColumns)
    {

    }
    public void UnlockSlot(int wantRows, int wantColumns)
    {

    }


    public int CountItem(ItemContainer wantItem)
    {
        if (!wantItem) return 0;

        int result = 0;

        // 해당 아이템을 가지고 있는 슬롯을 모두 찾기
        foreach (ItemSlot currentSlot in FindFirstItem(wantItem))
        {
            result += currentSlot.GetStack(); // 개수에 지금 보고 있는 슬롯의 개수를 더해주기
        }

        return result;
    }
    public int CountItem(ItemContainer wantItem, out List<ItemSlot> returnSlots)
    {
        returnSlots = new();
        if (!wantItem) return 0;
        int result = 0;

        // 해당 아이템을 가지고 있는 슬롯을 모두 찾기
        foreach(ItemSlot currentSlot in FindFirstItem(wantItem))
        {
            returnSlots.Add(currentSlot);// 리스트에 넣기
            result += currentSlot.GetStack(); // 개수에 지금 보고 있는 슬롯의 개수를 더해주기
        }

        return result;
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


        /* 2차원 배열
        int height = slots.GetLength(0); // GetLength(0) 의 (0)은 (x,y)에서 x의 값임
        int width = slots.GetLength(1); // GetLength(1) 의 (1)은 (x,y)에서 y의 값임

        for (int row = 0; row < height; row++) 
        {

            for (int column = 0; column < width; column++)
            {
                // 널이라면 다음에 하기
                if (slots[row, column] is null) continue;
                // yield return => 결과를 내보내고 나서 기다리기
                yield return slots[row,column];
            }
        }
        */

        int array = Mathf.Min(slots.Length, currentInventorySize);

        for (int i = 0; i < array; i++)
        {
            if (slots[i] is null) continue;
            // yield return => 결과를 내보내고 나서 기다리기
            yield return slots[i];
        }
    }

    public IEnumerable<ItemSlot> GetAllSlotReverse()
    {
        /* 2차원 배열
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
        */

        int array = Mathf.Min(slots.Length, currentInventorySize);

        for (int i = array - 1; i >= 0 ; i--)
        {
            if (slots[i] is null) continue;
            // yield return => 결과를 내보내고 나서 기다리기
            yield return slots[i];
        }
    }       

    public IEnumerable<ItemContainer> GetAllItem()
    {
        HashSet<ItemContainer> usedItem = new();

        foreach(ItemSlot currentSlot in GetAllSlot()) // 모든 슬롯 가져오기
        {
            ItemContainer currentItem = currentSlot.GetItem(); // 슬롯의 아이템 가져오기
            if (currentItem is null) continue; // 아이템없으면 넘어감
            if (!usedItem.Add(currentItem)) continue; // 통과했다면 리스트에 추가
            yield return currentItem; // 아이템 반환해주기
        }
        // List<ItemContainer> usedItem = new(); 했을때만 아래 컨테인스 사용
        // if (usedItem.Contains(currentItem)) continue; // Contains : 포함
        // 만약 리스트에 커런트아이템이 포함되어있다면
    }

    public Dictionary<ItemContainer, List<ItemSlot>> GetAllItemList()
    {
        Dictionary<ItemContainer, List<ItemSlot>> result = new();

        foreach(ItemSlot currentSlot in GetAllSlot())
        {
            ItemContainer currentItem = currentSlot.GetItem(); // 슬롯의 아이템 가져오기
            if (currentItem is null) continue; // 아이템없으면 넘어감
            if (result.TryGetValue(currentItem, out List<ItemSlot> currentList))
            {
                currentList.Add(currentSlot);
            }
            else
            {
                result.Add(currentItem, new() { currentSlot });
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
    
    // public ItemSlot GetSlot(int index, int width) => slots[(index / width), (index % width - 1)]; // 2차원 배열
    public ItemSlot GetSlot(int index)
    {
     
        if (slots is null || slots.Length == 0 || slots.Length <= index || index < 0) return null; // 배열이 0 1 2 3 4 일때 0~4 까지 5번째 칸이 있는거지 5 라는 칸은 없음 그렇기 때문에 GetLength(0) 는 5를 나타내어 = 까지도 넣어야함

        // 1차원 배열
        return slots[index];

        /* 2차원 배열
        int width = slots.GetLength(0);
        slots[(index / width), (index % width - 1)]
        */
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

    /* 2차원 배열
    public ItemSlot[,] Clear()
    {
        ItemSlot[,] origin = slots;
        Initialize();
        return origin;
    }
    */
    public ItemSlot[] Clear()
    {
        ItemSlot[] origin = slots;
        Initialize();
        return origin;
    }

    public int RemoveItem(System.Predicate<ItemContainer> condition)
    {
        return 0;
    }

    public void MergeAll()
    {
        foreach (ItemContainer curruntItem in GetAllItem())
        {
            MergeItem(curruntItem);
        }
    }

    public void MergeItem(ItemContainer wantItem)
    {
        if (!wantItem) return;
        
        int maxStack = wantItem.maxStack;
        if (maxStack <= 1) return;
        
        int totalCount = CountItem(wantItem, out List<ItemSlot> containSlots);
        if (totalCount <= 1) return;
        if (containSlots is null) return;
        
        int slotCount = containSlots.Count;
        if (totalCount >= slotCount * maxStack || slotCount <= 1) return;

        int finalSlot = slotCount - 1;
        for (int i = 0; i < finalSlot; i++) 
        {
            ItemSlot currentSlot = containSlots[i];
            for (int j = finalSlot; j > i; j--)
            {
                if (currentSlot.GetIsMax()) break;
                ItemSlot targetSlot = containSlots[j];
                targetSlot.GiveItem(currentSlot);
                if (targetSlot.GetIsEmpty()) finalSlot--;
            }
        }
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

        return amount; // rl RemoveItemOnExistSlots(wantItem, amount);
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

    public void MoveItem(int index, Inventory targetInventory, int targetIndex, int amount = -1)
    {

    }

    public void ExchangeItem(int index, int targetIndex)
    {
        ExchangeItem(index, this, targetIndex);
    }

    public void ExchangeItem(int index, ItemSlot targetSlot)
    {

        ItemSlot first = GetSlot(index);
        if (first is null) return; // 슬롯이 없으면 리턴
        
        first.ExchangeItem(targetSlot);
        first.NoticeChanged();
        targetSlot.NoticeChanged();
    }

    public void ExchangeItem(int index, Inventory targetInventory, int targetIndex)
    {
        ItemSlot first = GetSlot(index);
        if (first is null) return; // 슬롯이 없으면 리턴
        if (!targetInventory) return; // 인벤토리가 없으면 리턴
        
        ItemSlot second = targetInventory.GetSlot(targetIndex);
        if (second is null) return; // 대상이 없으면 리턴

        first.ExchangeItem(second);
        first.NoticeChanged();
        second.NoticeChanged();
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
