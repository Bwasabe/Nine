using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private GameObject[] UIPanels;
    [SerializeField]
    private GameObject[] BigUIPanels;
    [SerializeField]
    private ItemRealPanel itemRealPanel1;
    [SerializeField]
    private ItemRealPanel itemRealPanel2;



    [SerializeField]
    private GameObject weaponPanel;
    [SerializeField]
    private GameObject skillPanel;
    [SerializeField]
    private GameObject accessoriPanel;
    [SerializeField]
    private GameObject hilItemPanel;


    [SerializeField]
    private GameObject weaponPanel2;
    [SerializeField]
    private GameObject skillPanel2;
    [SerializeField]
    private GameObject accessoriPanel2;
    [SerializeField]
    private GameObject hilItemPanel2;



    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject inputManager;

    private PlayerController player;
    private PlayerMove playerMove;
    private ItemInfo lastItem1;
    private ItemInfo lastItem2;

    private Vector2 screenSize;

    public bool ChangeItem = false;
    public ItemInfo willChangeItem;
    [SerializeField]
    private GameObject cencleChaneItemBtn;
    [SerializeField]
    private ButtonChange buttonChange;

    private void Update(){
        cencleChaneItemBtn.SetActive(ChangeItem);
    }
    public void CencleChaneItem(){
        willChangeItem = null;
        ChangeItem = false;
    }

    private void Start() {
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y*Camera.main.aspect;
        player = GameManager.Instance.Player;
        playerMove = GameManager.Instance.PlayerMove;
        AddPanel();
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
    public void OnOnePanel(int num){
        int i=0;
        foreach(GameObject panel in UIPanels){
            if(panel == null)continue;
            panel.SetActive(i == num || i == num+4);
            i++;
        }
    }
    public void GoButtonChange(){
        buttonChange.PressThisButton(20);
    }
    public void OnOnePanel(bool num){
        BigUIPanels[0].transform.DOLocalMoveX(1500f*((num)?0f:-1f), 0.5f);
        BigUIPanels[1].transform.DOLocalMoveX(1500f*((!num)?0f:1f), 0.5f);
        // BigUIPanels[0].SetActive(num);
        // BigUIPanels[1].SetActive(!num);
    }
    public void ClickItemPanel(ItemInfo item, PanelStatus num){
        itemRealPanel2.SetAndShowPanel(item);
    }
    private void InfoPanel(PanelStatus num, ItemInfo item, ItemInfo item2 = null){
        
        switch(num){
            case PanelStatus.Mounting:
                itemRealPanel2.SetAndShowPanel(item);
                itemRealPanel1.gameObject.SetActive(false);
                itemRealPanel2.gameObject.SetActive(true);
                itemRealPanel2.transform.DOMoveY(2.928f,0.5f).SetEase(Ease.OutCirc);
            break;
            case PanelStatus.Dismount:
                itemRealPanel2.SetAndShowPanel(item);
                itemRealPanel1.gameObject.SetActive(false);
                itemRealPanel2.gameObject.SetActive(true);
                itemRealPanel2.transform.DOMoveY(2.928f,0.5f).SetEase(Ease.OutCirc);
            break;
            case PanelStatus.Replace:
                itemRealPanel2.SetAndShowPanel(item);
                itemRealPanel1.SetAndShowPanel(item2);
                itemRealPanel1.gameObject.SetActive(true);
                itemRealPanel2.gameObject.SetActive(true);
                itemRealPanel2.transform.DOMoveY(-3.46f,0.5f).SetEase(Ease.OutCirc);
            break;
        }

    }
    public enum PanelStatus{
        Mounting,
        Replace,
        Dismount,
    }

    private static void CreatePanel(ItemInfo item, string eventName, GameObject preFab){
        ItemPanel itemPanel = Instantiate(preFab, preFab.transform.parent).GetComponent<ItemPanel>();
        itemPanel.item = item;
        Action action = ()=>{itemPanel.ResetPanel();};
        action();
        EventManager.StartListening(eventName, action);
        itemPanel.gameObject.SetActive(true);
    }
    public void AddPanel(ItemInfo item = null){
        if(item == null){
            foreach(ItemInfo _item in Inventory.Instance.inventori){
                CreatePanelByType(_item, true);
            }
        }else
            CreatePanelByType(item, true);
    }
    public void CreatePanelByType(ItemInfo _item, bool gogogo){
        switch(_item.itemType){
            case ItemType.weapon:
                CreatePanel(_item, "RESET_WEAPON_ITEM", (gogogo)?weaponPanel:weaponPanel2);
            break;
            case ItemType.skill:
                CreatePanel(_item, "RESET_SKILL_ITEM", (gogogo)?skillPanel:skillPanel2);
            break;
            case ItemType.accessories:
                CreatePanel(_item, "RESET_ACCESSORI_ITEM", (gogogo)?accessoriPanel:accessoriPanel2);
            break;
            case ItemType.hilItem:
                CreatePanel(_item, "RESET_HILITEM_ITEM", (gogogo)?hilItemPanel:hilItemPanel2);
            break;
        }
    }
    public void ShowPanel(ItemInfo item, int show){
        if(show == 0){
            InfoPanel(PanelStatus.Mounting, item);
        }else if(show == 1){
            if(lastItem1 == null){
                return;
            }
            InfoPanel(PanelStatus.Mounting, lastItem1);
        }else if(show == 2){
            lastItem1 = item;
            InfoPanel(PanelStatus.Mounting, lastItem1);
        }else if(show == 3){
            InfoPanel(PanelStatus.Replace, lastItem1, item);
        }
    }
    public void HidePanel(){
        itemRealPanel1.gameObject.SetActive(true);
    }
    public static void TriggerUI(ItemInfo item){
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
    }
}
