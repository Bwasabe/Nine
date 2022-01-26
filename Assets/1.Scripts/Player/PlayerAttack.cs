using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : CharacterAttack
{

    [SerializeField]
    private GameObject attackCol;
    [SerializeField]
    private GameObject upAttackCol;

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
}
