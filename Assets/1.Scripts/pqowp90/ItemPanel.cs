using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private bool chackchack;
    public ItemInfo item;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Image itemSprite;
    [SerializeField]
    private GameObject isModing;
    
    public void ResetPanel(){
        
        itemSprite.sprite = item.itemSprite;
        if(chackchack)return;
        itemName.text = item.count.ToString();
        isModing.SetActive(item.isModeing);
    }
    public void ClickThis(){
        if(UIManager.Instance.ChangeItem && chackchack){
            Inventory.Instance.ChangeWhatItem(item, UIManager.Instance.willChangeItem);
            UIManager.Instance.CencleChaneItem();
        }
        if(item.itemId == 0)return;
        UIManager.Instance.ShowPanel(item, 2);
    }
    public void JustClickThis(int show){
        if(UIManager.Instance.ChangeItem && chackchack && show == 0){
            UIManager.Instance.ShowPanel(item, 3);
            return;
        }
        if(item.itemId == 0)return;
        UIManager.Instance.ShowPanel(item, show);
    }

}
