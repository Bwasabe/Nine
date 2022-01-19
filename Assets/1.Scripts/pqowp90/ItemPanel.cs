using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ItemPanel : MonoBehaviour
{
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
        UIManager.Instance.ShowPanel(item);
    }

}
