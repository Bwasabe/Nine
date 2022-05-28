using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ddiring : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start(){
        PoolManager.CreatePool<IPoolObj>("LandingDust", gameObject, 2);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){

            Vector3 disVec = Vector3.Cross(transform.right, other.transform.position - transform.position );
            if(disVec.z<0f){
                Transform dust = PoolManager.GetItem<IPoolObj>("LandingDust").transform;
                dust.SetParent(null);
                dust.localScale = Vector3.one;
                dust.position = transform.position;
            }
        }
    }
}
