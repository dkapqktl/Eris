using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum ItemType
{
    Miscellaneous = 0, 
    Material = 10,
    Quest = 90,
    Important = 100,
    Consumable = 400,
    Equipment = 500, 
    Length
}



[CreateAssetMenu(fileName = "ItemContainer", menuName = "Item/ItemBase")]
public class ItemContainer : InfoContainer
{
    [Header("Item Base Info")]
    public int id;
    [Space]
    [Header("Item Detail")]
    public ItemType type;
    public int maxStack;
    public float weight;
   

    public virtual int CompareByType(ItemContainer other)
    {
        if (other == null) return 1;
        int result = type - other.type; 
        if (result != 0) return result; // 타입으로 분류 하고
        return id - other.id; // 같은 타입이라면 아이디로 분류 해주기
    }
}
