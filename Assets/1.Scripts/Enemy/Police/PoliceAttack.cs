using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PoliceAttack : EnemyAttack
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform shootPos;
    [SerializeField]
    private float scaleX = 0.5f;

    private Rigidbody2D rb;

    public Sequence bulletScale { get; private set; }


    private void Start()
    {
        Initialize();
    }
    protected override void AddFSM()
    {
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Attack, AttackEnter);
        enemyAI.AddFSMAction(FSMStates.FixedUpdate, EnemyAI.States.Attack, ChangeFacing);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Attack, Attacking);
    }
    protected override void Initialize()
    {
        base.Initialize();
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void AttackEnter()
    {
        rb.velocity = Vector2.up * rb.velocity.y;
    }

    protected override void Attack()
    {
        //TODO: 풀링 소환
        Vector2 dir = GameManager.Instance.Player.transform.position - transform.position;
        GameObject g = Instantiate(bullet, shootPos.position, Quaternion.identity);
        g.SetActive(true);
        g.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(
            Mathf.Atan2(dir.y, dir.x), -enemyFOV.ViewAngle * Mathf.Rad2Deg + 180f, enemyFOV.ViewAngle * Mathf.Rad2Deg + 180f));//*(enemyMove.isFacingRight ? 1f : -1f ));
        bulletScale = DOTween.Sequence();
        bulletScale.Append(g.transform.DOScaleX(scaleX, 0.3f));
        Debug.Log(g.transform.eulerAngles);

    }
    public void KillScale()
    {
        bulletScale.Kill();
    }
}
