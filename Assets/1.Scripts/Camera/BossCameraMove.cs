using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Events;

public class BossCameraMove : MonoBehaviour
{
    [SerializeField]
    private float _moveDuration;
    [SerializeField]
    private float _cameraSize;

    [SerializeField]
    private Vector3 _movePos = Vector3.zero;

    [SerializeField]
    CinemachineVirtualCamera _cam = null;

    private CinemachineTransposer _camTransposer = null;

    [SerializeField]
    private UnityAction _bossAnimationAction = null;
    private void Start() {
        _camTransposer = _cam.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _cam.AddCinemachineComponent<CinemachineTransposer>();
        _cam.DestroyCinemachineComponent<CinemachineTransposer>();
        gameObject.SetActive(false);
        MaincamTransform.DOMove(_movePos, _moveDuration);
        _cam.transform.DOMove(_movePos, _moveDuration);

        DOTween.To(
            () => _cam.m_Lens.OrthographicSize,
            value => _cam.m_Lens.OrthographicSize = value,
            _cameraSize, _moveDuration
        );

    }
}
