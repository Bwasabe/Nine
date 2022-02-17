using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hi : MonoBehaviour
{
    float time; float time2;
    private PlayerMove playerMove;
    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        PoolManager.CreatePool<IPoolObj>("Ghost",GameManager.Instance.gameObject);
    }
    public void GOGhost(float time, float time2){
        this.time = time;
        this.time2 = time2;
        StartCoroutine(Ghost());
        
            
    }
    private IEnumerator Ghost(){
        float time3 = 0f;
        bool loop = true;
        yield return Yields.WaitForSeconds(0.03f);
        while(loop){
            IPoolObj iPoolObj = PoolManager.GetItem<IPoolObj>("Ghost");
            iPoolObj.gameObject.SetActive(true);
            iPoolObj.SetSprite(playerMove.spriteRenderer.sprite ,playerMove.isBack);
            iPoolObj.transform.position = transform.position;
            
            
            time3 += time2;
            if(time3>time){
                loop = false;
            }
            yield return Yields.WaitForSeconds(time2);
        }
    }
}
