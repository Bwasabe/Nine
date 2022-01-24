using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
[RequireComponent(typeof(EnemyAI))]

public class EnemyMove : MonoBehaviour
{
    private EnemyAI enemyAI;
    private CharacterBase characterBase;

    private void Start() {
        characterBase = GetComponent<CharacterBase>();
    }



}
