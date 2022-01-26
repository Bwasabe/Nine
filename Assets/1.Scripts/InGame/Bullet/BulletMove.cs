using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float destroyDuration;


    protected virtual void OnEnable() {
        StartCoroutine(Disappear());
    }
    protected virtual void Update() {
        Move();
    }

    protected virtual void Move(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    protected virtual IEnumerator Disappear(){
        yield return Yields.WaitForSeconds(destroyDuration);
        //TODO: 풀링 꺼짐
        Destroy(gameObject);
    }
}
