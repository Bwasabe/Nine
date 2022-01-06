using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D) ,typeof(Collider2D) , typeof(SpriteRenderer) )]

public class EnemyBase : MonoBehaviour
{
    protected Collider2D col;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start(){
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
