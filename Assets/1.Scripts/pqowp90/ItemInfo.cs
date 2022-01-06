using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    weapon,
    armor,
    accessories,
    hilItem,
}


[CreateAssetMenu(fileName = "Item List", menuName = "ScriptableObject/ItemList", order = int.MaxValue)]
[System.Serializable]
public class ItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemInfo> itemInfos = new List<ItemInfo>();
}
[System.Serializable]
public class ItemInfo{
    public string itemName;
    public int itemId;
    public int count;
    public Sprite itemSprite;
    public ItemType itemType;

    

    public ItemInfo DeepCopy()
    {
        ItemInfo newCopy = new ItemInfo();
        newCopy.itemName = this.itemName;
        newCopy.itemId = this.itemId;
        newCopy.count = this.count;
        newCopy.itemType = this.itemType;
        return newCopy;
    }
}