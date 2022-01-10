using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : EnemyBase , IDamageable
{
    [SerializeField]
    private int hp;

    protected override void Start()
    {
        base.Start();
    }

    public virtual void Damage(int damage){
        if(hp < 1){
            Dead();
        }
        else{

        }
    }

    protected virtual void Dead(){
        
    }
}
