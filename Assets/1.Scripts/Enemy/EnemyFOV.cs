using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{

    [Range(0, 360)]
    [SerializeField]
    private float viewAngle = 40f;
    [SerializeField]
    private float viewRange;
    [SerializeField]
    private float attackRange = 5f;
    [SerializeField]
    private float bungOffRange = 2f;

    public float ViewAngle
    {
        get
        {
            return viewAngle;
        }
    }

    public float ViewRange
    {
        get
        {
            return viewRange;
        }
    }
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }
    public float BungOffRange
    {
        get
        {
            return bungOffRange;
        }
    }

    public readonly float tolerance = 1.192586f;



    [SerializeField]
    private LayerMask obstacleLayerMask;

    private int playerLayer;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }


    public bool IsTracePlayer()
    {
        bool isTrace = false;
        Collider2D col = Physics2D.OverlapCircle(transform.position, viewAngle, 1 << playerLayer);

        if (col != null)
        {
            // z축 필요없으니 벡터 2로 변환시킴
            Vector2 dir = GameManager.Instance.Player.transform.position - transform.position;
            dir -= Vector2.up * tolerance;
            dir.Normalize();
            if (Vector2.Angle(transform.localScale.x > 0 ? transform.right : transform.right * -1f, dir) < viewAngle* 0.5f)
            {

                isTrace = true;
                Debug.Log("와 쫒아");
            }
        }

        return isTrace;
    }



    public bool IsViewPlayer()
    {
        bool isView = false;
        Vector2 dir = GameManager.Instance.Player.transform.position - transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast
            (transform.position, dir.normalized, Mathf.Infinity, obstacleLayerMask);

        if (hit2D.collider != null)
        {
            isView = (hit2D.collider.gameObject.CompareTag("Player"));
        }

        return isView;
    }

    public bool IsDistancePossible(float distance)
    {
        return (GameManager.Instance.Player.transform.position - transform.position).sqrMagnitude
            <= Mathf.Pow(distance, 2);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, viewAngle);
        // Vector2 dir = transform.right * (viewAngle + 90f);
        Vector3 left = Quaternion.AngleAxis(-viewAngle / 2, Vector3.forward) * (transform.localScale.x > 0 ? Vector3.right : Vector3.left);
        Vector3 right = Quaternion.AngleAxis(viewAngle / 2, Vector3.forward) * (transform.localScale.x > 0 ? Vector3.right : Vector3.left);
        Gizmos.DrawLine(transform.position, transform.position + left * viewRange);
        Gizmos.DrawLine(transform.position, transform.position + right * viewRange);
        //Gizmos.DrawLine(transform.position, Mathf.Atan2(dir.y , dir.x)    );
        //Gizmos.DrawLine(transform.position, new Vector2(Mathf.Sin((viewAngle + 90f) * Mathf.Deg2Rad), Mathf.Cos((viewAngle+ 90f) * Mathf.Deg2Rad) * viewRange));

        // Gizmos.DrawLine(transform.position, new Vector2(Mathf.Sin((viewAngle + 90f) * Mathf.Deg2Rad), Mathf.Cos((viewAngle+ 90f) * Mathf.Deg2Rad) * viewRange));
        // Gizmos.DrawLine(transform.position, new Vector2(Mathf.Sin((viewAngle + 90f) * Mathf.Deg2Rad), Mathf.Cos((viewAngle+ 90f) * Mathf.Deg2Rad))* viewRange * -1f);

        //Gizmos.DrawLine(transform.position, new Vector2(Mathf.Cos(-viewAngle+90) ,Mathf.Sin(-viewAngle+90)*viewRange ));
    }

}
