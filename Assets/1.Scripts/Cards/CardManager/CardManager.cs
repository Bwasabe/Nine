using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] cardImages;

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

    private Vector2 oldSize;
    private Vector2 bigSize;

    private float timer;
    private int currentCard = -1;


    private bool isCardUse;

    private IEnumerator Start()
    {
        //Debug.Log(Mathf.Atan2(105f, 70f) * Mathf.Rad2Deg);
        spriteRenderers = cardRoot.GetComponentsInChildren<SpriteRenderer>();
        oldSize = cards[0].localScale;
        bigSize = Vector2.one * 1.1f;
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
                cards[currentCard].DOScale(oldSize, 0.3f).SetEase(Ease.Linear);
            });
        });

        yield return Yields.WaitForSeconds(1f);
        cards[currentCard].DOScale(new Vector2(0.2f, 1.3f), 0.05f).OnComplete(() =>
        {
            cards[currentCard].DOScale(oldSize, 0.05f);
        });
        yield return Yields.WaitForSeconds(0.1f);
        currentCard = -1;
        FadeCards(1f, 0.2f);
        isCardUse = false;
    }
    private void CardUse()
    {
        cards[currentCard].DOLocalMoveY(1.7f, 0.3f).SetEase(Ease.OutQuint);
        cards[currentCard].DOScale(bigSize* 1.2f, 0.3f).SetEase(Ease.Linear);
        spriteRenderers[currentCard].DOFade(0f, 0.3f);
        timer = 0f;
        ImageDeselect();
        StartCoroutine(ResetCard());
    }
    private IEnumerator ImageUse()
    {
        int num = currentCard;
        cardMask.anchoredPosition = cardImages[num].anchoredPosition;
        cardImages[num].SetParent(cardMask);
        for (int i = 0; i < 50; i++)
        {
            Vector2 pos = cardImages[num].position;
            cardMask.anchoredPosition = cardMask.anchoredPosition + Vector2.one*5f;
            cardImages[num].position = pos;
            //Debug.Log(pos);
            yield return Yields.WaitForSeconds(0.01f);
        }
        cardImages[num].SetParent(cardMask.transform.parent);
        cardImages[num].anchoredPosition = new Vector2(-100f, -150f);
        yield return Yields.WaitForSeconds(0.2f);
        StartCoroutine(ImageReset(num));
    }
    private IEnumerator ImageReset(int num)
    {
        Image cardImage = cardImages[num].GetComponent<Image>();
        Color oldColor = cardImage.color;
        cardImages[num].pivot = Vector2.one * 0.5f;
        yield return Yields.WaitForSeconds(0.1f);
        cardImage.color = Color.gray;
        //TODO: 대충 뒷면
        cardImages[num].transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        cardImages[num].DOLocalRotate(new Vector3(0f, 0f, 360f), 0.3f);
        cardImages[num].DOAnchorPosX(firstAnchorPosX + cardSpacing * num, 0.3f);
        yield return Yields.WaitForSeconds(0.5f);
        cardImages[num].DOScale(new Vector2(0.2f, 1.3f), 0.05f).OnComplete(() =>
        {
            cardImages[num].DOScale(Vector2.one, 0.05f);
        });
        yield return Yields.WaitForSeconds(0.1f);
        cardImage.color = oldColor;
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
        cards[currentCard].DOScale(bigSize, 0.3f);
        cards[currentCard].DOLocalMoveY(0.4f, 0.3f);
        CardDeselect(currentCard);
        ImageDeselect(currentCard);

    }
    private void CheckAppearCardTime()
    {
        if (!cardRoot.gameObject.activeSelf) return;
        timer += Time.deltaTime;
        if (timer > cardDuration)
        {
            timer = 0f;
            FadeCards(0f, 0.1f);
            CardDeselect();
            ImageDeselect();
            currentCard = -1;
        }
    }
    private void FadeCards(float fadeValue, float duration)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].DOFade(fadeValue, duration);
        }
    }



    private void CardDeselect(int value = -1)
    {
        for (int i = 0; i < 5; i++)
        {
            if (value != -1)
            {
                if (i == value) continue;
            }
            cards[i].DOScale(oldSize, 0.3f);
            cards[i].DOLocalMoveY(0f, 0.3f);
        }
    }
    private void ImageDeselect(int value = -1)
    {
        for (int i = 0; i < 5; i++)
        {
            if (value != -1)
            {
                if (i == value) continue;
            }
            cardImages[i].DOSizeDelta(new Vector2(50f, 80), 0.3f);
        }
    }

}

