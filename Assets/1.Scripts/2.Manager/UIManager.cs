using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject inputManager;


    private PlayerController player;
    private PlayerMove playerMove;

    private void Start() {
        player = GameManager.Instance.Player;
        playerMove = GameManager.Instance.PlayerMove;
    }
    public void OnClickOption(){
        inputManager.SetActive(true);
        optionPanel.SetActive(true);
        player.slide -= player.getSlide;
        playerMove.IsFreeze();

    }

    public void OnClickExit(){
        inputManager.SetActive(false);
        optionPanel.SetActive(false);
        player.slide += player.getSlide;
        playerMove.IsMove();
    }
}
