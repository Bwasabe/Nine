using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    ////TODOLIST 
    //// TODO : 적 충돌 아예 없애버리기 => 플레이어의 충돌을 없애는 쪽을 선택하는게 좋을듯
    //// TODO: 보스 만들기
    ////            ㄴ TODO: 주사위 떨구기
    ////            ㄴ TODO: 장판 만들기
    ////            ㄴ TODO: 불기둥을 만들기
    ////            ㄴ TODO: 플레이어 머리 위로 떨어지는 주사위 만들기

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
    public float TimeScale{
        get{
            return timeScale;
        }
        set{
            timeScale = value;
            Time.timeScale = timeScale;
        }
    }

    #endregion

    #region ReturnValuesOfPublic
    private PlayerMove playerMove;
    private PlayerController player;

    private float timeScale;
    #endregion
    


}
