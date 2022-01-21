// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D) ,typeof(Collider2D) , typeof(SpriteRenderer) )]
// [RequireComponent(typeof(Animator))]
// public class CharacterBase : MonoBehaviour
// {
//     private enum Facing
//     {
//         Left,
//         Right,
//     }

//     protected Facing facing;
//     protected SpriteRenderer spriteRenderer;
//     protected Rigidbody2D rb;
//     protected Collider2D col;
//     protected Animator animator;
//     [SerializeField]
//     private Character character;

//     public Character Character{
//         get{
//             return character;
//         }
//     }


//     protected virtual void Start() {
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         rb = GetComponent<Rigidbody2D>();
//         col = GetComponent<Collider2D>();
//         animator = GetComponent<Animator>();
//     }

//     protected virtual void FixedUpdate(){
//         ChangeFacing();
//     }

//     private void ChangeFacing(){
//         if(rb.velocity.x > 0.1f){
//             facing = Facing.Right;
//         }
//         else if(rb.velocity.x <-0.1f){
//             facing = Facing.Left;
//         }
//         float scaleX = facing == Facing.Right ? 1f : -1f;
//         transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*scaleX , transform.localScale.y , transform.localScale.z);
//     }

// }
