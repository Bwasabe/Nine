using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D))]
public class CarlosGroundPoundDice : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30f;
    [SerializeField]
    private float _gapPlayer = 1f;
    [SerializeField]
    private float _moveDuration = 0.8f;
    [SerializeField]
    private LayerMask _bottomLayer;


    private BoxCollider2D _boxCol = null;
    private Transform _playerTransform = null;

    private Transform _diceMesh = null;

    private SpriteRenderer _spriteRenderer = null;

    private Material _material = null;


    private void OnEnable()
    {
        transform.position = GameManager.Instance.Player.transform.position;
        _boxCol ??= GetComponent<BoxCollider2D>();
        _playerTransform ??= GameManager.Instance.Player.transform;
        _diceMesh ??= transform.GetChild(0);
        _material ??= _diceMesh.GetComponent<MeshRenderer>().material;
        MoveToUp();
    }

    [ContextMenu("위로 올라가며 회전")]
    private void MoveToUp()
    {

        Vector3 playerPos = _playerTransform.position;
        playerPos.y += _gapPlayer;

        transform.DOMove(playerPos, _moveDuration).SetEase(ease: Ease.OutCirc);

        _material.DOFade(1f, _moveDuration);

        Vector3 rotation = _diceMesh.transform.eulerAngles;

        rotation.y += 720f;

        _diceMesh.DOLocalRotate(rotation, _moveDuration, RotateMode.FastBeyond360).OnComplete(() =>
        {
            _boxCol.enabled = true;
            StartCoroutine(GroundCoroutine());
            Debug.Log("ㅁㄴㅇㄹㄴㅁ");
        });
        //TODO: Raycast로 자신의 위치에서 위아래로 하나 선을 그어 닿은 지점이 땅일경우 멈추도록 해야함
    }

    private IEnumerator GroundCoroutine()
    {
        while (!IsGround())
        {
            transform.Translate(transform.up * -1 * _speed * Time.deltaTime);
            yield return null;
        }
        _boxCol.enabled = false;
        //TODO: 풀링
        _material.DOFade(0f, _moveDuration).OnComplete(() => Destroy(gameObject));
    }

    private bool IsGround()
    {
        Vector2 rayPos = new Vector2(transform.position.x, _boxCol.bounds.max.y);
        return Physics2D.Raycast(rayPos, Vector2.down, _diceMesh.transform.localScale.y, _bottomLayer);
    }

}
