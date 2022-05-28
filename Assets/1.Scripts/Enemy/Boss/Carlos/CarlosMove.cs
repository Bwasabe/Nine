using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarlosMove : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _moveTransforms = new List<Transform>();

    public void Move(){
        int randomPos = Random.Range(1, _moveTransforms.Count);

        transform.position = _moveTransforms[randomPos].position;
    }
}
