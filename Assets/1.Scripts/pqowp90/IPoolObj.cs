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
    private void Awake(){
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }
    
    public void OnPool(){
        transform.DOKill();
        image.DOKill();
        text.DOKill();
        image.DOFade(1f,0f);
        text.DOFade(1f,0f);
        
        StartCoroutine(Die());
        
    }
    public void SetText(string text){
        this.text.text = text;
    }
    private IEnumerator Die(){
        yield return new WaitForSeconds(lifeTime);
        transform.DOLocalMoveY(-500f, 2f);
        image.DOFade(0f,disableTime).OnComplete(()=>gameObject.SetActive(false));
        text.DOFade(0f,disableTime);
    }




}
