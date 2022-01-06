using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GameManager.Instance.Player.colEnter += () =>
            {
                Debug.Log("상자");
                
            };
        }
    }
}
