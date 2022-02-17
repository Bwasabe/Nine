using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardOBJ : MonoBehaviour, IPoolable
{
    private Vector2 size;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Card myCard;
    public void OnPool(){
        if(spriteRenderer == null)spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = size;
        spriteRenderer.sprite = myCard.cardSprite;
        gameObject.SetActive(true);
    }
    void Awake()
    {
        size = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void UseThisCard(){
        transform.DOLocalMoveY(transform.position.y+0.1f, 0.2f);
        transform.DOScale(transform.localScale*1.2f, 0.1f);
    }
}
