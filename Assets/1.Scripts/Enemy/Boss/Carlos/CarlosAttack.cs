using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;


public class CarlosAttack : MonoBehaviour
{

    public enum Pattons
    {
        Patton1 = 1,
        Patton2 = 0,
        Length = 2,
    }

    [SerializeField]
    private Transform _rollingDice = null;


    [SerializeField]
    private float _rollingDuration = 0.5f;

    [SerializeField]
    private int _patton = 0;
    [SerializeField]
    private int _dice = 0;

    [SerializeField]
    private float _attackDelay = 5f;


    [Header("패턴 1")]
    [SerializeField]
    private GameObject _spawnEnemy = null;

    [SerializeField]
    private List<Transform> _enemySpawnTransform = new List<Transform>();

    [Header("패턴 2")]
    [SerializeField]
    private float _diceGroundPoundDelay = 0.5f;


    private CarlosMove _carlosMove = null;


    private IEnumerator Start()
    {
        _carlosMove = GetComponent<CarlosMove>();
        yield return WaitForSeconds(1f);
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Spin();
            yield return WaitForSeconds(_attackDelay);
        }
    }


    [ContextMenu("스핀")]
    private void Spin()
    {
        Vector3 diceDir = Vector3.zero;
        _patton = 1;//Random.Range((int)Pattons.Patton1, (int)Pattons.Length);

        _dice = Random.Range(1, 4);
        switch (_dice)
        {
            case 1: // 6 , 1
                diceDir = Vector3.up * (_patton * 180f);
                break;
            case 2: //5 , 2
                diceDir = Vector3.right * (90 + _patton * 180f);
                break;
            case 3: // 4 , 3
                diceDir = Vector3.up * (90 + _patton * 180f);
                break;
            default:
                Debug.LogError("먼가 이상함");
                break;
        }

        diceDir.y -= 3600f;
        diceDir.z = 360f;

        _rollingDice.DOLocalRotate(diceDir, _rollingDuration, RotateMode.FastBeyond360).OnComplete(() =>
        {
            _rollingDice.rotation = Quaternion.Euler(diceDir.x, diceDir.y, 0f);
            Attack();
            Debug.Log("1단계 완료   ");
        });
    }

    private void Attack()
    {
        switch (_patton)
        {
            case (int)Pattons.Patton1:
                Patton1();
                break;
            case (int)Pattons.Patton2:
                StartCoroutine(Patton2());
                break;
            default:
                Debug.LogError("뭔가 이상함");
                break;
        }
    }

    private void Patton1()
    {
        Debug.Log("왜");
        for (int i = 0; i < _dice; i++)
        {
            int randomSpawnPos = Random.Range(0, _enemySpawnTransform.Count);
            GameObject g = Instantiate(_spawnEnemy, _enemySpawnTransform[randomSpawnPos].position , Quaternion.identity);
            g.SetActive(true);
            Rigidbody2D rb = g.GetComponent<Rigidbody2D>();

            Debug.Log(g);
            switch (randomSpawnPos)
            {
                case 0:
                    rb.AddForce(Vector2.right * 0.02f);
                    break;
                case 1:
                    rb.AddForce(Vector2.left * 0.02f);
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator Patton2()
    {
        for (int i = 0; i < 7 - _dice; i++)
        {
            //Instantiate()
            yield return WaitForSeconds(_diceGroundPoundDelay);
        }
    }

}
