using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    #region PublicValues
    public PlayerMove PlayerMove
    {
        get
        {
            if(playerMove == null)
            {
                playerMove = FindObjectOfType<PlayerMove>();
            }
            return playerMove;
        }
        private set{}
    }
    public PlayerController Player{
        get{
            if(!player){
                player = FindObjectOfType<PlayerController>();
            }
            return player;
        }
        private set{}
    }
    #endregion

    #region ReturnValuesOfPublic
    private PlayerMove playerMove;
    private PlayerController player;
    #endregion


}
