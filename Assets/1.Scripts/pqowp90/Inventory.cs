using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{  
    [SerializeField]
    ItemList itemList;
    [SerializeField]
    private List<ItemInfo> inventori = new List<ItemInfo>();
    public void AddItem(int itemId, int count){
        ItemInfo item = inventori.Find(x => x.itemId == itemId);
        if(item!=null){
            item.count+=count;
        }else{
            inventori.Add(itemList.itemInfos.Find(x => x.itemId == itemId));
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
    private void NoNoNoItem(){
        Debug.Log("아이템이 더이상 없는데 어떻게 아이템을 써");
    }

}
