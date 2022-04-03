using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using UnityEngine;

using static Define;
using static Yields;
public class Scene2 : MonoBehaviour, IChangeable
{
    [SerializeField]
    private GameObject _startPlayer = null;

    [SerializeField]
    private Transform _fastCameraPos;
    [SerializeField]
    private Transform _cameraPos;

    [SerializeField]
    private AnimationClip[] _animationClips = null;

    private float _animationLength;

    private Coroutine _cameraCoroutine = null;

    private Tweener _fastCameraTweener = null;
    private Tweener _cameraTweener = null;

    private void Awake() {
        EventManager.StartListening("Scene2", Scene2ToScene3);
    }
    private void Start()
    {
        for (int i = 0; i < _animationClips.Length; i++)
        {
            _animationLength += _animationClips[i].length;
        }
    }
    public void SceneChange()
    {
        _cameraCoroutine = StartCoroutine(CameraMove());
    }

    private void Scene2ToScene3()
    {
        ManagerStart.Instance.SetCurrentScene(MethodBase.GetCurrentMethod().DeclaringType.FullName, 3);
        StopCoroutine(_cameraCoroutine);
        if (_cameraTweener != null)
        {
            _cameraTweener.Kill();
        }
        if (_fastCameraTweener != null)
        {
            _fastCameraTweener.Kill();
        }
        _startPlayer.SetActive(false);
        FlickerDirect.Instance.SceneChange(3);
    }
    private IEnumerator CameraMove()
    {
        _startPlayer.SetActive(true);
        _fastCameraTweener = MaincamTransform.DOMove(_fastCameraPos.position, 0.5f);
        yield return WaitForSeconds(0.5f);
        _cameraTweener = MaincamTransform.DOMove(_cameraPos.position, _animationLength).OnComplete(() =>
        {
            FlickerDirect.Instance.SceneChange(3);
            ManagerStart.Instance.SetCurrentScene(MethodBase.GetCurrentMethod().DeclaringType.FullName, 3);

            _startPlayer.SetActive(false);
        });
    }
}
