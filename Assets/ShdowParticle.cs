using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShdowParticle : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private ParticleSystem myParticleSystem;
    void Start(){
        myParticleSystem = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        myParticleSystem.textureSheetAnimation.SetSprite(0, spriteRenderer.sprite);
    }
}
