using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CardManager : MonoBehaviour
{

    [SerializeField]
    private Transform leftTr;
    [SerializeField]
    private Transform rightTr;
    private Transform animationGo;
    [SerializeField]
    private Animator cardAnimation;


    private Action action;
    private void Update(){
        RoundAlignment();
    }
    private void Start(){
        action += ()=>{RoundAlignment();};
        PoolManager.CreatePool<CardOBJ>("CardPrefab", GameManager.Instance.gameObject);
        animationGo = cardAnimation.transform;
    }

    [SerializeField]
    private List<Transform> cardTs = new List<Transform>();
    [SerializeField]
    private List<Transform> usedCardTs = new List<Transform>();
    public void UseCard(){
        StartCoroutine(UseCardCorutin());
    }
    private IEnumerator UseCardCorutin(){
        yield return new WaitForSeconds(0f);
        CardOBJ CardPrefab = PoolManager.GetItem<CardOBJ>("CardPrefab");
        cardAnimation.Play("New State");
        cardAnimation.SetTrigger("GoCard");
        CardPrefab.transform.parent = animationGo;
        CardPrefab.transform.localPosition = Vector3.zero;
        CardPrefab.gameObject.SetActive(true);
    }
    private void Create(){
        
    }

    private void RoundAlignment()
    {
        int cardCount = cardTs.Count;
        if (cardCount == 1)
        {
            cardTs[0].transform.localPosition = Vector3.Lerp(cardTs[0].transform.localPosition, new Vector3(0f, rightTr.localPosition.y, 0f), 0.1f);
            return;
        }
        else if (cardCount == 0)
        {
            return;
        }
        float[] objLerps = new float[cardCount];

        float interval = 1f / (cardCount - 1);
        for (int i = 0; i < cardCount; i++)
            objLerps[i] = interval * i;

        for (int i = 0; i < cardCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.localPosition / 5f * cardCount, rightTr.localPosition / 5f * cardCount, objLerps[i]);
            cardTs[i].transform.localPosition = Vector3.Lerp(cardTs[i].transform.localPosition, targetPos, 0.1f);
        }
    }
}

