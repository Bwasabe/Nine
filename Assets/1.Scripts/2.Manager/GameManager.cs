using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerMove Player{ get; private set; }

    private void Start() {
        Player = FindObjectOfType<PlayerMove>();
    }
}
