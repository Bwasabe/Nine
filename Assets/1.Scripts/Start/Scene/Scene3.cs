using static Define;
using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Reflection;
using UnityEngine.Experimental.Rendering.Universal;

public class Scene3 : MonoBehaviour, IChangeable
{

    [SerializeField]
    private GameObject _switchSound = null;

    [SerializeField]
    private GameObject _startCardPrefab = null;
    [SerializeField]
    private Transform _cardroot = null;

    [SerializeField]
    private float _maxSpawnTime = 5f;

    private Transform _thisTransform = null;

    [SerializeField]
    private GameObject _offNeons = null;
    [SerializeField]
    private GameObject _onNeons = null;
    [SerializeField]
    private GameObject _casinoLight = null;
    [SerializeField]
    private Light2D _globalLight = null;
    [SerializeField]
    private GameObject _lights = null;
    [SerializeField]
    private TouchToStart _touchToStart;
    // [SerializeField]
    // private GameObject _touchToStart = null;

    private Coroutine _coroutine = null;

    private CameraStart _cameraStart;

    private void Awake() {
        EventManager.StartListening("Scene3", Scene3ToEndScene);
    }
    private void Start()
    {
        _thisTransform = transform;
        _cameraStart = MaincamTransform.GetComponent<CameraStart>();
    }


    private void Scene3ToEndScene()
    {
        StopCoroutine(_coroutine);
        Onlight();
        ManagerStart.Instance.SetCurrentScene(MethodBase.GetCurrentMethod().DeclaringType.FullName, 4);
    }

    public void SceneChange()
    {
        _coroutine = StartCoroutine(SceneChage());
    }

    private void Onlight()
    {
        _switchSound.SetActive(true);
        _offNeons.SetActive(false);
        _onNeons.SetActive(true);
        ManagerStart.Instance.FadeObject.color = Vector4.zero;
        //_touchToStart.SetActive(true);
        StartCoroutine(SceneLoad());
    }

    private IEnumerator SceneLoad()
    {
        yield return WaitForSeconds(0.5f);
        Tweeners();

    }
    private void Tweeners()
    {
        //MaincamTransform.DOMoveY(0f, 1.5f);
        //MaincamTransform.DORotate(Vector3.right * -120f, 1.5f).OnComplete(() =>
        ManagerStart.Instance.FadeObject.DOColor(new Vector4(1f, 1f, 1f, 0.9f), 3f).SetEase(Ease.InExpo).OnComplete(() => ManagerStart.Instance.FadeObject.color = Color.white);    
        DOTween.To(() => _globalLight.intensity, (value) => _globalLight.intensity = value, 35f, 3f).SetEase(Ease.InExpo).OnComplete(() =>
        {
            //MaincamTransform.rotation = Quaternion.Euler(-60f, 0f, 0f);
            //ManagerStart.Instance.FadeObject.gameObject.SetActive(false);
            ManagerStart.Instance.FadeObject.rectTransform.localPosition = new Vector3(0f, 0f, 300f);
            _lights.SetActive(false);
            _cameraStart.enabled = true;
            _globalLight.gameObject.SetActive(false);
            _casinoLight.SetActive(true);
            ManagerStart.Instance.SetCurrentScene(MethodBase.GetCurrentMethod().DeclaringType.FullName, 4);
            MaincamTransform.position = new Vector3(MaincamTransform.position.x, 1.5f, MaincamTransform.position.z);
            MaincamTransform.rotation = Quaternion.Euler(Vector3.right * -120f);
        });
    }

    private IEnumerator SceneChage()
    {
        ManagerStart.Instance.FirstFade();
        MaincamTransform.DOMove(new Vector3(_thisTransform.position.x, _thisTransform.position.y, -10f), 0.5f);
        yield return WaitForSeconds(1f);
        Onlight();
    }

    // private IEnumerator ThrowCards()
    // {
    //     while (true)
    //     {
    //         yield return WaitForSeconds(Random.Range(0.3f, _maxSpawnTime));
    //         SpawnCard();
    //     }
    // }

    // private void SpawnCard()
    // {
    //     if (_cardroot.childCount > 1)
    //     {
    //         Transform child = _cardroot.GetChild(1);
    //         child.gameObject.SetActive(true);
    //         child.SetParent(_thisTransform);
    //     }
    //     else
    //     {
    //         GameObject card = Instantiate(_startCardPrefab);
    //         card.SetActive(true);
    //     }
    // }
}
