using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{  
    [SerializeField]
    ItemList itemList;
    [SerializeField]
    public List<ItemInfo> inventori = new List<ItemInfo>();
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
            switch (item.itemType)
            {
                case ItemType.accessories:
                    EventManager.TriggerEvent("RESET_ACCESSORI_ITEM");
                    break;
            }
        }else{
            item = itemList.itemInfos.Find(x => x.itemId == itemId).DeepCopy();
            item.count = 1;
            inventori.Add(item);
            switch (item.itemType)
            {
                case ItemType.accessories:
                    UIManager.Instance.AddAccessoriPanel(item);
                    break;
            }
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

}
