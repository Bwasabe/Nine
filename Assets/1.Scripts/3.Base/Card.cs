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

    public Card CardDeepCopy()
    {
        Card newCopy = new Card();
        newCopy.cardNumber = this.cardNumber;
        newCopy.cardPattern = this.cardPattern;
        newCopy.cardSprite = this.cardSprite;
        newCopy.isJoker = this.isJoker;
        return newCopy;
    }

}
