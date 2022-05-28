using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlosEnemyDamaged : EnemyDamaged
{
    [SerializeField]
    private GameObject _deadDices = null;
    [SerializeField]
    private Transform _diceSpawnPos = null;
    [SerializeField]
    private Vector3[] _cubeScale = { Vector3.one * 4f, Vector3.one * 2f, Vector3.one * 1.5f };

    private const string _friendClassName = "Carlos";

    private int _diceNumber = 0;

    public void SetDiceNumber(string friendClassName, int setNumber)
    {
        for (int i = 0; i < _friendClassName.Length; i++)
        {
            if (_friendClassName[i] != friendClassName[i])
            {
                return;
            }
        }
        _diceNumber = setNumber;
    }

    public override void Dead()
    {
        base.Dead();
        SpawnDice();
    }

    private void SpawnDice()
    {
        GameObject parent = Instantiate(_deadDices, _diceSpawnPos.position + Vector3.left * 5f, Quaternion.identity);

        parent.transform.SetParent(null);
        parent.transform.localScale = _cubeScale[_diceNumber - 1];
        Transform g = parent.transform.GetChild(0);


        Vector3 diceDir = Vector3.zero;
        switch (_diceNumber)
        {
            case 1: // 6 , 1
                diceDir = Vector3.up * 180f;
                break;
            case 2: //5 , 2
                diceDir = Vector3.right * 270f;
                break;
            case 3: // 4 , 3
                diceDir = Vector3.up * 270f;
                break;
            default:
                Debug.LogError("먼가 이상함");
                break;
        }
        g.rotation = Quaternion.Euler(diceDir.x, diceDir.y, diceDir.z);
        Debug.Log(_diceNumber + "asf  " + diceDir);
        parent.transform.position = new Vector2(parent.transform.position.x, GameManager.Instance.Player.transform.position.y);
        parent.SetActive(true);
    }
}
