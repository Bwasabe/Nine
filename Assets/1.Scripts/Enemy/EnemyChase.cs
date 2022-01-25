using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFOV))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyMove))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    private EnemyFOV enemyFOV;

    private EnemyAI enemyAI;
    private EnemyMove enemyMove;

    private Rigidbody2D rb;


    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float moveSmooth = 5f;
    [SerializeField]
    private float findPlayerDuration = 1f;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyFOV = GetComponent<EnemyFOV>();
        rb = GetComponent<Rigidbody2D>();
        AddFSM();
    }
    private void AddFSM()
    {
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Patrol, IsCanChasePlayer);
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Chase, FindPlayerMotion);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, ChaseMove);
    }



    private void IsCanChasePlayer()
    {
        Debug.Log(enemyFOV.IsViewPlayer());
        Debug.Log(enemyFOV.IsTracePlayer());
        if (enemyFOV.IsViewPlayer() && enemyFOV.IsTracePlayer() && !enemyFOV.IsAttackPossible())
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Chase);
        }
    }

    private void ChaseMove()
    {
        if (GameManager.Instance.Player.transform.position.x > transform.position.x)
        {
            speed = Mathf.Abs(speed);
        }
        else
        {
            speed = Mathf.Abs(speed) * -1f;
        }
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, speed, Time.deltaTime *moveSmooth), rb.velocity.y);
    }

    private IEnumerator FindPlayerMotionCoroutine()
    {
        float oldSpeed = speed;
        //TODO: 적이 플레이어를 발견한 모션
        speed = 0f;
        yield return Yields.WaitForSeconds(findPlayerDuration);
        speed = oldSpeed;
    }
    private void FindPlayerMotion()
    {
        StartCoroutine(FindPlayerMotionCoroutine());
    }

}
