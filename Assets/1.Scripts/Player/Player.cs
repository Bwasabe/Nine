using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO List
//TODO : 플레이어가 고군분투처럼 spring joint를 이용해서 날라다니기
//TODO : 적에게 벽력일섬쓰는거 만들기
//TODO : 공격시 적에게 빨려들어가는거 만들기


public class Player : MonoBehaviour
{
    private PlayerMove player;

    #region 이벤트
    private void Start() {
        player = GameManager.Instance.Player;
    }
    // private void Update() {
    //     if(Input.GetMouseButtonDown(0)){

    //     }
    // }
    #endregion

    #region 충돌체크

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Box")){
            player.MinusJumpCount(1);
            player.ResetVelocity(Vector2.zero);
        }
    }
    #endregion



}
