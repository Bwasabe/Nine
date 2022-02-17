using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraStart : MonoBehaviour
{
    [SerializeField]
    private List<BackgroundMove> backgrounds;


    private void Start() {
        DOTween.To(() => Camera.main.orthographicSize, x => {
            Camera.main.orthographicSize = x;
            Debug.Log(Camera.main.orthographicSize);
        }, 8, 2f);
        transform.DORotate(Vector3.zero, 3f).OnComplete(() => backgrounds.ForEach(x => x.enabled = true));
        transform.DOMoveY(0f, 3f);
    }


}
