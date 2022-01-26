using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IPoolObj : MonoBehaviour, IPoolable
{
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float disableTime;
    [SerializeField]
    private DisableMotion disableMotion;
    private enum DisableMotion{
        faid_out,
        fly_up,
        fly_down,
    }
    
    public void OnPool(){

        StartCoroutine(Die());
    }
    private IEnumerator Die(){
        yield return new WaitForSeconds(lifeTime);
        switch(disableMotion){
            case DisableMotion.faid_out:
                GetComponent<Image>().DOFade(0f,disableTime).OnComplete(()=>gameObject.SetActive(false));
            break;
            case DisableMotion.fly_up:

            break;
            case DisableMotion.fly_down:

            break;
        }
    }




}
