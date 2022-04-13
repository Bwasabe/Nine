using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    [SerializeField]
    private Image _fadeObj = null;

    public Image FadeObject
    {
        get
        {
            return _fadeObj;
        }
    }


    [SerializeField]
    private CinemachineVirtualCamera _vCam = null;

    public CinemachineVirtualCamera VirtualCamera
    {
        get
        {
            return _vCam;
        }
    }


    [Header("위아래 바")]
    [SerializeField]
    private RectTransform _underBar = null;

    [SerializeField]
    private RectTransform _topBar = null;

    [Header("바 관련 변수들")]
    [SerializeField]
    private float _barPos = 75f;
    [SerializeField]
    private float _barMoveDuration = 1f;

    public void ShowBarImage()
    {
        _underBar.DOAnchorPosY(_barPos, _barMoveDuration);
        _topBar.DOAnchorPosY(-_barPos, _barMoveDuration);
    }

    public void DisappearBarImage()
    {
        _underBar.DOAnchorPosY(-_barPos, _barMoveDuration);
        _topBar.DOAnchorPosY(_barPos, _barMoveDuration);
    }


}
