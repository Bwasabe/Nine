using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemRealPanel : MonoBehaviour
{
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text itemsulmyoung;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text buttonText;
    private ItemInfo item;
    private Action buttonTodo;
    
    public void SetAndShowPanel(ItemInfo item){
        if(item.itemId == 0){
            gameObject.SetActive(false);
            return;
        }
        buttonTodo = Inventory.Instance.ChackToDo(item, (text)=>{SetButtonText(text);});
        this.item = item;
        itemName.text = item.itemName;
        itemsulmyoung.text = item.explan;
        itemImage.sprite = item.itemSprite;
    }
    public void SetButtonText(string text){
        buttonText.text = text;
    }
    public void ClickButton(){
        buttonTodo();
        buttonTodo = Inventory.Instance.ChackToDo(item, (text)=>{SetButtonText(text);});
    }
    
}
