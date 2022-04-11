using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Events;

public class BossCameraMove : MonoBehaviour
{

    private CinemachineTransposer _camTransposer = null;

    [SerializeField]
    private UnityEvent _bossEnterAnimationEvent = null;


    private bool isFirstEnter = false;

    private void Start() {
        _camTransposer = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(isFirstEnter)return;
        isFirstEnter = true;
        //Cinemachine Do nothing
        VirtualCamera.AddCinemachineComponent<CinemachineTransposer>();
        VirtualCamera.DestroyCinemachineComponent<CinemachineTransposer>();

        FadeObject.color = Vector4.zero;
        FadeObject.DOFade(1f, 1f).OnComplete(() =>{
            _bossEnterAnimationEvent.Invoke();
            GameManager.Instance.PlayerMove.IsFreeze();
        });

    }

    public void DebugEnter(){
        Debug.Log("엔터");
    }

    public void DebugExit(){
        Debug.Log("나감");

    }
}
