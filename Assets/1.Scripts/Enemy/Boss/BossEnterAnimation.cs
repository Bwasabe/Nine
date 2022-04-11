using static Define;
using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossEnterAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform _camStartPos = null;
    [SerializeField]
    private Transform _camTowardPos = null;
    [SerializeField]
    private Transform _camEndPos = null;

    [SerializeField]
    private AnimationClip _enterAnimationClip = null;
    [SerializeField]
    private AnimationClip _returnAnimationClip = null;


    [SerializeField]
    private float _animationStopTime = 2.15f;
    [SerializeField]
    private float _orthographicSize = 3f;


    [SerializeField]
    private SpriteRenderer _bossEye = null;

    [SerializeField]
    private float _bossEyeMovement = 0.1f;

    private Animator _animator = null;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void EnterAnimation()
    {
        ObjectManager.Instance.ShowBarImage();
        VirtualCamera.m_Lens.OrthographicSize = _orthographicSize;
        FadeObject.DOFade(0f, 0.8f);
        VirtualCamera.transform.position = _camStartPos.position;
        VirtualCamera.transform.DOMove(_camTowardPos.position, _animationStopTime).SetEase(Ease.Linear);
        Debug.Log("윙");
        _animator.Play(_enterAnimationClip.name);
    }

    public void ExitAnimation()
    {
        StartCoroutine(BossAnimationExit());
    }

    private IEnumerator BossAnimationExit()
    {
        yield return WaitForSeconds(0.2f);
        _bossEye.gameObject.SetActive(true);
        _bossEye.DOFade(0f, 1.5f);
        yield return WaitForSeconds(2f);
        ObjectManager.Instance.DisappearBarImage();
        DOTween.To(
            () => VirtualCamera.m_Lens.OrthographicSize,
            value => VirtualCamera.m_Lens.OrthographicSize = value,
            10f, 1f
        );
        VirtualCamera.transform.DOMove(_camEndPos.position, 1f).OnComplete(() =>{
            GameManager.Instance.PlayerMove.IsMove();
        });
    }

    public void BossEyeDown(){
        _bossEye.transform.position -= Vector3.up * _bossEyeMovement;
    }

    public void BossEyeUp(){
        _bossEye.transform.position += Vector3.up * _bossEyeMovement;
    }

}
