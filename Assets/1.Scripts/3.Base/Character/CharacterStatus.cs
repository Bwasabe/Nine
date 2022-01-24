using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBase))]
public class CharacterStatus : MonoBehaviour ,IDamageable
{
    [SerializeField]
    private new string name;

    [SerializeField]
    protected int hp;

    protected CharacterBase characterBase;

    protected virtual void Start() {
        characterBase = GetComponent<CharacterBase>();
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
        characterBase.spriteRenderer.color = Color.red;
        yield return Yields.WaitForSeconds(0.1f);
        characterBase.spriteRenderer.color = Color.white;
    }
    
    public virtual void Dead(){

    }
}
