using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LowerGuardAttack : EnemyAttack
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform shootPos;



    private Rigidbody2D rb;


    private void Start()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        rb = GetComponentInParent<Rigidbody2D>();
    }
    protected override void AttackEnter()
    {
        rb.velocity = Vector2.up * rb.velocity.y;
    }

    protected override void Attack()
    {
        //TODO: 풀링 소환
        Vector2 dir = GameManager.Instance.Player.transform.position - transform.position;
        dir -= Vector2.up * enemyFOV.tolerance;
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        GameObject g = Instantiate(bullet, shootPos.position, Quaternion.Euler(0f, 0f, rotationZ));
        g.SetActive(true);
        if (enemyMove.isFacingRight)
        {
            rotationZ = Mathf.Clamp(g.transform.eulerAngles.z, -enemyFOV.ViewAngle, enemyFOV.ViewAngle);
        }
        else
        {
            rotationZ = Mathf.Clamp(g.transform.eulerAngles.z, -enemyFOV.ViewAngle + 180f, enemyFOV.ViewAngle + 180f);
        }
        g.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        //g.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);// -enemyFOV.ViewAngle * 0.5f, enemyFOV.ViewAngle * 0.5f * Mathf.Rad2Deg);


        base.Attack();
    }



}
