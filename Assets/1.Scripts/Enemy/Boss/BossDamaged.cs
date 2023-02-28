using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BossDamaged : EnemyDamaged
{
    [SerializeField]
    private Slider _hpBar = null;
    [SerializeField]
    private float _duration = 5f;

    // [SerializeField]
    // private List<UnityEvent> _deadEvents = new List<UnityEvent>();

    [SerializeField]    //TODO: private
    private int _currentPatton = 1;
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

    protected override void Dead()
    {
        base.Dead();
        //_deadEvents[_currentPatton-1]?.Invoke();
        //Debug.Break();
    }

}
