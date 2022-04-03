using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    ///TODOLIST
    //TODO: 적 시야각 Gizemo만들기
    //TODO: 총알 제대로 날리기
    //TODO: 한번 어텍에 들어가면 총을 쏜후 chase로 돌아가기

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
