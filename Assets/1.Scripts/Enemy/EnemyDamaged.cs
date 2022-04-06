using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyMove))]
public class EnemyDamaged : CharacterDamaged
{
    private EnemyAI enemyAI;

    private EnemyAI.States states;

    private Rigidbody2D rb;


    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Invincible, StartInvincible);
    }
    public override void Damage(int damage)
    {
        if (enemyAI.FSM.State == EnemyAI.States.Invincible) return;
        states = enemyAI.FSM.State;
        base.Damage(damage);
        if (hp >= 1)
        {
            Debug.Log("'왜 안바뀌지'");
            enemyAI.FSM.ChangeState(EnemyAI.States.Invincible);
            CheckPlayer();
        }
        else{
            enemyAI.FSM.ChangeState(EnemyAI.States.Dead);
        }
    }

    private void CheckPlayer(){
        Transform pTransform = GameManager.Instance.Player.transform;
        float facingDir = 1f;
        if(transform.position.x > pTransform.position.x){
            facingDir = -1f;
        }
        else{
            facingDir = 1f;
        }
        rb.velocity = Vector2.right * 0.2f * facingDir;
    }

    private IEnumerator DamagedMotion()
    {
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.red;
            yield return Yields.WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return Yields.WaitForSeconds(0.1f);
        }
        enemyAI.FSM.ChangeState(states);
    }
    private void StartInvincible()
    {
        StartCoroutine(DamagedMotion());
    }
}
