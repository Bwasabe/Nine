using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Character/Status", order = 0)]
public class Character : ScriptableObject {
    public new string name;
    public int hp;
    public float speed;
    public int damage;
}