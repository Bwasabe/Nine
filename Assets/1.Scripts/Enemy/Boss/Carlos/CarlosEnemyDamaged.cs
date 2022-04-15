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
    private Vector3[] _cubeScale = { Vector3.one * 3f, Vector3.one * 2f, Vector3.one * 1f };

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

    protected override void OverrideDead()
    {
        SpawnDice();
    }   

    private void SpawnDice()
    {
        GameObject g = Instantiate(_deadDices, _diceSpawnPos);
        g.SetActive(true);
        g.transform.SetParent(null);
        g.transform.localScale = _cubeScale[1];
        Debug.Log(g.transform.localScale);
        g.transform.position = new Vector2(g.transform.position.x, GameManager.Instance.Player.transform.position.y);
    }
}
