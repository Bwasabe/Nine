using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour, ISkillable
{
    [SerializeField]
    private Card myCard;


    public virtual void Skill(){
        
    }

}
