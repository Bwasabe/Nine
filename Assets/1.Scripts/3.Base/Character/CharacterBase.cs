using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterBase : MonoBehaviour
{
    public enum Facing
    {
        Left,
        Right,
    }

    public Facing facing { get; protected set; }
    public SpriteRenderer spriteRenderer { get; protected set; }
    public Rigidbody2D rb { get; protected set; }
    public Collider2D col { get; protected set; }
    public Animator animator { get; protected set; }


    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate(){
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
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*scaleX , transform.localScale.y , transform.localScale.z);
    }

}
