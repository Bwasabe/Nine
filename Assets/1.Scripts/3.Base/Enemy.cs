using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 0)]
public class Enemy : ScriptableObject {
    public new string name;

    public int hp;
    public int atk;

    public float walkSpeed;
    public float runSpeed;
    public float attackDelay;
    public float attackRange;
    public float viewRange;

}
