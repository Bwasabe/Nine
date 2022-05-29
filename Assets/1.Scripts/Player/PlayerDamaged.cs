using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : CharacterDamaged
{
    private bool isDamaged;
    public override void Damage(int damage){
        isDamaged = true;
        
        base.Damage(damage);
        if(hp>= 1){
            StartCoroutine(DamageMotion());
        }
    }
    private IEnumerator DamageMotion(){
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.enabled = false;
            yield return Yields.WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return Yields.WaitForSeconds(0.1f);
        }
        isDamaged = false;
    }

    protected override void Dead()
    {
        base.Dead();
        Debug.Break();
    }
}
