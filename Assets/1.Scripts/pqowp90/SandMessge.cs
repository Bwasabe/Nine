using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SandMessge : MonoSingleton<SandMessge>
{
    private void Start()
    {
        PoolManager.CreatePool<IPoolObj>("MessageBox", gameObject);
    }
    public static void MessegeBoxOn(string text, Vector2 pos){

        IPoolObj Akimchi = PoolManager.GetItem<IPoolObj>("MessageBox");
        if(Akimchi){
            Akimchi.transform.localPosition = pos;
            Akimchi.transform.DOKill();
            Akimchi.SetText(text);
        }
    }
    
}
