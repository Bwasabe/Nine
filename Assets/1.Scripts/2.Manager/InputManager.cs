using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    
    public KeyCode keyAttack;
    public KeyCode keyJump;
    public KeyCode keyRope;
    public KeyCode keySlide;
    public KeyCode keyLeft;
    public KeyCode keyRight; 


    private void OnClick(){
        Event e = Event.current;

        if(e.isMouse){
            InputManager.Instance.keyAttack = System.;
        }
    }

}


