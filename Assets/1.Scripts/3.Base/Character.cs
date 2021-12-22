using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Character/Status", order = 0)]
public class Character : ScriptableObject {
    public new string name;
    public int hp;
    public float speed;
    public float jumpPower;
    public float slidingSpeed;
    public float slidingDuration;
    public int jumpCount;
    public int jumpMaxCount;
    public float gravity;

    public Character(Character character){
        this.name = character.name;
        this.hp = character.hp;
        this.speed = character.speed;
        this.jumpPower = character.jumpPower;
        this.slidingSpeed = character.slidingSpeed;
        this.jumpCount = character.jumpCount;
        this.jumpMaxCount = character.jumpMaxCount;
        this.gravity = character.gravity;
    }

}