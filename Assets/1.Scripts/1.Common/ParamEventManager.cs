using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParamEventManager<Object>
{
    private static Dictionary<string, Action<Object>> eventParamDictionary = new Dictionary<string, Action<Object>>();

    public static void StartListening(string eventName, Action<Object> listener)
    {
        Action<Object> thisEvent;
        if (eventParamDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventParamDictionary[eventName] = thisEvent;
        }
        else
        {
            eventParamDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action<Object> listener)
    {
        Action<Object> thisEvent;
        if (eventParamDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventParamDictionary[eventName] = thisEvent;
        }
        else
        {
            eventParamDictionary.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName, Object param)
    {
        Action<Object> thisEvent;
        if (eventParamDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(param);
        }
    }


    private static Dictionary<string, Func<Object, Object>> eventParamFuncDictionary = new Dictionary<string, Func<Object, Object>>();

    public static void StartListening(string eventName, Func<Object, Object> listener)
    {
        Func<Object, Object> thisEvent;
        if (eventParamFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventParamFuncDictionary[eventName] = thisEvent;
        }
        else
        {
            eventParamFuncDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Func<Object, Object> listener)
    {
        Func<Object, Object> thisEvent;
        if (eventParamFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventParamFuncDictionary[eventName] = thisEvent;
        }
        else
        {
            eventParamFuncDictionary.Remove(eventName);
        }
    }

    public static void FuncParamTriggerEvent(string eventName, Object param)
    {
        Func<Object, Object> thisEvent;
        if (eventParamFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }
}
