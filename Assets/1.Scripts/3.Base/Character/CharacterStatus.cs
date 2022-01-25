using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterStatus : MonoBehaviour ,IDamageable
{
    [SerializeField]
    private new string name;

    [SerializeField]
    protected int hp;

    private SpriteRenderer spriteRenderer;
    protected virtual void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void Damage(int damage){
        if(hp < 1){
            Dead();
        }
        else{
            hp--;
            StartCoroutine(DamageMotion());
        }
    }

    private IEnumerator DamageMotion(){
        spriteRenderer.color = Color.red;
        yield return Yields.WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    
    public virtual void Dead(){

    }
}
