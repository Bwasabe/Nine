using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TouchToStart : MonoBehaviour
{
    private RectTransform _rectTransform = null;
    private void Start() {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.DOScale(1.1f, 0.7f).SetLoops(-1 , LoopType.Yoyo);
    }

    private void Update() {
        if(Input.anyKeyDown){
            SceneManager.LoadScene("건들지마 2");
        }
    }
}
