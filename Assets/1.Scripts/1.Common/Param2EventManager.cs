using System.Collections.Generic;
using System;

public class Param2EventManager<Object, Object2>
{
    

    /// <summary>
    /// EventManager<Type>.FunctionName("KeyName" , Func)
    /// </summary>
    private static Dictionary<string, Func<Object, Object2>> eventParamFuncDictionary = new Dictionary<string, Func<Object, Object2>>();

    public static void StartListening(string eventName, Func<Object, Object2> listener)
    {
        Func<Object, Object2> thisEvent;
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

    public static void StopListening(string eventName, Func<Object, Object2> listener)
    {
        Func<Object, Object2> thisEvent;
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
        Func<Object, Object2> thisEvent;
        if (eventParamFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }

    private static Dictionary<string, Action<Object, Object2>> eventParamDictionary = new Dictionary<string, Action<Object, Object2>>();

    public static void StartListening(string eventName, Action<Object, Object2> listener)
    {
        Action<Object, Object2> thisEvent;
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

    public static void StopListening(string eventName, Action<Object, Object2> listener)
    {
        Action<Object, Object2> thisEvent;
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

    public static void TriggerEvent(string eventName, Object param, Object2 param2)
    {
        Action<Object, Object2> thisEvent;
        if (eventParamDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(param, param2);
        }
    }
    
}
