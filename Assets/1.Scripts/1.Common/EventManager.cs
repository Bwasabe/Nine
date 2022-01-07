using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager<Object> : MonoBehaviour
{

    private static Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

    public static void StartListening(string eventName, Action listener)
    {
        Action thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action listener)
    {
        Action thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        Action thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }
    /// <summary>
    /// EventManager<Type>.FunctionName("KeyName" , Func)
    /// </summary>
    private static Dictionary<string, Func<Object>> eventFuncDictionary = new Dictionary<string, Func<Object>>();

    public static void StartListening(string eventName, Func<Object> listener)
    {
        Func<Object> thisEvent;
        if (eventFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventFuncDictionary[eventName] = thisEvent;
        }
        else
        {
            eventFuncDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Func<Object> listener)
    {
        Func<Object> thisEvent;
        if (eventFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventFuncDictionary[eventName] = thisEvent;
        }
        else
        {
            eventFuncDictionary.Remove(eventName);
        }
    }

    public static void FuncTriggerEvent(string eventName, Object param)
    {
        Func<Object> thisEvent;
        if (eventFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

}
