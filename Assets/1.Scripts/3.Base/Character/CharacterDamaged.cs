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
    private Animator animator;

    [SerializeField]
    private string damageAnimationName;
    [SerializeField]
    private string deadAnimationName;



    private bool isDead;


    public virtual void Damage(int damage)
    {
        if (isDead) return;
        if (hp >= 1)
        {
            hp -= damage;
            animator.Play(damageAnimationName);
        }
        else
        {
            Dead();
            isDead = true;
            animator.Play(deadAnimationName);

        }
    }

    public virtual void Dead()
    {
        Debug.Log("죽음");

    }


}
