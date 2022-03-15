using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewCardManager : MonoSingleton<NewCardManager>
{
    [SerializeField]
    private List<Transform> cardTs = new List<Transform>();


    [SerializeField]
    private float cardDuration = 1f;
    [SerializeField]
    private Transform leftTr;
    [SerializeField]
    private Transform rightTr;


    [SerializeField]
    private Transform cardRoot;
    [SerializeField]
    private Sprite reverseCard;

    private SpriteRenderer[] spriteRenderers;

    private float timer;


    private void Start()
    {
        spriteRenderers = cardRoot.GetComponentsInChildren<SpriteRenderer>();
        for(int i=0;i<spriteRenderers.Length;i++){
            cardTs.Add(spriteRenderers[i].transform);
        }
    }

    private void Update() {
        //CheckAppearCardTime();
        RoundAlignment();
    }
    private void CardUse()
    {
        cardRoot.DOLocalMoveY(1.7f, 0.3f).SetEase(Ease.OutQuint);
        cardRoot.DOScale(1.2f, 0.3f).SetEase(Ease.Linear);
        timer = 0f;
    }
    private void CheckAppearCardTime()
    {
        if (!cardRoot.gameObject.activeSelf) return;
        timer += Time.deltaTime;
        if (timer > cardDuration)
        {
            timer = 0f;
            FadeCards(0f, 0.1f);
        }
    }
    private void FadeCards(float fadeValue, float duration)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].DOFade(fadeValue, duration);
        }
    }

    

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
        }
    }
}


