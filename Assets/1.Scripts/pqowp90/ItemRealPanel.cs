using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRealPanel : MonoBehaviour
{
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text itemsulmyoung;
    [SerializeField]
    private Image itemImage;
    
    public void SetAndShowPanel(ItemInfo item){
        itemName.text = item.itemName;
        itemsulmyoung.text = item.explan;
        itemImage.sprite = item.itemSprite;
    }
    
}
