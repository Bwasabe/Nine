using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFOV))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBungOff : MonoBehaviour
{
    private EnemyFOV enemyFOV;
    private EnemyAI enemyAI;

    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float moveSmooth = 5f;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyFOV = GetComponent<EnemyFOV>();
        enemyAI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
        AddFSM();
    }
    private void AddFSM()
    {
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.BungOff, BungOffMove);
    }
    

    private void BungOffMove()
    {
        if (GameManager.Instance.Player.transform.position.x > transform.position.x)
        {
            speed = Mathf.Abs(speed) * -1f;
        }
        else
        {
            speed = Mathf.Abs(speed);

        }
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, speed, Time.deltaTime * moveSmooth), rb.velocity.y);
        Debug.Log("도망 : " + enemyFOV.IsDistancePossible(enemyFOV.AttackRange * 0.7f));
        CheckGoBackAttack();
    }
    private void CheckGoBackAttack()
    {
        if (enemyFOV.IsDistancePossible(enemyFOV.AttackRange * 0.7f) && !enemyFOV.IsDistancePossible(enemyFOV.BungOffRange) )
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Attack);
        }
    }
}
