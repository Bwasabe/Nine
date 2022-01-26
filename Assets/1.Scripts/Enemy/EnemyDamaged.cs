using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyDamaged : CharacterDamaged
{
    private EnemyAI enemyAI;

    private EnemyAI.States states;


    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Invincible, StartInvincible);
    }
    public override void Damage(int damage)
    {
        Debug.Log("적 맞음");
        if (enemyAI.FSM.State == EnemyAI.States.Invincible) return;
        states = enemyAI.FSM.State;
        base.Damage(damage);
        if (hp >= 1)
        {
            enemyAI.FSM.ChangeState(EnemyAI.States.Invincible);
        }
        else{
            enemyAI.FSM.ChangeState(EnemyAI.States.Dead);
        }
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
