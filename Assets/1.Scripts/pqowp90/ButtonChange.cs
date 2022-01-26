using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonChange : MonoBehaviour
{
    [SerializeField]
    private bool isBig;
    protected static Action ButtonChangeMans;
    protected static Action ButtonChangeMans2;
    private Image buttonImage;
    private Outline outline;
    private void Awake(){
        buttonImage = GetComponent<Image>();
        outline = GetComponent<Outline>();
        if(isBig)ButtonChangeMans2 += ButtonChangeMan;
        else ButtonChangeMans += ButtonChangeMan;
        
    }
    private void ButtonChangeMan(){
        SetBtn(false);
    }

    private void SetBtn(bool on){
        outline.enabled = on;
        buttonImage.color = (on)?new Color(0.1294118f,0.1647059f,0.2078432f):new Color(0.3529412f,0.3490196f,0.3529412f);
    }
    public void PressThisButton(int myNum){
        if(UIManager.Instance.ChangeItem)return;
        if(!isBig){
            UIManager.Instance.OnOnePanel(myNum);
        }else{
            UIManager.Instance.OnOnePanel(myNum==10);
        }
        if(isBig)ButtonChangeMans2();
        else ButtonChangeMans();
        SetBtn(true);
    }
}
