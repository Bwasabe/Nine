using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

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
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void AttackEnter()
    {
        rb.velocity = Vector2.up * rb.velocity.y;
    }

    protected override void Attack()
    {
        //TODO: 풀링 소환
        Vector2 dir = GameManager.Instance.Player.transform.position - transform.position;
        GameObject g = Instantiate(bullet, shootPos.position, Quaternion.identity);
        g.SetActive(true);
        g.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(Mathf.Atan2(dir.y, dir.x), -enemyFOV.ViewAngle * 0.5f, enemyFOV.ViewAngle * 0.5f) * Mathf.Rad2Deg);
        Debug.Log(g.transform.rotation);
    }



}
