using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel;
    public void OnClickOption(){
        InputManager.Instance.gameObject.SetActive(true);
        optionPanel.SetActive(true);
    }

    public void OnClickExit(){
        InputManager.Instance.gameObject.SetActive(false);
        optionPanel.SetActive(false);

    }
}
