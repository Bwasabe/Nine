using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Player/Status", order = 0)]
public class Player : ScriptableObject {
    public new string name;
    public int hp;
    public float speed;
    public float jumpPower;
    public float slidingSpeed;
    public float slidingDuration;
    public int jumpCount;
    public int jumpMaxCount;
    public float gravity;

    public Player(Player player){
        this.name = player.name;
        this.hp = player.hp;
        this.speed = player.speed;
        this.jumpPower = player.jumpPower;
        this.slidingSpeed = player.slidingSpeed;
        this.jumpCount = player.jumpCount;
        this.jumpMaxCount = player.jumpMaxCount;
        this.gravity = player.gravity;
    }

}