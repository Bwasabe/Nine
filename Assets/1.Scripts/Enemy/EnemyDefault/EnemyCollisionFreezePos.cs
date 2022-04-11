using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyDamaged))]

public class EnemyCollisionFreezePos : MonoBehaviour
{
    private Rigidbody2D _playerRB = null;
    private Rigidbody2D _rb = null;

    private EnemyDamaged _enemyDamaged = null;


    private void Start() {
        _playerRB = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        _rb = GetComponent<Rigidbody2D>();
        _enemyDamaged = GetComponent<EnemyDamaged>();
    }
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            _rb.AddForce(_playerRB.velocity * -1f);
            _enemyDamaged.CheckPlayer();
        }
    }

}
