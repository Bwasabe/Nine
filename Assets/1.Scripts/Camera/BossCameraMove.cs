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
    private Transform _playerMoveTransform = null;

    [SerializeField]
    private UnityEvent _bossEnterAnimationEvent = null;


    private bool isFirstEnter = false;

    private CinemachineVirtualCamera _vcam = null;

    private void Start() {
        _vcam = ObjectManager.Instance.VirtualCamera;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(isFirstEnter)return;
        isFirstEnter = true;
        //Cinemachine Do nothing
        _vcam.AddCinemachineComponent<CinemachineTransposer>();
        _vcam.DestroyCinemachineComponent<CinemachineTransposer>();

        ObjectManager.Instance.FadeObject.color = Vector4.zero;
        ObjectManager.Instance.FadeObject.DOFade(1f, 1f).OnComplete(() =>{
            _bossEnterAnimationEvent.Invoke();
            GameManager.Instance.PlayerMove.IsFreeze();
            GameManager.Instance.Player.transform.position = _playerMoveTransform.position;
        });

    }

    public void DebugEnter(){
        Debug.Log("엔터");
    }

    public void DebugExit(){
        Debug.Log("나감");

    }
}
