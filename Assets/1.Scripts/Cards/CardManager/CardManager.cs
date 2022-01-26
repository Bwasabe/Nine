using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    // [SerializeField]
    // private RectTransform[] cardRects;

    // [SerializeField]
    // private RectTransform cardMask;
    // [SerializeField]
    // private float cardSpacing = 68;
    // [SerializeField]
    // private float cardDuration = 1f;
    // [SerializeField]
    // private float firstAnchorPosX;
    [SerializeField]
    private Transform leftTr;
    [SerializeField]
    private Transform rightTr;


    // [SerializeField]
    // private Transform cardRoot;
    // [SerializeField]
    // private RectTransform cardImageRoot;
    // [SerializeField]
    // private Transform[] cards;
    // [SerializeField]
    // private Sprite reverseCard;

    // private SpriteRenderer[] spriteRenderers;
    // private Image[] cardImages;
    // private Card[] cardNumber;
    // private float[] cardPosY = {0,0,0,0,0 };

    // private Vector2 oldSize;
    // private Vector2 bigSize;

    // private float timer;
    // private float cardImagePosY;


    // private IEnumerator Start()
    // {
    //     //Debug.Log(Mathf.Atan2(105f, 70f) * Mathf.Rad2Deg);
    //     spriteRenderers = cardRoot.GetComponentsInChildren<SpriteRenderer>();
    //     cardImages = cardImageRoot.GetComponentsInChildren<Image>();
    //     oldSize = cards[0].localScale;
    //     bigSize = Vector2.one * 1.1f;
    //     cardImagePosY = cardRects[0].anchoredPosition.y;
    //     for (int i = 0; i < cards.Length; i++){
    //         cardPosY[i] = cards[i].localPosition.y;
    //         Debug.Log(cardPosY[i]);
    //     }
    //     yield return Yields.WaitForSeconds(0.5f);
    //     StartCoroutine(InitCards());
    //     CardSprite();
    // }

    // private IEnumerator InitCards()
    // {
    //     for (int i = 4; i >= 0; i--)
    //     {
    //         cardRects[i].DOAnchorPosX(firstAnchorPosX + cardSpacing * i, 0.1f);
    //         yield return Yields.WaitForSeconds(0.1f);
    //     }
    // }

    // private void Update()
    // {
    //     //CheckCardKey();
    //     CheckAppearCardTime();
    //     //UseCard();
    // }


    // private void UseCard()
    // {
    //     if (Input.GetKeyDown(InputManager.keyMaps[Keys.USECARD]))
    //     {
    //         if (currentCard == -1 || isCardUse) return;
    //         isCardUse = true;
    //         CardUse();
    //         StartCoroutine(ImageUse());
    //     }
    // }
    // private IEnumerator ResetCard(Sprite sprite)
    // {
    //     yield return Yields.WaitForSeconds(0.5f);
    //     spriteRenderers[currentCard].DOFade(1f, 0.3f).OnComplete(() =>
    //     {
    //         cards[currentCard].transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    //         cards[currentCard].DOLocalRotate(new Vector3(0f, 0f, 360f), 0.2f).OnComplete(() =>
    //         {
    //             cards[currentCard].DOLocalMoveY(cardPosY[currentCard],0.3f).SetEase(Ease.InQuint);
    //             cards[currentCard].DOScale(oldSize, 0.3f).SetEase(Ease.Linear);
    //         });
    //     });

    //     yield return Yields.WaitForSeconds(1f);
    //     cards[currentCard].DOScale(new Vector2(0.2f, 1.3f), 0.05f).OnComplete(() =>
    //     {
    //         //TODO: 대충 앞면
    //         spriteRenderers[currentCard].sprite = sprite;
    //         cards[currentCard].DOScale(oldSize, 0.05f);
    //     });
    //     yield return Yields.WaitForSeconds(0.1f);
    //     FadeCards(1f, 0.2f);
    // }
    // private void CardUse()
    // {
    //     Sprite sprite = spriteRenderers[currentCard].sprite;
    //     cards[currentCard].DOLocalMoveY(cardPosY[currentCard]+ 1.7f, 0.3f).SetEase(Ease.OutQuint);
    //     cards[currentCard].DOScale(bigSize * 1.2f, 0.3f).SetEase(Ease.Linear);
    //     spriteRenderers[currentCard].DOFade(0f, 0.3f).OnComplete(() => spriteRenderers[currentCard].sprite = reverseCard);
    //     timer = 0f;
    //     ImageDeselect();
    //     StartCoroutine(ResetCard(sprite));
    // }
    // private IEnumerator ImageUse()
    // {
    //     int num = currentCard;
    //     cardMask.anchoredPosition = cardRects[num].anchoredPosition;
    //     cardRects[num].SetParent(cardMask);
    //     for (int i = 0; i < 50; i++)
    //     {
    //         Vector2 pos = cardRects[num].position;
    //         cardMask.anchoredPosition = cardMask.anchoredPosition + Vector2.one * 5f;
    //         cardRects[num].position = pos;
    //         //Debug.Log(pos);
    //         yield return Yields.WaitForSeconds(0.01f);
    //     }
    //     cardRects[num].SetParent(cardImageRoot);
    //     cardRects[num].anchoredPosition = new Vector2(-100f, cardImagePosY);
    //     yield return Yields.WaitForSeconds(0.2f);
    //     StartCoroutine(ImageReset(num));
    // }
    // private IEnumerator ImageReset(int num)
    // {
    //     cardRects[num].pivot = Vector2.one * 0.5f;
    //     yield return Yields.WaitForSeconds(0.1f);
    //     //TODO: 대충 뒷면
    //     cardImages[num].sprite = reverseCard;
    //     cardRects[num].transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    //     cardRects[num].DOLocalRotate(new Vector3(0f, 0f, 360f), 0.3f);
    //     cardRects[num].DOAnchorPosX(firstAnchorPosX + cardSpacing * num, 0.3f);
    //     yield return Yields.WaitForSeconds(0.5f);
    //     cardRects[num].DOScale(new Vector2(0.2f, 1.3f), 0.05f).OnComplete(() =>
    //     {
    //         cardRects[num].DOScale(Vector2.one, 0.05f);
    //     });
    //     yield return Yields.WaitForSeconds(0.1f);
    //     //TODO: 대충 앞면
    //     cardImages[num].sprite = spriteRenderers[num].sprite;

    // }



    // private void CheckCardKey()
    // {
    //     if (isCardUse) return;
    //     if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD1]))
    //     {
    //         currentCard = 0;
    //     }
    //     else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD2]))
    //     {
    //         currentCard = 1;
    //     }
    //     else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD3]))
    //     {
    //         currentCard = 2;
    //     }
    //     else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD4]))
    //     {
    //         currentCard = 3;
    //     }
    //     else if (Input.GetKeyDown(InputManager.keyMaps[Keys.CARD5]))
    //     {
    //         currentCard = 4;
    //     }
    //     else
    //     {
    //         return;
    //     }
    //     FadeCards(1f, 0.2f);
    //     timer = 0f;
    //     cardRects[currentCard].DOSizeDelta(new Vector2(70f, 105f), 0.3f);
    //     cards[currentCard].DOScale(bigSize, 0.3f);
    //     cards[currentCard].DOLocalMoveY(cardPosY[currentCard],0.3f);
    //     CardDeselect(currentCard);
    //     ImageDeselect(currentCard);

    // }
    // private void CheckAppearCardTime()
    // {
    //     if (!cardRoot.gameObject.activeSelf) return;
    //     timer += Time.deltaTime;
    //     if (timer > cardDuration)
    //     {
    //         timer = 0f;
    //         FadeCards(0f, 0.1f);
    //         CardDeselect();
    //         ImageDeselect();
    //     }
    // }
    // private void FadeCards(float fadeValue, float duration)
    // {
    //     for (int i = 0; i < spriteRenderers.Length; i++)
    //     {
    //         spriteRenderers[i].DOFade(fadeValue, duration);
    //     }
    // }

    // private void CardSprite()
    // {
    //     for (int i = 0; i < cardRects.Length; i++)
    //     {
    //         cardRects[i].GetComponent<Image>().sprite = spriteRenderers[i].sprite;
    //     }
    // }

    // private void CardDeselect(int value = -1)
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         if (value != -1)
    //         {
    //             if (i == value) continue;
    //         }
    //         cards[i].DOScale(oldSize, 0.3f);
    //         cards[i].DOLocalMoveY(cardPosY[i], 0.3f);
    //     }
    // }
    // private void ImageDeselect(int value = -1)
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         if (value != -1)
    //         {
    //             if (i == value) continue;
    //         }
    //         cardRects[i].DOSizeDelta(new Vector2(50f, 80), 0.3f);
    //     }
    // }

    // private void Update(){
    //     RoundAlignment();
    // }

    [SerializeField]
    private List<Transform> cardTs = new List<Transform>();

    [ContextMenu("원형")]
    private void RoundAlignment()
    {
        float[] objLerps = new float[cardTs.Count];

        switch (cardTs.Count)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (cardTs.Count - 1);
                for (int i = 0; i < cardTs.Count; i++)
                    objLerps[i] = interval * i;
                break;
        }
        for (int i = 0; i < cardTs.Count; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);

            var targetRot = Quaternion.identity;
            if (cardTs.Count >= 4)
            {

                float curve = Mathf.Sqrt(Mathf.Pow(0.5f, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            cardTs[i].DOMove(targetPos , 1f);
            cardTs[i].DORotate(targetRot.eulerAngles, 1f);
            //cardTs[i].transform.SetPositionAndRotation(targetPos, targetRot);
        }
    }
}
//     private void RoundAlignment()
//     {
//         int cardCount = cardTs.Count;
//         if (cardCount == 1)
//         {
//             cardTs[0].transform.localPosition = Vector3.Lerp(cardTs[0].transform.localPosition, new Vector3(0f, rightTr.localPosition.y, 0f), 0.1f);
//             return;
//         }
//         else if (cardCount == 0)
//         {
//             return;
//         }
//         float[] objLerps = new float[cardCount];

//         float interval = 1f / (cardCount - 1);
//         for (int i = 0; i < cardCount; i++)
//             objLerps[i] = interval * i;

//         for (int i = 0; i < cardCount; i++)
//         {
//             var targetPos = Vector3.Lerp(leftTr.localPosition / 5f * cardCount, rightTr.localPosition / 5f * cardCount, objLerps[i]);
//             cardTs[i].transform.localPosition = Vector3.Lerp(cardTs[i].transform.localPosition, targetPos, 0.1f);
//         }
//     }
// }

