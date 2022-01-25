using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoSingleton<Inventory>
{  
    [SerializeField]
    ItemList itemList;
    [SerializeField]
    public List<ItemInfo> inventori{get; private set;} = new List<ItemInfo>();

    [SerializeField]
    public List<ItemInfo> weapons = new List<ItemInfo>();
    [SerializeField]
    public List<ItemInfo> skills = new List<ItemInfo>();
    [SerializeField]
    public List<ItemInfo> accessories = new List<ItemInfo>();
    [SerializeField]
    public List<ItemInfo> hilItems = new List<ItemInfo>();

    public void Test(int id){
        AddItem(id, 1);
    }
    public void AddItem(int itemId, int count){
        ItemInfo item = null;
        if(inventori.Find(x => x.itemId == itemId)!=null){
            item = inventori.Find(x => x.itemId == itemId);
            if(item.count >= 99){
                NONONOMore();
                return;
            }
            item.count+=count;
            switch(item.itemType){
                case ItemType.weapon:
                    EventManager.TriggerEvent("RESET_WEAPON_ITEM");
                break;
                case ItemType.skill:
                    EventManager.TriggerEvent("RESET_SKILL_ITEM");
                break;
                case ItemType.accessories:
                    EventManager.TriggerEvent("RESET_ACCESSORI_ITEM");
                break;
                case ItemType.hilItem:
                    EventManager.TriggerEvent("RESET_HILITEM_ITEM");
                break;
            }
        }else{
            item = itemList.itemInfos.Find(x => x.itemId == itemId).DeepCopy();
            item.count = 1;
            inventori.Add(item);
            UIManager.Instance.AddPanel(item);
        }
        
    }
    public void UseItem(int itemId, int count){
        ItemInfo item = inventori.Find(x => x.itemId == itemId);
        if(item!=null){
            if(item.count-count<0){
                NoNoNoItem();
            }
            item.count-=count;
        }else{
            NoNoNoItem();
        }
    }
    private void NONONOMore(){
        Debug.Log("아이템이 너무 많잖아");
    }
    private void NoNoNoItem(){
        Debug.Log("아이템이 더이상 없는데 어떻게 아이템을 써");
    }
    public Action ChackToDo(ItemInfo item, Action<string> setBtnText){
        List<ItemInfo> mouding = getArr(item.itemType);
        for(int i=0;i<mouding.Count;i++){
            if(mouding[i].itemId == item.itemId){
                setBtnText("장비헤제");
                return ()=>{mouding[i] = itemList.itemInfos.Find(x => x.itemId == 0);Debug.Log("아이템을 헤제시켰습니다");};
            }
        }
        for(int i=0;i<mouding.Count;i++){
            if(mouding[i].itemId == 0){
                setBtnText("장비장착");
                return ()=>{mouding[i] = item;Debug.Log("아이템을 장착했습니다");};
            }
        }
        setBtnText("교체");
        return ()=>{Debug.Log("교체할 아이템을 선택하세요");};
    }
    private List<ItemInfo> getArr(ItemType itemType){
        switch(itemType){
            case ItemType.weapon:
            return weapons;
            case ItemType.skill:
            return skills;
            case ItemType.accessories:
            return accessories;
            case ItemType.hilItem:
            return hilItems;
            default:
            return null;
        }
    }
    // public Action ItemMounting(ref List<ItemInfo> Mount, ItemInfo item){
    //     for(int i=0;i<Mount.Count;i++){
    //         if(Mount[i] == null){
    //             Action buttonDo = ()=>{Mount[i] = item;};
    //             break;
    //         }
    //     }

    // }
}
