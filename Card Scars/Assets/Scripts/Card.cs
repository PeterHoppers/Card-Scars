using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardOwner;
    private CardType cardType;
    private int cardScars;
    private bool isFaceUp = true;

    private SpriteRenderer cardRenderer;

    void Start()
    {
        cardRenderer = GetComponent<SpriteRenderer>();
        cardRenderer.sprite = CardArtDictionary.Instance.GetCardImage(cardType);
    }

    public override string ToString()
    {
        return "Card values: (Owner, Suit, Value, Scars) -- " + CardOwner + ", " + CardType.suit + ", " + CardType.value + ", " + CardScars;
    }

    // Accessors for card variables
    public string CardOwner
    {
        get { return cardOwner; }
        set { cardOwner = value; }
    }
    public CardType CardType
    {
        get { return cardType; }
        set { cardType = value; }
    }
    public int CardScars
    {
        get { return cardScars; }
        set { cardScars = value; }
    }
    public bool IsFaceUp
    {
        get { return isFaceUp; }
        set { isFaceUp = value; }
    }

}

// Enum for each possible card suit
public enum Suit
{
    Heart,
    Diamond,
    Club,
    Spade
}

[System.Serializable]
public struct CardType
{
    public Suit suit;
    public int value;
}
