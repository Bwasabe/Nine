using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardStart : MonoBehaviour
{
    private Tweener _tweener;

    private Transform _thisTransform = null;

    [SerializeField]
    private Transform _spawnMaxTransform = null;
    [SerializeField]
    private Transform _spawnMinTransform = null;
    

    [SerializeField]
    private Transform _parentTransform = null;

    [SerializeField]
    private float _maxArriveTime = 3f;

    private void OnEnable()
    {
        if (!_thisTransform)
        {
            _thisTransform = transform;
        }
        _thisTransform.position = new Vector3(_spawnMaxTransform.position.x, Random.Range(_spawnMinTransform.position.y, _spawnMaxTransform.position.y), _spawnMaxTransform.position.z);
        _tweener = _thisTransform.DORotate(Vector3.forward * 360f, 1f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        _thisTransform.DOMoveX(-15f, _maxArriveTime).OnComplete(() =>
        {
            gameObject.SetActive(false);
            _thisTransform.SetParent(_parentTransform);
        });
    }
    private void OnDisable()
    {
        _tweener.Kill();
    }
}
