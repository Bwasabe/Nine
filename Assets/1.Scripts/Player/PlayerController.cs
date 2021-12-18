using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action attack;
    public event Action slide;
    public event Action colEnter;
    public event Action colExit;

    private void Awake() {
        attack += () => { };
        slide += () => { };
        colEnter += () => { };
        colExit += () => { };
    }

    private void OnTriggerEnter2D(Collider2D other) {
        colEnter();
    }
    private void OnTriggerExit2D(Collider2D other) {
        colExit();
    }



}
