using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 10)]
public class Card : ScriptableObject {
    public enum CardPattern{
        Spade,
        Diamond,
        Heart,
        Clover
    }
    public string description;
    public int cardNumber;

    
}
