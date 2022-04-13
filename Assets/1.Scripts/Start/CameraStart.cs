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
        ManagerStart.Instance.FadeObject.DOFade(0f, 1f);
        MainCam.transform.rotation = Quaternion.Euler(-60f, 0f, 0f);
        DOTween.To(() => MainCam.orthographicSize, x => {
            MainCam.orthographicSize = x;
        }, 8, 2f);
        transform.DORotate(Vector3.zero, 3f).OnComplete(() => backgrounds.ForEach(x => x.enabled = true));
        transform.DOMoveY(-10f, 3f);
    }


}
