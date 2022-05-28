using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IPoolObj : MonoBehaviour, IPoolable
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float disableTime;
    private Image image;
    private Text text;
    public bool isTextBox;
    private SpriteRenderer spriteRenderer;
    private void Awake(){
        if(!isTextBox){
            image = GetComponent<Image>();
            text = GetComponentInChildren<Text>();
        }else{
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
    
    public void OnPool(){
        if(!isTextBox){
            transform.DOKill();
            image.DOKill();
            text.DOKill();
            image.DOFade(1f,0f);
            text.DOFade(1f,0f);
            
            StartCoroutine(Die());
        }else{
            if(spriteRenderer == null){
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            spriteRenderer.color = new Color(0f,0f,0f,0.7f);
            spriteRenderer.DOColor(new Color(0f,0f,0f,0.2f), 0.3f).OnComplete(()=>{gameObject.SetActive(false);});
        }
        
    }
    public void SetSprite(Sprite image, bool flip){
        spriteRenderer.sprite = image;
        spriteRenderer.flipX = flip;
    }
    public void SetText(string text){
        this.text.text = text;
    }
    private IEnumerator Die(){
        yield return new WaitForSeconds(lifeTime);
        transform.DOLocalMoveY(-500f, 2f);
        image.DOFade(0f,disableTime).OnComplete(()=>{gameObject.SetActive(false);});
        text.DOFade(0f,disableTime);
    }
    public void Hide(){
        transform.DOKill();
        gameObject.SetActive(false);
    }




}
