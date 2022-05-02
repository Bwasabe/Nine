using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Reflection;


[RequireComponent(typeof(CarlosMove))]
[RequireComponent(typeof(EnemyAI))]

public class CarlosAttack : MonoBehaviour
{
    public enum Pattons
    {
        Patton2 = 0,
        Patton1 = 1,
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
    private AnimationClip _rollingDiceClip = null;
    [SerializeField]
    private GameObject _spawnEnemy = null;

    [SerializeField]
    private List<Transform> _enemySpawnTransform = new List<Transform>();

    [SerializeField]
    private int _firstRand = 1;
    [SerializeField]
    private int _endRand = 4;

    [Header("패턴 2")]
    [SerializeField]
    private AnimationClip _carlosImpactTable = null;
    [SerializeField]
    private GameObject _groundPoundDice = null;
    [SerializeField]
    private float _diceGroundPoundDelay = 0.5f;

    [Header("카메라 흔들기 속성")]
    [SerializeField]
    private float _shakeCamDuration = 0.2f;
    [SerializeField]
    private float _shakeCamStrength = 3f;
    [SerializeField]
    private int _shakeCamVibrato = 10;
    [SerializeField]
    private float _shakeCamRandomness = 45f;


    private CarlosMove _carlosMove = null;

    private Transform _playerTransform = null;

    private Vector3 _tempDiceDir = Vector3.zero;

    private EnemyAI _enemyAI = null;

    private Animator _animator = null;

    private void Start()
    {
        Debug.Log(MethodBase.GetCurrentMethod().DeclaringType.FullName);
        Debug.Log(MethodBase.GetCurrentMethod().DeclaringType);
        Debug.Log(Assembly.GetExecutingAssembly().ManifestModule.Name);
        Debug.Log(Assembly.GetExecutingAssembly().ManifestModule.ScopeName);
        Debug.Log(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
        
        _animator = GetComponent<Animator>();
        _enemyAI = GetComponent<EnemyAI>();
        _playerTransform = GameManager.Instance.Player.transform;
        _carlosMove = GetComponent<CarlosMove>();
        //yield return WaitForSeconds(1f);
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Spin();
            yield return WaitUntil(() => _enemyAI.FSM.State == EnemyAI.States.Idle);
            yield return WaitForSeconds(_attackDelay);
        }
    }


    private void Spin()
    {
        _enemyAI.FSM.ChangeState(EnemyAI.States.Attack);
        Vector3 diceDir = Vector3.zero;
        _patton = Random.Range(0, (int)Pattons.Length);

        //_animator.SetFloat("Patton", _patton);

        _dice = Random.Range(_firstRand, _endRand);

        switch (_dice)
        {
            case 1: // 6 , 1
                diceDir = Vector3.up * (_patton * 180f);
                //diceDir.z = 360f;
                break;
            case 2: //5 , 2
                diceDir = Vector3.right * (90f + _patton * 180f);//Vector3.one * -90f + (Vector3.up * _patton * 180f); //Vector3.right * (90 + _patton * 180f);
                //diceDir.z = 270f;
                break;
            case 3: // 4 , 3
                diceDir = Vector3.up * (90 + _patton * 180f);
                break;
            default:
                Debug.LogError("먼가 이상함");
                break;
        }
        _tempDiceDir = diceDir;
        diceDir.z = 360f;
        diceDir.y -= 3600f;

        _rollingDice.parent.localRotation = Quaternion.identity;
        _rollingDice.DOLocalRotate(diceDir, _rollingDuration,RotateMode.FastBeyond360).OnComplete(() =>
        {
            _rollingDice.parent.localRotation = _rollingDice.localRotation;
            _rollingDice.localRotation = Quaternion.identity;
            Attack();
        });
    }

    protected void EndToRollingDice(){
        _enemyAI.FSM.ChangeState(EnemyAI.States.Idle);
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
        for (int i = 0; i < _dice; i++)
        {
            int randomSpawnPos = Random.Range(0, _enemySpawnTransform.Count);
            GameObject g = Instantiate(_spawnEnemy, _enemySpawnTransform[randomSpawnPos].position, Quaternion.identity);
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
            g.GetComponent<CarlosEnemyDamaged>().SetDiceNumber(MethodBase.GetCurrentMethod().DeclaringType.FullName, _dice);
        }
        _enemyAI.FSM.ChangeState(EnemyAI.States.Idle);
    }

    private IEnumerator Patton2()
    {
        _animator.Play(_carlosImpactTable.name);
        yield return WaitForSeconds(_carlosImpactTable.length);
        for (int i = 0; i < 7 - _dice; i++)
        {
            GameObject g = Instantiate(_groundPoundDice , _playerTransform.position, Quaternion.identity);
            g.transform.SetParent(null);
            g.transform.GetChild(0).rotation = Quaternion.Euler(_tempDiceDir.x, _tempDiceDir.y, _tempDiceDir.z);
            g.SetActive(true);

            yield return WaitForSeconds(_diceGroundPoundDelay);
        }
        _enemyAI.FSM.ChangeState(EnemyAI.States.Idle);
    }

    protected void ShakeCamPatton2(){
        ObjectManager.Instance.VirtualCamera.transform.DOShakePosition(_shakeCamDuration, _shakeCamStrength, _shakeCamVibrato, _shakeCamRandomness);
    }

}
