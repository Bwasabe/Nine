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
    private Vector2 _moveDir = Vector2.right;


    private bool isActive = false;

    private float _currentTime = 0f;

    private void OnEnable()
    {
        Destroy(gameObject, _destroyDelay);
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
        while (Time.time < _currentTime + 10f)
        {
            transform.Translate(_moveDir * _speed * Time.deltaTime);
            yield return null;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
