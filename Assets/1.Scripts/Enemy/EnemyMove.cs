using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyFOV))]

public class EnemyMove : CharacterMove
{


    private float timer;
    protected override void Start()
    {
        base.Start();
    }

    

    private void CheckPlatform()
    {
        if (IsPlatformExist() == false)
        {
            isFacingRight = !isFacingRight;
        }
    }

    
    public void SetIsFacingToLocalScale()
    {
        isFacingRight = transform.localScale.x > 0 ? true : false;
    }
    public bool IsPlatformExist()
    {
        return Physics2D.OverlapCircle(new Vector2(isFacingRight ? col.bounds.max.x + 1f : col.bounds.min.x - 1f, col.bounds.min.y), 0.1f, layerMask);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector2(isFacingRight ? col.bounds.max.x + 1f : col.bounds.min.x - 1f, col.bounds.min.y), 0.1f);
    }


}
