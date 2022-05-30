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
    [SerializeField]
    public List<Card> cardInfos = new List<Card>();
}
[System.Serializable]
public class ItemInfo{
    public string itemName;
    public string explan;
    public int itemId;
    public int count;
    public Sprite itemSprite;
    public Sprite realSprite;
    public ItemType itemType;
    public bool isModeing;
    

    

    public ItemInfo DeepCopy()
    {
        ItemInfo newCopy = new ItemInfo();
        newCopy.itemName = this.itemName;
        newCopy.explan = this.explan;
        newCopy.itemId = this.itemId;
        newCopy.count = this.count;
        newCopy.itemType = this.itemType;
        newCopy.itemSprite = this.itemSprite;
        newCopy.realSprite = this.realSprite;
        return newCopy;
    }
    public void SetInfo(ItemInfo item)
    {
        item.isModeing = true;
        if(this.itemId!=0)
            Inventory.Instance.inventori.Find(x => x.itemId == this.itemId).isModeing = false;
        this.itemName = item.itemName;
        this.explan = item.explan;
        this.itemId = item.itemId;
        this.count = item.count;
        this.itemType = item.itemType;
        this.itemSprite = item.itemSprite;
        this.realSprite = item.realSprite;
    }
}