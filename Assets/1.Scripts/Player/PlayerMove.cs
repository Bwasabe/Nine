using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : MonoBehaviour
{
    public event Action move;
    public event Action jump;

    private void Awake() {
        move += () => { };
        jump += () => { };
    }

    

}
