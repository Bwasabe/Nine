using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamaged : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int hp;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    private string damageAnimationName;
    [SerializeField]
    private string deadAnimationName;

    [SerializeField]
    private Animator animator;
   

    public virtual void Damage(int damage)
    {
        Debug.Log("베이스");
        if (hp < 1)
        {
            Dead();
            animator.Play(deadAnimationName);
        }
        else
        {
            hp-= damage;
            animator.Play(damageAnimationName);
        }
    }

    public virtual void Dead()
    {
        Debug.Log("죽음");
        Debug.Break();
    }

    
}
