using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using static Define;
using static Yields;

public class Scene2 : MonoBehaviour, IChangeable
{
    [SerializeField]
    private float _cameraPos = 38f;
    [SerializeField]
    private Transform _jokerCard = null;

    public void SceneChange()
    {
        MaincamTransform.DOMoveX(_cameraPos, 5f);
        StartCoroutine(MoveJoker());
    }

    private IEnumerator MoveJoker()
    {
        yield return WaitForSeconds(1.0f);
        _jokerCard.DOMoveX(9f, 3.5f).OnComplete(() => _jokerCard.gameObject.SetActive(false));
        _jokerCard.DORotate(Vector3.forward * 360f, 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
