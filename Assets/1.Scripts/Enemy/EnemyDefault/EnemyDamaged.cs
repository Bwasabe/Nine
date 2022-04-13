using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(EnemyAI))]
public class EnemyDamaged : CharacterDamaged
{
    private EnemyAI enemyAI;

    private EnemyAI.States states;

    private Rigidbody2D rb;

    private Coroutine _hitCoroutine = null;


    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
        //enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Invincible, StartInvincible);
    }
    public override void Damage(int damage)
    {
        if (enemyAI.FSM.State == EnemyAI.States.Dead) return;
        states = enemyAI.FSM.State;

        if (_hitCoroutine != null)
        {
            StopCoroutine(_hitCoroutine);
        }
        _hitCoroutine = StartCoroutine(DamagedMotion());

        base.Damage(damage);

        if (hp >= 1)
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Invincible);
            enemyAI.FSM.ChangeState(states);
            if (enemyAI.FSM.State == EnemyAI.States.Patrol)
            {
                CheckPlayer();
            }
        }
        if (hp <= 0)
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Dead);
        }
    }

    public void CheckPlayer()
    {
        Transform pTransform = GameManager.Instance.Player.transform;
        float facingDir = 1f;
        if (transform.position.x > pTransform.position.x)
        {
            facingDir = -1f;
        }
        else
        {
            facingDir = 1f;
        }
        rb.velocity = Vector2.right * 0.2f * facingDir;
    }

    private IEnumerator DamagedMotion()
    {
        spriteRenderer.color = Color.red;
        yield return WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return WaitForSeconds(0.1f);
    }

    public override void Dead()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        rb.gravityScale = 0f;
        enemyAI.enabled = false;
        spriteRenderer.DOFade(0f, 2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

}
