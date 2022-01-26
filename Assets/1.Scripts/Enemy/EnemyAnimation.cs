using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]

public class EnemyAnimation : MonoBehaviour
{
    private EnemyAI enemyAI;

    private Animator animator;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        if(enemyAI.FSM.State == EnemyAI.States.Invincible || enemyAI.FSM.State == EnemyAI.States.Dead)return;
        animator.SetInteger("States", (int)enemyAI.FSM.State);
    }

    [ContextMenu("애니메이션 강제설정")]
    private void SetAnimation()
    {
        animator.SetInteger("States", (int)enemyAI.FSM.State);
    }

}
