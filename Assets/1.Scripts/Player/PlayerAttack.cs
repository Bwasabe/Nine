

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : CharacterAttack
{

    [SerializeField]
    private GameObject attackCol;
    [SerializeField]
    private GameObject upAttackCol;
    [SerializeField]
    private float _shakeCamDuration = 0.1f;
    [SerializeField]
    private float _shakeCamStrength = 0.2f;
    [SerializeField]
    private int _shakeCamVibrato = 2;
    [SerializeField]
    private float _shakeCamRandomness = 45f;

    public void Attack(int cardCount){
        if(cardCount >= 6){
            upAttackCol.SetActive(true);
        }
        else{
            attackCol.SetActive(true);
        }
    }
    public void OffCol(){
        attackCol.SetActive(false);
        upAttackCol.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if ((1 << other.gameObject.layer & targetLayer) > 0)
        {
            ObjectManager.Instance.VirtualCamera.transform.DOShakePosition(_shakeCamDuration, _shakeCamStrength, _shakeCamVibrato, _shakeCamRandomness);
        }
    }
}
