using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyBase : MonoBehaviour
{
    public enum Facing
    {
        Left,
        Right,
    }

    public enum Run{
        Walk,
        Run,
    }

    protected Facing facing;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected Animator animator;

    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update(){
        ChangeFacing();
    }

    private void ChangeFacing(){
        if(rb.velocity.x > 0.1f){
            facing = Facing.Right;
        }
        else if(rb.velocity.x <-0.1f){
            facing = Facing.Left;
        }
        float scaleX = facing == Facing.Right ? 1f : -1f;
        transform.localScale = new Vector3(transform.localScale.x*scaleX , transform.localScale.y , transform.localScale.z);
    }
}
