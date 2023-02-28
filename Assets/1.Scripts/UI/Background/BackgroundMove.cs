using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private MeshRenderer meshRenderer = null;

    private Vector2 offset = Vector2.zero;


    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update() {
        SetOffset();
    }
    private void SetOffset(){
        offset.x += speed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex",offset);
    }
}
