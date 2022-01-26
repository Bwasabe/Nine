using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterMove : MonoBehaviour
{
    public bool isFacingRight{ get; protected set; }

    protected Rigidbody2D rb;
    protected Collider2D col;


    

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    protected virtual void Update() {
        ChangeFacing();
    }

    private void ChangeFacing(){
        if(rb.velocity.x > 0.1f){
            isFacingRight = true;
        }
        else if(rb.velocity.x <-0.1f){
            isFacingRight = false;
        }
        float scaleX = isFacingRight ? 1f : -1f;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*scaleX , transform.localScale.y , transform.localScale.z);
    }
    protected bool IsGrounded(){
        return Physics2D.OverlapBox(col.bounds.center, new Vector2(col.bounds.size.x - 0.1f, col.bounds.size.y), 180f);
    }
}
