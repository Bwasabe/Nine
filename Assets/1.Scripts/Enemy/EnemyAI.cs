using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MonsterLove.StateMachine;

public enum FSMStates
{
    Awake,
    OnEnable,
    Start,

    Enter,
    Update,
    Exit,
    OnTriggerEnter2D,

}
public class EnemyAI : MonoBehaviour
{
    public enum States
    {
        Undisturbed,
        Idle,
        Patrol,
        Chase,
        Attack,
        Skill,
        Invincible,
        Dead,
    }
    public StateMachine<States, StateDriverUnity> FSM { get; private set; }


    private Dictionary<FSMStates, Dictionary<States, Action>> fsmStateDictionary = new Dictionary<FSMStates, Dictionary<States, Action>>();



    private void Awake()
    {
        FSM = new StateMachine<States, StateDriverUnity>(this);
        fsmStateDictionary[FSMStates.Awake][States.Undisturbed]?.Invoke();
    }
    private void Update()
    {
        FSM.Driver.Update.Invoke();
        fsmStateDictionary[FSMStates.Update][States.Undisturbed]?.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        FSM.Driver.OnTriggerEnter2D.Invoke(other);
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Undisturbed]?.Invoke();
    }




    public void AddFSMAction(FSMStates eventState, States state, Action action)
    {
        fsmStateDictionary[eventState][state] += action;
    }




    #region Idle

    void Idle_Enter()
    {
        fsmStateDictionary[FSMStates.Enter][States.Idle]?.Invoke();
    }
    void Idle_Update()
    {
        fsmStateDictionary[FSMStates.Update][States.Idle]?.Invoke();
    }
    void Idle_Exit()
    {
        fsmStateDictionary[FSMStates.Exit][States.Idle]?.Invoke();
    }
    void Idle_OnTriggerEnter2D(Collider2D other)
    {
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Idle]?.Invoke();
    }

    #endregion

    #region patrol

    void Patrol_Enter()
    {
        fsmStateDictionary[FSMStates.Enter][States.Patrol]?.Invoke();
    }
    void Patrol_Update()
    {
        fsmStateDictionary[FSMStates.Update][States.Patrol]?.Invoke();
    }
    void Patrol_Exit()
    {
        fsmStateDictionary[FSMStates.Exit][States.Patrol]?.Invoke();
    }
    void Patrol_OnTriggerEnter2D(Collider2D other)
    {
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Patrol]?.Invoke();
    }

    #endregion


    #region Chase

    void Chase_Enter()
    {
        fsmStateDictionary[FSMStates.Enter][States.Chase]?.Invoke();
    }
    void Chase_Update()
    {
        fsmStateDictionary[FSMStates.Update][States.Chase]?.Invoke();
    }
    void Chase_Exit()
    {
        fsmStateDictionary[FSMStates.Exit][States.Chase]?.Invoke();
    }
    void Chase_OnTriggerEnter2D(Collider2D other)
    {
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Chase]?.Invoke();
    }

    #endregion


    #region Attack

    void Attack_Enter()
    {
        fsmStateDictionary[FSMStates.Enter][States.Attack]?.Invoke();
    }
    void Attack_Update()
    {
        fsmStateDictionary[FSMStates.Update][States.Attack]?.Invoke();
    }
    void Attack_Exit()
    {
        fsmStateDictionary[FSMStates.Exit][States.Attack]?.Invoke();
    }
    void Attack_OnTriggerEnter2D(Collider2D other)
    {
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Attack]?.Invoke();
    }

    #endregion


    #region Skill

    void Skill_Enter()
    {
        fsmStateDictionary[FSMStates.Enter][States.Skill]?.Invoke();
    }
    void Skill_Update()
    {
        fsmStateDictionary[FSMStates.Update][States.Skill]?.Invoke();
    }
    void Skill_Exit()
    {
        fsmStateDictionary[FSMStates.Exit][States.Skill]?.Invoke();
    }
    void Skill_OnTriggerEnter2D(Collider2D other)
    {
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Skill]?.Invoke();
    }

    #endregion


    #region Invincible

    void Invincible_Enter()
    {
        fsmStateDictionary[FSMStates.Enter][States.Invincible]?.Invoke();
    }
    void Invincible_Update()
    {
        fsmStateDictionary[FSMStates.Update][States.Invincible]?.Invoke();
    }
    void Invincible_Exit()
    {
        fsmStateDictionary[FSMStates.Exit][States.Invincible]?.Invoke();
    }
    void Invincible_OnTriggerEnter2D(Collider2D other)
    {
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Invincible]?.Invoke();
    }

    #endregion


    #region Dead

    void Dead_Enter()
    {
        fsmStateDictionary[FSMStates.Enter][States.Dead]?.Invoke();
    }
    void Dead_Update()
    {
        fsmStateDictionary[FSMStates.Update][States.Dead]?.Invoke();
    }
    void Dead_Exit()
    {
        fsmStateDictionary[FSMStates.Exit][States.Dead]?.Invoke();
    }
    void Dead_OnTriggerEnter2D(Collider2D other)
    {
        fsmStateDictionary[FSMStates.OnTriggerEnter2D][States.Dead]?.Invoke();
    }

    #endregion

}
