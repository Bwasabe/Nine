using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust_DestroyEvent : MonoBehaviour, IPoolable
{
    public void destroyEvent()
    {
        gameObject.SetActive(false);
    }
    public void OnPool(){

    }
}
