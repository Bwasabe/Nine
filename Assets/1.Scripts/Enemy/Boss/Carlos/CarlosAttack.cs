using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarlosAttack : MonoBehaviour
{
    [SerializeField]
    private Transform _rollingDice = null;

    [SerializeField]
    private float _rollingDuration = 0.5f;

    [ContextMenu("스핀")]
    private void Spin() {
        _rollingDice.DOLocalRotate(new Vector3(_rollingDice.transform.rotation.x, _rollingDice.transform.rotation.y - 3600f, _rollingDice.transform.rotation.z)
                    , _rollingDuration, RotateMode.FastBeyond360);
    }
}
