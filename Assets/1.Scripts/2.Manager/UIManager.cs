using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private ItemRealPanel itemRealPanel1;
    [SerializeField]
    private GameObject accessoriPanel;
    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject inputManager;

    private PlayerController player;
    private PlayerMove playerMove;
    

    private void Start() {
        player = GameManager.Instance.Player;
        playerMove = GameManager.Instance.PlayerMove;
        AddAccessoriPanel();
    }
    public void OnClickOption(){
        if(inputManager == null) return;
        inputManager.SetActive(true);
        optionPanel.SetActive(true);
        player.slide -= player.getSlide;
        playerMove.IsFreeze();

        
    }

    public void OnClickExit(){
        if(inputManager == null) return;
        inputManager.SetActive(false);
        optionPanel.SetActive(false);
        player.slide += player.getSlide;
        playerMove.IsMove();
    }

    private static void CreatePanel(ItemInfo item, string eventName, GameObject preFab){
        ItemPanel itemPanel = Instantiate(preFab, preFab.transform.parent).GetComponent<ItemPanel>();
        itemPanel.item = item;
        Action action = ()=>{itemPanel.ResetPanel();};
        action();
        EventManager.StartListening(eventName, action);
        itemPanel.gameObject.SetActive(true);
    }
    public void AddAccessoriPanel(ItemInfo item = null){
        if(item == null){
            foreach(ItemInfo _item in Inventory.Instance.inventori){
                CreatePanel(_item, "RESET_ACCESSORI_ITEM", accessoriPanel);
            }
        }else
            CreatePanel(item, "RESET_ACCESSORI_ITEM", accessoriPanel);
    }
    public void ShowPanel(ItemInfo item){
        itemRealPanel1.SetAndShowPanel(item);
        itemRealPanel1.gameObject.SetActive(true);
    }
    public void HidePanel(){
        itemRealPanel1.gameObject.SetActive(true);
    }
}
