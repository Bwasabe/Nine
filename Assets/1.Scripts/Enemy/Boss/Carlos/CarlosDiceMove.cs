using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlosDiceMove : CharacterAttack
{
    [SerializeField]
    private int _warringCount = 0;
    [SerializeField]
    private float _blinkDelay = 0.1f;
    [SerializeField]
    private float _firstBlinkDelay = 1f;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _destroyDelay = 15f;
    [SerializeField]
    private GameObject _blinkObj = null;
    [SerializeField]
    private Vector2 _moveDir = Vector2.up;


    private bool isActive = false;

    private float _currentTime = 0f;

    private void OnEnable()
    {
        Destroy(gameObject, 10f);
        _currentTime = Time.time;
        StartCoroutine(Init());
    }
    private IEnumerator Init()
    {
        yield return WaitForSeconds(_firstBlinkDelay);
        for (int i = 0; i < _warringCount * 2; i++)
        {
            _blinkObj.SetActive(isActive);
            yield return WaitForSeconds(_blinkDelay * 0.5f);
            isActive = !isActive;
        }
        _blinkObj.SetActive(false);
        StartCoroutine(TenDelayDestroy());
    }

    private IEnumerator TenDelayDestroy()
    {
        Debug.Log("다이스함수가 실행됨");
        while (Time.time < _currentTime + 10f)
        {
            Debug.Log("다이스 움직이는 중");
            transform.Translate(_moveDir * _speed * Time.deltaTime);
            yield return null;
        }
    }

}
