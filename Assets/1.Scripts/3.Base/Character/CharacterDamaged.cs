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
    private AnimationClip damageAnimation;
    [SerializeField]
    private AnimationClip deadAnimation;



    private bool isDead;


    public virtual void Damage(int damage)
    {
        if (isDead) return;
        if (hp >= 1)
        {
            hp -= damage;
            if (damageAnimation != null)
            {
                animator.Play(damageAnimation.name);
            }
        }
        if (hp <= 0)
        {
            Dead();
            isDead = true;
            if (deadAnimation != null)
            {
                animator.Play(deadAnimation.name);
            }
        }
    }

    public virtual void Dead()
    {
        Debug.Log("죽음");
        Debug.Log("ㅁㄴ읾ㄶㅇ");

    }


}
