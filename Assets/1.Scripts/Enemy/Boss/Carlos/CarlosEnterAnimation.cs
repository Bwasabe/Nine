using static Define;
using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

[RequireComponent(typeof(EnemyAI))]
public class CarlosEnterAnimation : MonoBehaviour
{
    [Header("카메라 움직이는 위치")]
    [SerializeField]
    private Transform _camStartPos = null;
    [SerializeField]
    private Transform _camTowardPos = null;
    [SerializeField]
    private Transform _camEndPos = null;

    [Header("보스 시작 애니메이션")]
    [SerializeField]
    private AnimationClip _enterAnimationClip = null;

    [Header("보스 텍스트 관련")]
    [SerializeField]
    private TMP_Text _bossNameText = null;
    [SerializeField]
    private Text _bossExplainText = null;
    [SerializeField]
    private float _bossTextShowDuration = 2f;
    [SerializeField]
    private float _bossNameTextAnchorPosX = -700f;

    [Header("카메라 관련")]
    [SerializeField]
    private float _animationStopTime = 2.15f;
    [SerializeField]
    private float _orthographicSize = 3f;

    [Header("보스 눈 관련")]
    [SerializeField]
    private SpriteRenderer _bossEye = null;
    [SerializeField]
    private Transform _bossEyeEffectTransform = null;
    [SerializeField]
    private float _bossEyeScaleX = 2f;
    [SerializeField]
    private float _bossEyeDuration = 1.5f;

    [SerializeField]
    private float _bossEyeMovement = 0.1f;

    [Header("카메라 흔들기 속성")]
    [SerializeField]
    private float _shakeCamDuration = 0.2f;
    [SerializeField]
    private float _shakeCamStrength = 3f;
    [SerializeField]
    private int _shakeCamVibrato = 10;
    [SerializeField]
    private float _shakeCamRandomness = 45f;
    [Header("보스 주사위")]
    [SerializeField]
    private MeshRenderer _bossDice = null;


    [SerializeField]
    private GameObject _wall = null;

    private Animator _animator = null;
    private EnemyAI _enemyAI = null;

    private CinemachineVirtualCamera _vcam = null;

    private CarlosAttack _carlosAttack = null;

    private void Start()
    {
        _vcam = ObjectManager.Instance.VirtualCamera;
        _animator = GetComponent<Animator>();
        _enemyAI = GetComponent<EnemyAI>();
        _carlosAttack = GetComponent<CarlosAttack>();
    }

    public void EnterAnimation()
    {
        ObjectManager.Instance.ShowBarImage();
        _vcam.m_Lens.OrthographicSize = _orthographicSize;
        ObjectManager.Instance.FadeObject.DOFade(0f, 0.8f);
        _vcam.transform.position = _camStartPos.position;
        _vcam.transform.DOMove(_camTowardPos.position, _animationStopTime).SetEase(Ease.Linear);
        _animator.Play(_enterAnimationClip.name);
        _wall.SetActive(true);
    }

    public void ExitAnimation()
    {
        StartCoroutine(BossAnimationExit());
    }

    private IEnumerator BossAnimationExit()
    {

        yield return WaitForSeconds(0.2f);
        _bossEye.gameObject.SetActive(true);
        _bossEyeEffectTransform.DOScaleX(_bossEyeScaleX, _bossEyeDuration * 0.2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InCirc).SetDelay(0.1f);
        _bossEye.DOFade(0f, _bossEyeDuration);
        yield return WaitForSeconds(2f);
        ObjectManager.Instance.DisappearBarImage();
        DOTween.To(
            () => _vcam.m_Lens.OrthographicSize,
            value => _vcam.m_Lens.OrthographicSize = value,
            10f, 1f
        );
        _vcam.transform.DOMove(_camEndPos.position, 1f).OnComplete(() =>
        {
            GameManager.Instance.PlayerMove.IsMove();
            _bossDice.gameObject.SetActive(true);
            _bossDice.material.DOFade(1f, 1f);
            _carlosAttack.enabled = true;
        });
        _bossExplainText.DOFade(0f, 1f);
        _bossNameText.DOFade(0f, 1f);
    }

    public void ShowText()
    {
        _bossExplainText.DOFade(1f, _bossTextShowDuration);
        _bossExplainText.rectTransform.DOAnchorPosX(_bossNameTextAnchorPosX, _bossTextShowDuration).SetEase(Ease.OutQuart).OnComplete(()=>{
            StartCoroutine(ShowBossName());
        });
    }

    private IEnumerator ShowBossName()
    {
        _bossNameText.DOFade(1f,0f);
        for (int i = 1; i <= _bossNameText.textInfo.characterCount; i++)
        {
            _bossNameText.maxVisibleCharacters = i;
            _vcam.transform.DOShakePosition(_shakeCamDuration, _shakeCamStrength, _shakeCamVibrato, _shakeCamRandomness);
            yield return WaitForSeconds(_bossTextShowDuration * 0.25f);
        }
    }

    public void BossEyeDown()
    {
        _bossEye.transform.position -= Vector3.up * _bossEyeMovement;
    }

    public void BossEyeUp()
    {
        _bossEye.transform.position += Vector3.up * _bossEyeMovement;
    }

    public void EndAnimation()
    {
        _enemyAI.FSM.ChangeState(EnemyAI.States.Idle);
    }
}
