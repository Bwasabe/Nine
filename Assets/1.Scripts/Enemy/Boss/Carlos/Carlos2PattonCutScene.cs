using static Yields;
using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using SoftMasking;

public class Carlos2PattonCutScene : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enableObjects = new List<GameObject>();

    [SerializeField]
    private List<GameObject> _objects = new List<GameObject>();


    [SerializeField]
    private Camera _effectCam = null;

    [Space]

    [SerializeField]
    private Volume _volume;

    [Space]

    [SerializeField]
    private Sprite _circleSprite = null;
    [SerializeField]
    private SoftMask _softMask;
    [SerializeField]
    private SoftMask _secSoftMask;

    [SerializeField]
    private MeshRenderer _dice;

    [SerializeField]
    private Material _darkDiceMaterial;

    [Space]
    [SerializeField]
    private float _darkDuration = 5f;
    [SerializeField]
    private Vector2 _darkSizeDelta = new Vector2(1550f, 1080f);
    [SerializeField]
    private Vector2[] _lensSizeDelta;

    private LensDistortion _lensDistortion;

    private RectTransform _maskRect = null;

    private RectTransform _secMaskRect = null;


    private Sequence _page2CutScene;


    private Animator _animator;
    private int Page2EnterAnimation = Animator.StringToHash("Carlos_Page2Animation");


    private CarlosAttack _carlosAttack = null;

    private void OnEnable() {
        if(!_carlosAttack)_carlosAttack = transform.parent.GetComponent<CarlosAttack>();
        
    }

    private void Start()
    {
        _animator = transform.parent.GetComponent<Animator>();
        _page2CutScene = DOTween.Sequence();
        _volume.sharedProfile.TryGet<LensDistortion>(out _lensDistortion);
        _maskRect = _softMask.GetComponent<RectTransform>();
        _secMaskRect = _secSoftMask.GetComponent<RectTransform>();

        //SetSequence();

    }

    public void EventDark()
    {
        _dice.transform.rotation = Quaternion.identity;
        Vector3 angle = new Vector3(0f, 7200f, 360f);
        _dice.transform.DORotate(angle, 5f, RotateMode.FastBeyond360).SetEase(Ease.InQuad).OnComplete(() => _dice.material = _darkDiceMaterial);
        _maskRect.DOSizeDelta(_darkSizeDelta, _darkDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                
                _maskRect.sizeDelta = new Vector2(MainCam.pixelWidth, MainCam.pixelHeight);
                _maskRect.gameObject.SetActive(false);
                _secMaskRect.gameObject.SetActive(true);
            }
        );

    }

    private void SetSequence()
    {
        _page2CutScene.Append(
            _maskRect.DOSizeDelta(_darkSizeDelta, _darkDuration).SetEase(Ease.Linear).OnComplete(() =>
            {

                _maskRect.sizeDelta = new Vector2(MainCam.pixelWidth, MainCam.pixelHeight);
                _maskRect.gameObject.SetActive(false);
                _secMaskRect.gameObject.SetActive(true);
            }
            ));

        _page2CutScene.Append(
            DOTween.To(
            () => _lensDistortion.intensity.value,
            x => _lensDistortion.intensity.value = x,
            0.3f, 0.75f
        ).SetEase(Ease.InSine)
        );
        _page2CutScene.Join(
            _secMaskRect.DOSizeDelta(_lensSizeDelta[0], 0.75f).SetEase(Ease.Linear)
        );

        _page2CutScene.AppendInterval(0.3f);


        _page2CutScene.Append(
            DOTween.To(
            () => _lensDistortion.intensity.value,
            x => _lensDistortion.intensity.value = x,
            0.6f, 0.75f
        ).SetEase(Ease.InSine)
        );

        _page2CutScene.Join(
            _secMaskRect.DOSizeDelta(_lensSizeDelta[1], 0.75f).SetEase(Ease.Linear)
        );

        _page2CutScene.AppendInterval(0.3f);

        _page2CutScene.AppendCallback(() =>
        {
            _secSoftMask.invertMask = true;
            _secSoftMask.invertOutsides = true;
            _secMaskRect.sizeDelta = Vector2.zero;
        });

        _page2CutScene.Append(
            DOTween.To(
            () => _lensDistortion.intensity.value,
            x => _lensDistortion.intensity.value = x,
            -0.5f, 0.3f
            ).SetEase(Ease.OutBack)
        );

        _page2CutScene.Join(
            _secMaskRect.DOSizeDelta(new Vector2(MainCam.pixelWidth, MainCam.pixelWidth), 0.5f).SetEase(Ease.Linear)
        );

        _page2CutScene.Append(
            DOTween.To(
            () => _lensDistortion.intensity.value,
            x => _lensDistortion.intensity.value = x,
            0f, 0.2f
            ).SetEase(Ease.Linear)
        );

        _page2CutScene.AppendCallback(() =>
            _objects.ForEach(x => x.SetActive(false))
        );
    }

    public void Page2()
    {
        _enableObjects.ForEach(x => x.SetActive(true));

        Debug.Log(_carlosAttack);
        _carlosAttack.StopAllCoroutines();
        _carlosAttack.enabled = false;

        ObjectManager.Instance.FadeObject.DOFade(1f, 2f).OnComplete(() =>
        {
            ObjectManager.Instance.FadeObject.DOFade(0f, 0f);
            ObjectManager.Instance.VirtualCamera.m_Lens.OrthographicSize = 5f;
            ObjectManager.Instance.VirtualCamera.transform.position = new Vector3(-20f, 0f, -10f);

            _effectCam.orthographicSize = 5f;


            _animator.Play(Page2EnterAnimation);
            EventDark();

        });

    }



}
