using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightingIntensity : MonoBehaviour
{
    Material outlineMat;

    private void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        outlineMat = sr.material;
    }

    private void Start()
    {
        DOTween.To(
            () => outlineMat.GetFloat("_Intensity"),
            value => outlineMat.SetFloat("_Intensity", value),
            1.5f, 2f
        ).SetLoops(-1, LoopType.Yoyo);
    }
}
