using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 10)]
public class Card : ScriptableObject {
    public enum CardPattern{
        Heart,
        Diamond,
        Clover,
        Spade,
    }
    public CardPattern cardPattern;
    public int cardNumber;
    public Sprite cardSprite;
    public bool isJoker = false;

}
