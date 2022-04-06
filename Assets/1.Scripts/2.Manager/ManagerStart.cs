using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class ManagerStart : MonoSingleton<ManagerStart>
{
    [SerializeField]
    private Image _fadeObject = null;

    public Image FadeObject
    {
        get
        {
            return _fadeObject;
        }
    }

    [SerializeField]
    private Color _firstFadeColor;


    [SerializeField]
    private Color _fadeColor;



    #region 이벤트 함수

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.TriggerEvent("Scene" + _currentScene);
        }
    }

    #endregion
    public void Fade()
    {
        _fadeObject.color = _fadeColor;
    }
    public void FirstFade()
    {
        _fadeObject.color = _firstFadeColor;
    }

    private int _currentScene = 1;
    public int CurrentScene
    {
        get
        {
            return _currentScene;
        }
    }

    private bool CheckScene(string value)
    {
        string scene = "Scene";
        for (int i = 0; i < 5; i++)
        {
            if (value[i] != scene[i])
            {
                return false;
            }
        }
        return true;
    }
    public void SetCurrentScene(string methodName, int value)
    {
        if (CheckScene(methodName))
        {
            _currentScene = value;
        }
        else
        {
            throw new System.Exception(MethodBase.GetCurrentMethod().DeclaringType.FullName + " is Not FriendClass");
        }
    }
}
