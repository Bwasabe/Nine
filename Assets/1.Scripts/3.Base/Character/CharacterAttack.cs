using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    protected LayerMask targetLayer;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        if ((1 << other.gameObject.layer & targetLayer) > 0)
        {
            IDamageable target =  other.gameObject.GetComponent<IDamageable>();
            if(target != null){
                target.Damage(damage);
            }
        }
    }
}
