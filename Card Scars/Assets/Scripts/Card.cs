using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardOwner;
    private Suit cardSuit;
    private int cardValue;
    private int cardScars;

    public static List<Card> CreateStandardDeck()
    {
        List<Card> standardDeck = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for(int i = 1; i <= 13; i++)
            {
                Card card = new GameObject().AddComponent<Card>();
                card.cardOwner = "Game Master";
                card.CardSuit = suit;
                card.cardValue = i;
                card.cardScars = 0;

                standardDeck.Add(card);
            }
        }

        return standardDeck;
    }

    // Enum for each possible card suit
    public enum Suit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    // Accessors for card variables
    public string CardOwner
    {
        get { return cardOwner; }
        set { cardOwner = value; }
    }
    public Suit CardSuit
    {
        get { return cardSuit; }
        set { cardSuit = value; }
    }
    public int CardValue
    {
        get { return cardValue; }
        set { cardValue = value; }
    }
    public int CardScars
    {
        get { return cardScars; }
        set { cardScars = value; }
    }
}
