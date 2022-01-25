using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float destroyDuration;


    private void OnEnable() {
        StartCoroutine(Disappear());
    }
    private void Update() {
        Move();
    }

    private void Move(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private IEnumerator Disappear(){
        yield return Yields.WaitForSeconds(destroyDuration);
        //TODO: 풀링 꺼짐
        Destroy(gameObject);
    }
}
