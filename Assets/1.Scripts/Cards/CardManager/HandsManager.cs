using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    [SerializeField]
    private List<Card> deck = new List<Card>();
    private List<Card> currentCards = new List<Card>();

    private int fairCount;

    private int[] cardNumbers = { 0, 0, 0, 0, 0 };
    private void FairCheck()
    {
        //4점이면 풀하우스 6점이면 포카드
        for (int i = 0; i < currentCards.Count; i++)
        {

            for (int j = 1; i + j < currentCards.Count; j++)
            {
                if (currentCards[j].cardNumber == currentCards[i + j].cardNumber)
                {
                    fairCount++;
                }
            }
        }
    }

    private bool StraightCheck()
    {
        bool isStraight = false;
        //TODO: 카드 얕은복사 주의
        for (int i = 0; i < currentCards.Count; i++)
        {
            cardNumbers[i] = currentCards[i].cardNumber;
        }
        for (int i = 0; i <= cardNumbers.Length; i++)
        {
            for (int j = 0; j <= cardNumbers.Length - i; j++)
            {
                if (cardNumbers[j] < cardNumbers[j + 1])
                {
                    int t = cardNumbers[j];
                    cardNumbers[j] = cardNumbers[j + 1];
                    cardNumbers[j + 1] = t;

                    // Card.CardPattern p = currentCards[j].cardPattern;
                    // currentCards[j].cardPattern = currentCards[j + 1].cardPattern;
                    // currentCards[j + 1].cardPattern = p;
                }
            }
        }

        if ((cardNumbers[0] - 1) == cardNumbers[1])
        {
            if ((cardNumbers[1] - 1) == cardNumbers[2])
            {
                if ((cardNumbers[2] - 1) == cardNumbers[3])
                {
                    if ((cardNumbers[3] - 1) == cardNumbers[4])
                    {
                        //스트레이트 성공
                        isStraight = true;
                    }
                }
            }
        }
        else if(RoyalCheck()){
            isStraight = true;
        }
        return isStraight;
    }

    private bool FlushCheck()
    {
        bool isFlush = false;
        if (currentCards[0].cardPattern == currentCards[1].cardPattern && currentCards[0].cardPattern == currentCards[2].cardPattern
        && currentCards[0].cardPattern == currentCards[3].cardPattern && currentCards[0].cardPattern == currentCards[4].cardPattern
        && currentCards[0].cardPattern == currentCards[5].cardPattern)
        {
            isFlush = true;
            //플러시 성공
        }
        return isFlush;
    }

    private bool RoyalCheck()
    {
        bool isRoyal = false;
        // int[] cardNumbers = { 0, 0, 0, 0, 0 };
        // for (int i = 0; i < currentCards.Count; i++)
        // {
        //     cardNumbers[i] = currentCards[i].cardNumber;
        // }
        // for (int i = 0; i <= cardNumbers.Length; i++)
        // {
        //     for (int j = 0; j <= cardNumbers.Length - i; j++)
        //     {
        //         if (cardNumbers[j] < cardNumbers[j + 1])
        //         {
        //             int t = cardNumbers[j];
        //             cardNumbers[j] = cardNumbers[j + 1];
        //             cardNumbers[j + 1] = t;
        //         }
        //     }
        // }
        if (cardNumbers[0] == 13 && cardNumbers[1] == 12 && cardNumbers[2] == 11 && cardNumbers[3] == 10 && cardNumbers[4] == 1)
        {
            isRoyal = true;
        }
        return isRoyal;
    }
}
