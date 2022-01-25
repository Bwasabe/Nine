using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    weapon,
    skill,
    accessories,
    hilItem,
}


[CreateAssetMenu(fileName = "ItemList", menuName = "ItemList", order = int.MaxValue)]
[System.Serializable]
public class ItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemInfo> itemInfos = new List<ItemInfo>();
}
[System.Serializable]
public class ItemInfo{
    public string itemName;
    public string explan;
    public int itemId;
    public int count;
    public Sprite itemSprite;
    public ItemType itemType;
    

    

    public ItemInfo DeepCopy()
    {
        ItemInfo newCopy = new ItemInfo();
        newCopy.itemName = this.itemName;
        newCopy.explan = this.explan;
        newCopy.itemId = this.itemId;
        newCopy.count = this.count;
        newCopy.itemType = this.itemType;
        newCopy.itemSprite = this.itemSprite;
        return newCopy;
    }
}