using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandMessge : MonoSingleton<SandMessge>
{
    void Start()
    {
        PoolManager.CreatePool<IPoolObj>("MessegeBox", gameObject);
    }
    public static void MessegeBoxOn(string text){
        PoolManager.GetItem<IPoolObj>("MessegeBox");
    }
}
