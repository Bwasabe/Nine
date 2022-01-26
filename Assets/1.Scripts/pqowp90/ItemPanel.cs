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
    
    public void ResetPanel(){
        itemName.text = item.count.ToString();
        itemSprite.sprite = item.itemSprite;
    }
    public void ClickThis(){
        if(item.itemId == 0)return;
        UIManager.Instance.ShowPanel(item, 2);
    }
    public void JustClickThis(int show){
        if(item.itemId == 0)return;
        UIManager.Instance.ShowPanel(item, show);
    }

}
