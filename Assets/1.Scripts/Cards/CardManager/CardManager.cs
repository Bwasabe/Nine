using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] cardImages;
    // [SerializeField]
    // private Transform leftTr;
    // [SerializeField]
    // private Transform rightTr;

    [SerializeField]
    private RectTransform cardMask;
    [SerializeField]
    private float cardSpacing = 68;
    [SerializeField]
    private float cardDuration = 1f;
    [SerializeField]
    private float firstAnchorPosX;



    [SerializeField]
    private Transform cardRoot;
    [SerializeField]
    private Transform[] cards;


    private SpriteRenderer[] spriteRenderers;
    private Card[] cardNumber;


    private float timer;
    private int currentCard = -1;


    private bool isCardUse;

    private IEnumerator Start()
    {
        //Debug.Log(Mathf.Atan2(105f, 70f) * Mathf.Rad2Deg);
        spriteRenderers = cardRoot.GetComponentsInChildren<SpriteRenderer>();
        yield return Yields.WaitForSeconds(0.5f);
        StartCoroutine(InitCards());
    }

    private IEnumerator InitCards()
    {
        for (int i = 4; i >= 0; i--)
        {
            cardImages[i].DOAnchorPosX(firstAnchorPosX + cardSpacing * i, 0.1f);
            yield return Yields.WaitForSeconds(0.1f);
        }
    }


    private void Update()
    {
        CheckCardKey();
        CheckAppearCardTime();
        UseCard();
    }


    private void UseCard()
    {
        if (Input.GetKeyDown(InputManager.keyMaps[Keys.USECARD]))
        {
            if (currentCard == -1 || isCardUse) return;
            isCardUse = true;
            CardUse();
            StartCoroutine(ImageUse());
        }
    }
    private IEnumerator ResetCard()
    {
        yield return Yields.WaitForSeconds(0.5f);
        spriteRenderers[currentCard].DOFade(1f, 0.3f).OnComplete(() =>
        {
            cards[currentCard].transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            cards[currentCard].DOLocalRotate(new Vector3(0f, 0f, 360f), 0.2f).OnComplete(() =>
            {
                cards[currentCard].DOLocalMoveY(0f, 0.3f).SetEase(Ease.InQuint);
                cards[currentCard].DOScale(new Vector2(0.5f, 0.9f), 0.3f).SetEase(Ease.Linear).OnComplete(() => isCardUse = false);
                currentCard = -1;
                FadeCards(1f, 0.2f);
            });
        });
    }
    private void CardUse()
    {
        cards[currentCard].DOLocalMoveY(1.7f, 0.3f).SetEase(Ease.OutQuint);
        cards[currentCard].DOScale(new Vector2(0.9f, 1.4f), 0.3f).SetEase(Ease.Linear);
        spriteRenderers[currentCard].DOFade(0f, 0.3f);
        timer = 0f;
        StartCoroutine(ResetCard());
    }
    private IEnumerator ImageUse()
    {
        int num = currentCard;
        Vector2 pos = cardImages[num].transform.position;
        cardMask.anchoredPosition = cardImages[num].anchoredPosition;
        //cardMask.transform.position = cardImages[num].transform.position;
        cardImages[num].SetParent(cardMask);
        for (int i = 0; i < 50; i++){
            cardMask.transform.position = (Vector2)cardMask.transform.position + (Vector2.one * 0.05f);
            cardImages[num].transform.position = pos;
            yield return Yields.WaitForSeconds(0.01f);
        }
        cardImages[num].SetParent(cardMask.transform.parent);
        cardImages[num].anchoredPosition = new Vector2(-100f, -110f);
        StartCoroutine(ImageReset(pos.x , num));
        //cardImages[num].gameObject.SetActive(false);
    }
    private IEnumerator ImageReset(float pos , int num){
        yield return Yields.WaitForSeconds(0.2f);
        cardImages[num].GetComponent<Image>().color = Color.gray;
        //TODO: 대충 뒷면
        cardImages[num].DOAnchorPosX(pos, 0.1f);
        yield return Yields.WaitForSeconds(0.1f);
        //cardImages[num].DOSizeDelta(new Vector2)
        cardImages[num].GetComponent<Image>().color = new Color(0.9622642f , 0.6218405f , 0.6218405f , 1f);
        //TODO: 대충 앞면
        
    }


    private void CheckCardKey()
    {
        if (isCardUse) return;
        if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD1]))
        {
            currentCard = 0;
        }
        else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD2]))
        {
            currentCard = 1;
        }
        else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD3]))
        {
            currentCard = 2;
        }
        else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD4]))
        {
            currentCard = 3;
        }
        else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD5]))
        {
            currentCard = 4;
        }
        else
        {
            return;
        }
        FadeCards(1f, 0.2f);
        timer = 0f;
        cardImages[currentCard].DOSizeDelta(new Vector2(70f, 105f), 0.3f);
        cards[currentCard].DOScale(new Vector2(0.7f, 1.2f), 0.3f);
        cards[currentCard].DOLocalMoveY(0.4f, 0.3f);
        Deselect(currentCard);
    }
    private void CheckAppearCardTime()
    {
        if (!cardRoot.gameObject.activeSelf) return;
        timer += Time.deltaTime;
        if (timer > cardDuration)
        {
            timer = 0f;
            FadeCards(0f, 0.1f);
            Deselect();
            currentCard = -1;
            //cardRoot.gameObject.SetActive(false);
        }
    }
    private void FadeCards(float fadeValue, float duration)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].DOFade(fadeValue, duration);
        }
    }



    private void Deselect(int value = -1)
    {
        for (int i = 0; i < 5; i++)
        {
            if (value != -1)
            {
                if (i == value) continue;
            }
            cardImages[i].DOSizeDelta(new Vector2(50f, 80), 0.3f);
            cards[i].DOScale(new Vector2(0.5f, 0.9f), 0.3f);
            cards[i].DOLocalMoveY(0f, 0.3f);
        }
    }
    // private void RoundAlignment()
    // {
    //     float[] objLerps = new float[5];
    //     float height = 0.5f;

    //     float interval = 1f / (5 - 1);
    //     for (int i = 0; i < 5; i++)
    //         objLerps[i] = interval * i;

    //     for (int i = 0; i < 5; i++)
    //     {
    //         var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
    //         var targetRot = Quaternion.identity;
    //         float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));

    //         targetPos.y += curve;
    //         targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
    //         cards[i].transform.position = targetPos;
    //         cards[i].transform.rotation = targetRot;
    //     }
    // }
}

