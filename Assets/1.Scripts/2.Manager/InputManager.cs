using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Keys
{
    LEFT,
    RIGHT,
    JUMP,
    DOWN,
    SLIDE,
    ATTACK,
}
public class InputManager : MonoSingleton<InputManager>
{

    public static Dictionary<Keys, KeyCode> keyMaps = new Dictionary<Keys, KeyCode>();


    [SerializeField]
    private GameObject inputPanel;
    [SerializeField]
    private GameObject descPanel;

    [SerializeField]
    private Text[] keyTexts;
    private Event e;

    private int key;

    private bool isKeyChanging = false;

    private void Start()
    {
        // Debug.Log(keyMaps.Count);
        // foreach(KeyValuePair<Keys, KeyCode> item in keyMaps)
        // {
        //     Debug.Log(item.Key);
        // }
        keyMaps.Add(Keys.ATTACK, KeyCode.Mouse0);
        keyMaps.Add(Keys.JUMP, KeyCode.Space);
        keyMaps.Add(Keys.SLIDE, KeyCode.LeftShift);
        keyMaps.Add(Keys.LEFT, KeyCode.A);
        keyMaps.Add(Keys.RIGHT, KeyCode.D);
        keyMaps.Add(Keys.DOWN, KeyCode.S);
    }


    public void OnClickChange(int num)
    {
        descPanel.SetActive(true);
        isKeyChanging = true;
        key = num;
    }

    // private void Update()
    // {
    //     if (Input.anyKeyDown)
    //     {
    //         if (isKeyChanging)
    //         {
    //             e = Event.current;
    //             if(e.keyCode == KeyCode.None)return;
    //             if(e.isKey){
    //                 keyMaps[currentKey] = e.keyCode;
    //             }
    //             else if(e.isMouse){
    //                 keyMaps[currentKey] = e.button;
    //             }
    //             Debug.Log(keyMaps[Keys.LEFT]);
    //         }
    //     }

    //     if (e.isKey)
    //     {
    //         Debug.Log("Detected a keyboard event!" + e.keyCode);
    //     }
    // }
    private void OnGUI()
    {
        if (!isKeyChanging) return;
        if(!Input.anyKeyDown)return;
        e = Event.current;
        if(e.keyCode == KeyCode.None)return;
        if (e.keyCode == KeyCode.Escape)
        {
            descPanel.SetActive(false);
            isKeyChanging = false;
        }
        else if (e.isKey)
        {
            keyMaps[(Keys)key] = e.keyCode;
            Debug.Log(keyMaps[(Keys)key]);
            UpdateText();
        }
        else if (e.isMouse)
        {
            switch (e.button)
            {
                case 0: keyMaps[(Keys)key] = KeyCode.Mouse0; break;
                case 1: keyMaps[(Keys)key] = KeyCode.Mouse1; break;
                case 2: keyMaps[(Keys)key] = KeyCode.Mouse1; break;
                default: break;

            }
            Debug.Log(keyMaps[(Keys)key]);
            UpdateText();
        }

    }

    private void UpdateText(){
        for (int i = 0; i < keyTexts.Length; i++){
            keyTexts[i].text = string.Format("{0}", keyMaps[(Keys)i]);
        }
    }

}




