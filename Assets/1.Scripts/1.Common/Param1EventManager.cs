using System.Collections.Generic;
using System;

public class Param1EventManager<Object>
{
    /// <summary>
    /// EventManager<ReturnType>.FunctionName("KeyName" , Func)
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
}
