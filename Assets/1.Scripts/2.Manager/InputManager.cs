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

public class InputManager : MonoSingleton<InputManager>
{
    public static Dictionary<Keys, KeyCode> keyMaps = new Dictionary<Keys, KeyCode>();

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
            keyMaps.Add((Keys)temp, defaultKeys[temp]);
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
            keyMaps[(Keys)key] = e.keyCode;
            descPanel.SetActive(false);
            UpdateText();
            CheckOverlap();
        }
        else if (e.isMouse)
        {
            switch (e.button)
            {
                case 0: keyMaps[(Keys)key] = KeyCode.Mouse0; break;
                case 1: keyMaps[(Keys)key] = KeyCode.Mouse1; break;
                case 2: keyMaps[(Keys)key] = KeyCode.Mouse2; break;
                default: break;

            }
            descPanel.SetActive(false);
            UpdateText();
            CheckOverlap();
        }

    }

    private void UpdateText()
    {
        for (int i = 0; i < (int)Keys.LENGTH-1; i++)
        {
            keyList[i].text = string.Format("{0}", keyMaps[(Keys)i]);
        }
    }

    private void CheckOverlap(){
        for (int i = 0; i < keyMaps.Count; i++){
            if(i == key)continue;
            if(keyMaps[(Keys)i] == keyMaps[(Keys)key]){
                Debug.Log(key);
                Debug.Log(i);
                buttonRoot.GetChild(key+1).GetComponent<Image>().color = Color.red;
                buttonRoot.GetChild(i+1).GetComponent<Image>().color = Color.red;
            }
        }
    }
}




