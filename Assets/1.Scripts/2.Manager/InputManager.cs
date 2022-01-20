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
    USECARD,
    INTERACT,
    CARD1,
    CARD2,
    CARD3,
    CARD4,
    CARD5,
    LENGTH,
}

public class InputManager : MonoBehaviour
{
    public static Dictionary<Keys, KeyCode> keyMaps = new Dictionary<Keys, KeyCode>();

    private KeyCode[] defaultKeys = { KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.Space, KeyCode.LeftShift, KeyCode.Mouse0, KeyCode.E, KeyCode.F  ,
    KeyCode.Alpha1 , KeyCode.Alpha2 , KeyCode.Alpha3, KeyCode.Alpha4 , KeyCode.Alpha5};
    private string[] keyDescription = { "왼쪽", "오른쪽", "아래쪽", "점프", "슬라이딩", "공격", "카드사용", "상호작용", "카드1", "카드2", "카드3", "카드4", "카드5" };
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
        for (int i = 0; i < (int)Keys.LENGTH; i++)
        {
            int temp = i;
            keyMaps.Add((Keys)temp, defaultKeys[temp]);
            newKeyButton = Instantiate(keyButton, buttonRoot);
            newKeyButton.onClick.AddListener(() => OnClickChange(temp));
            newKeyButton.gameObject.SetActive(true);
            newKeyButton.name = i.ToString();
            keyList.Add(newKeyButton.transform.GetChild(0).GetComponent<Text>());
            newKeyButton.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0}", keyDescription[temp]);
        }
        UpdateText();
        gameObject.SetActive(false);
    }

    private void OnClickChange(int num)
    {
        descPanel.SetActive(true);
        key = num;
    }

    private void OnGUI()
    {
        if (!Input.anyKeyDown) return;
        e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.Escape)
            {
                descPanel.SetActive(false);
            }
            else if (e.isKey)
            {
                if (e.keyCode == KeyCode.None) return;
                keyMaps[(Keys)key] = e.keyCode;
            }
            else if (e.isMouse)
            {
                keyMaps[(Keys)key] = (KeyCode)(e.button + 323);
            }
        }
        else
        {
            return;
        }
        descPanel.SetActive(false);
        UpdateText();
        CheckOverlap();

    }

    private void UpdateText()
    {
        for (int i = 0; i < (int)Keys.LENGTH; i++)
        {
            keyList[i].text = string.Format("{0}", keyMaps[(Keys)i]);
        }
    }

    private void CheckOverlap()
    {
        for (int i = 0; i < keyMaps.Count; i++)
        {
            
            if (keyMaps[(Keys)i] == keyMaps[(Keys)key])
            {
                if (i == key) continue;
                buttonRoot.GetChild(key + 1).GetComponent<Image>().color = Color.red;
                Debug.Log(buttonRoot.GetChild(key + 1).GetComponent<Image>().color);
                buttonRoot.GetChild(i + 1).GetComponent<Image>().color = Color.red;
            }
            else
            {
                buttonRoot.GetChild(i + 1).GetComponent<Image>().color = Color.white;
                buttonRoot.GetChild(key + 1).GetComponent<Image>().color = Color.white;
            }
        }
    }
}




