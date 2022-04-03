using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceChase : EnemyChase
{
    protected override void AddFSM()
    {
        enemyAI.AddFSMAction(FSMStates.Enter, EnemyAI.States.Chase, FindPlayerMotion);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, ChaseMove);
        enemyAI.AddFSMAction(FSMStates.Update, EnemyAI.States.Chase, CheckAttackPossible);
    }
}
