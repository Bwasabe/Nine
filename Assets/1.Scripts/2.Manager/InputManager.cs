using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Keys
{
    LEFT,
    RIGHT,
    DOWN,
    JUMP,
    SLIDE,
    ATTACK,
    INTERACT,
    LENGTH,
}

public static class KeySetting
{
    public static Dictionary<Keys, KeyCode> keyMaps = new Dictionary<Keys, KeyCode>();
}

public class InputManager : MonoSingleton<InputManager>
{

    private KeyCode[] defaultKeys = { KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.Space, KeyCode.LeftShift, KeyCode.Mouse0, KeyCode.F };
    private string[] keyDescription = { "왼쪽", "오른쪽", "아래쪽", "점프", "슬라이딩", "공격", "상호작용" };
    private List<Text> keyList = new List<Text>();


    [SerializeField]
    private GameObject inputPanel;
    [SerializeField]
    private GameObject descPanel;


    [SerializeField]
    private Button keyButton;
    [SerializeField]
    private Transform buttonRoot;

    private Event e;


    private int key;



    private void Start()
    {
        Init(); 
    }
    private void Init()
    {
        Button newKeyButton;
        for (int i = 0; i < (int)Keys.LENGTH- 1; i++)
        {
            Debug.Log("잉");
            int temp = i;
            KeySetting.keyMaps.Add((Keys)temp, defaultKeys[temp]);
            newKeyButton = Instantiate(keyButton, buttonRoot);
            newKeyButton.onClick.AddListener(() => OnClickChange(temp));
            newKeyButton.gameObject.SetActive(true);
            keyList.Add(newKeyButton.transform.GetChild(0).GetComponent<Text>());
            newKeyButton.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}", keyDescription[temp]);
        }
        UpdateText();
        gameObject.SetActive(false);
    }

    public void OnClickChange(int num)
    {
        descPanel.SetActive(true);
        key = num;
    }

    private void Update()
    {
        //Debug.Log(KeySetting.keyMaps[Keys.LEFT]);
    }
    private void OnGUI()
    {
        if (!descPanel.activeSelf) return;
        if (!Input.anyKeyDown) return;
        e = Event.current;
        if (e.keyCode == KeyCode.Escape)
        {
            descPanel.SetActive(false);
        }
        else if (e.isKey)
        {
            if (e.keyCode == KeyCode.None) return;
            KeySetting.keyMaps[(Keys)key] = e.keyCode;
            Debug.Log(KeySetting.keyMaps[(Keys)key]);
            descPanel.SetActive(false);
            UpdateText();
        }
        else if (e.isMouse)
        {
            switch (e.button)
            {
                case 0: KeySetting.keyMaps[(Keys)key] = KeyCode.Mouse0; break;
                case 1: KeySetting.keyMaps[(Keys)key] = KeyCode.Mouse1; break;
                case 2: KeySetting.keyMaps[(Keys)key] = KeyCode.Mouse2; break;
                default: break;

            }
            descPanel.SetActive(false);
            Debug.Log(e.button);
            Debug.Log(KeySetting.keyMaps[(Keys)key]);
            UpdateText();
        }

    }

    private void UpdateText()
    {
        for (int i = 0; i < (int)Keys.LENGTH-1; i++)
        {
            keyList[i].text = string.Format("{0}", KeySetting.keyMaps[(Keys)i]);
        }
    }
}




