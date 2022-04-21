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

    private void OnEnable() {
        if(!_boxCol){
            _boxCol = GetComponent<BoxCollider2D>();
        }
        if(!_playerTransform) {
            _playerTransform = GameManager.Instance.Player.transform;
        }
        MoveToUp();
    }

    [ContextMenu("위로 올라가며 회전")]
    private void MoveToUp(){
        _diceMesh = transform.GetChild(0);
        Material material = _diceMesh.GetComponent<MeshRenderer>().material;

        transform.DOMove(new Vector2(_playerTransform.position.x , _gapPlayer + _playerTransform.position.y), _moveDuration).SetEase(ease : Ease.OutCirc);

        material.DOFade(1f, _moveDuration);

        Vector3 rotation = _diceMesh.transform.eulerAngles;

        rotation.y += 720f;

        _diceMesh.DOLocalRotate(rotation, _moveDuration, RotateMode.FastBeyond360).OnComplete(() =>{
            _boxCol.enabled = true;
            StartCoroutine(GroundCoroutine());
        });
        //TODO: Raycast로 자신의 위치에서 위아래로 하나 선을 그어 닿은 지점이 땅일경우 멈추도록 해야함
    }

    private IEnumerator GroundCoroutine(){
        while(!IsGround()){
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            yield return null;
        }
    }

    private bool IsGround(){
        Vector2 rayPos = new Vector2(transform.position.x, _boxCol.bounds.max.y);
        return Physics2D.Raycast(rayPos, Vector2.down, _diceMesh.transform.localScale.y , _bottomLayer);
    }

}
