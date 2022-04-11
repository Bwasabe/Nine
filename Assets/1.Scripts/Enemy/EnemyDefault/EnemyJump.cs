using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyJump : MonoBehaviour
{
    private EnemyAI enemyAI;
    private EnemyMove enemyMove;

    private Rigidbody2D rb;

    [SerializeField]
    private float jumpForce;


    private void Start() {
        Initialize();
    }
    private void Initialize(){
        enemyAI = GetComponent<EnemyAI>();
        enemyMove = GetComponent<EnemyMove>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void AddFSM(){
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, Jump);
    }

    private void Jump(){
        if(enemyMove.IsPlatformExist() == false){ //TODO: 이 코드는 현재 내려가는 아무런 조건없이 없으면 점프이기 때문에 조건을 고쳐야 함
            rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        }
    }

}
