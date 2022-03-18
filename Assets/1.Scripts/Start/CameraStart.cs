using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Define;

public class CameraStart : MonoBehaviour
{
    [SerializeField]
    private List<BackgroundMove> backgrounds;


    private void Start() {
        MainCam.orthographicSize = 8f;
        MainCam.transform.rotation = Quaternion.Euler(-60f, 0f, 0f);
        DOTween.To(() => MainCam.orthographicSize, x => {
            MainCam.orthographicSize = x;
            Debug.Log(MainCam.orthographicSize);
        }, 8, 2f);
        transform.DORotate(Vector3.zero, 3f).OnComplete(() => backgrounds.ForEach(x => x.enabled = true));
        transform.DOMoveY(0f, 3f);
    }


}
