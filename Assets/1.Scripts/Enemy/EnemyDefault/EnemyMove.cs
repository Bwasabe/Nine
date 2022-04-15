using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(EnemyFOV))]

public class EnemyMove : CharacterMove
{
    [SerializeField]
    protected LayerMask layerMask;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected void CheckPlatform()
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

    

    // private void ChangeRotatableObj(){
    //     int dir = 0;
    //     if(transform.localScale.x > 0){
    //         dir = 1;
    //     }
    //     else{
    //         dir = -1;
    //     }
    //     _rotatableObj.transform.localPosition = new Vector2(Mathf.Abs(_rotatableObj.transform.localPosition.x)*dir , _rotatableObj.localPosition.y);
    // }

}
