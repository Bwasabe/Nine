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
        //EventManager.StartListening("dhk", A);
    }
    public virtual void Damage(int damage){
        if(hp < 1){
            Dead();
        }
        else{

        }
    }
    private int A(){
        return 1;
    }
    protected virtual void Dead(){
        //TODO: 대충 StopListening
    }
}
