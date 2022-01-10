using System.Collections.Generic;
using System;

public class Param2EventManager<Object , T>
{
    /// <summary>
    /// EventManager<Type>.FunctionName("KeyName" , Func)
    /// </summary>
    private static Dictionary<string, Func<Object , T>> eventFuncDictionary = new Dictionary<string, Func<Object , T>>();

    public static void StartListening(string eventName, Func<Object , T> listener)
    {
        Func<Object , T> thisEvent;
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

    public static void StopListening(string eventName, Func<Object , T> listener)
    {
        Func<Object , T> thisEvent;
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

    public static T TriggerEvent(string eventName , Object param)
    {
        Func<Object , T> thisEvent;
        if (eventFuncDictionary.TryGetValue(eventName, out thisEvent))
        {
            return thisEvent.Invoke(param);
        }
        else{
            return default;
        }
    }


}
