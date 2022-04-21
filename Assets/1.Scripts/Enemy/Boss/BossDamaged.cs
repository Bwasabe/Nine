using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDamaged : EnemyDamaged
{
    [SerializeField]
    private Slider _hpBar = null;
    [SerializeField]
    private float _duration = 5f;


    public IEnumerator BossHpBarFill(){
        _hpBar.maxValue = hp;
        float time = _duration / _hpBar.maxValue;
        for (int i = 0; i < _hpBar.maxValue; i++){
            _hpBar.value++;
            yield return WaitForSeconds(time);
        }
    }

    public override void Damage(int damage){
        base.Damage(damage);

        _hpBar.value-= damage;
    }
}
