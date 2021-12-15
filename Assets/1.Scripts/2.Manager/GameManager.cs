using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerMovie Player{ get; private set; }

    private void Start() {
        Player = FindObjectOfType<PlayerMovie>();
    }
}
