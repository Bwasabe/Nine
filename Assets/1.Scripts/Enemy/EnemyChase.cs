using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyFOV))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    private EnemyFOV enemyFOV;

    protected EnemyAI enemyAI;


    private Rigidbody2D rb;


    [SerializeField]
    private float patrolTime = 2f;


    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float moveSmooth = 5f;
    [SerializeField]
    private float findPlayerDuration = 1f;

    private float timer;

    private Transform _playerTransform = null;


    public bool distancePlayer = false;
    public bool viewPlayer = false;
    public bool tracePlayer = false;


    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyFOV = GetComponent<EnemyFOV>();
        rb = GetComponent<Rigidbody2D>();
        _playerTransform = GameManager.Instance.Player.transform;
        AddFSM();
    }
    protected virtual void AddFSM()
    {
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, ReturnToPatrol);
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Chase, FindPlayerMotion);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, ChaseMove);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, CheckAttackPossible);
    }

    private void Update() {
        distancePlayer = enemyFOV.IsDistancePossible(enemyFOV.AttackRange);
        tracePlayer = enemyFOV.IsTracePlayer();
        viewPlayer = enemyFOV.IsViewPlayer();
    }



    private void ReturnToPatrol()
    {
        if (!enemyFOV.IsViewPlayer())
        {
            timer += Time.deltaTime;
            if (timer > patrolTime)
            {
                timer = 0f;
                enemyAI.FSM.ChangeState(EnemyAI.States.Patrol);
            }
        }
        else
        {
            timer = 0f;
        }
    }

    protected void ChaseMove()
    {
        if (_playerTransform.position.x-1f > transform.position.x)
        {
            speed = Mathf.Abs(speed);
        }
        else
        {
            speed = Mathf.Abs(speed) * -1f;
        }
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, speed, Time.deltaTime * moveSmooth), rb.velocity.y);
    }

    protected void CheckAttackPossible()
    {
        //Debug.Log(enemyFOV.IsDistancePossible(enemyFOV.AttackRange) + " / " + enemyFOV.IsTracePlayer() + " / " + enemyFOV.IsViewPlayer());
        if (enemyFOV.IsDistancePossible(enemyFOV.AttackRange) && enemyFOV.IsTracePlayer() && enemyFOV.IsViewPlayer())
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Attack);
        }
    }


    private IEnumerator FindPlayerMotionCoroutine()
    {
        float oldSpeed = speed;
        //TODO: 적이 플레이어를 발견한 모션
        speed = 0f;
        yield return Yields.WaitForSeconds(findPlayerDuration);
        speed = oldSpeed;
    }
    
    protected void FindPlayerMotion()
    {
        StartCoroutine(FindPlayerMotionCoroutine());
    }

}
