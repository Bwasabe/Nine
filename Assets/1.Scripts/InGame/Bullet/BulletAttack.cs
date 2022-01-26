using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().Damage(damage);
        }
        //TODO: 벽에 막히는게 없음
    }
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Downable") || other.gameObject.CompareTag("Ground"))
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
