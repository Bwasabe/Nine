using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    ///TODOLIST
    //로프엑션 구현하기
    //캐릭터 공격 구현하기
    //적 캐릭터 베이스 구현하기
    #region PublicValues
    public PlayerMove PlayerMove
    {
        get
        {
            if (playerMove == null)
            {
                playerMove = FindObjectOfType<PlayerMove>();
            }
            return playerMove;
        }
        private set { }
    }
    public PlayerController Player
    {
        get
        {
            if (!player)
            {
                player = FindObjectOfType<PlayerController>();
            }
            return player;
        }
        private set { }
    }
    #endregion

    #region ReturnValuesOfPublic
    private PlayerMove playerMove;
    private PlayerController player;
    #endregion


}
