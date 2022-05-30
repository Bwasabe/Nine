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
    [SerializeField]
    private SpriteRenderer hand;

    public void Test(int count){
        foreach(ItemInfo item in itemList.itemInfos){
            if(item.itemId == 0){
                continue;
            }
            AddItem(item.itemId, count);
        }
        
    }
    private void Start(){
        CreateMyPanel(weapons);
        CreateMyPanel(skills);
        CreateMyPanel(accessories);
        CreateMyPanel(hilItems);
    }
    public void ChangeWhatItem(ItemInfo item, ItemInfo item2){
        SandMessge.MessegeBoxOn("교체했습니다", new Vector2(0f, -300f));
        if(item.itemType == ItemType.weapon)
            hand.sprite = item2.realSprite;
        getArr(item.itemType).Find(x => x.itemId == item.itemId).SetInfo(item2);
        UIManager.TriggerUI(item);
    }
    private void CreateMyPanel(List<ItemInfo> hi){
        foreach(ItemInfo item in hi){
            UIManager.Instance.CreatePanelByType(item, false);
        }
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
                setBtnText("해제");
                return ()=>{
                    if(item.itemType == ItemType.weapon)
                        hand.sprite = null;

                    ItemInfo go = itemList.itemInfos.Find(x => x.itemId == 0);
                    go.itemType = item.itemType;
                    mouding[i].SetInfo(go);
                    SandMessge.MessegeBoxOn("아이템을 해제했습니다", new Vector2(0f, -300f));
                    UIManager.TriggerUI(mouding[i]);

                };
            }
        }
        for(int i=0;i<mouding.Count;i++){
            if(mouding[i].itemId == 0){
                setBtnText("장착");
                return ()=>{
                    if(item.itemType == ItemType.weapon)
                        hand.sprite = item.realSprite;
                    mouding[i].SetInfo(item);
                    //여기서 손으로 장착
                    SandMessge.MessegeBoxOn("아이템을 장착했습니다", new Vector2(0f, -300f));
                    UIManager.Instance.GoButtonChange();
                    UIManager.TriggerUI(mouding[i]);
                };
            }
        }
        setBtnText("교체");
        //SandMessge.MessegeBoxOn("교체할 아이템을 선택하세요", new Vector2(0f, -300f));
        return ()=>{
            
            UIManager.Instance.GoButtonChange();
            UIManager.Instance.ChangeItem = true;
            UIManager.Instance.willChangeItem = item;
            SandMessge.MessegeBoxOn("교체할 아이템을 선택하세요", new Vector2(0f, -300f));
            
            
        };
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
