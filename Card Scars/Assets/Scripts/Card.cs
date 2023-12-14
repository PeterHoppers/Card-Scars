using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardOwner;
    private CardType cardType;
    private int cardScars;
    private int positionInHand;
    private bool isFaceUp = false;
    private bool isSelected = false;
    private Hand hand;

    private SpriteRenderer cardRenderer;

    void Start()
    {
        cardRenderer = GetComponentInChildren<SpriteRenderer>();
        FlipCard(isFaceUp);
    }

    void OnMouseEnter()
    {
        //TODO: When we have multiple players, make sure a player can't hover/interact with their opponent's cards
        if (hand != null)
        {
            transform.localScale = new Vector2(1.1f, 1.1f);
        }
    }

    void OnMouseExit()
    {
        if (hand != null)
        {
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
    }

    void OnMouseDown()
    {
        if (hand != null)
        {
            hand.SetSelectedCard(this, !isSelected);
        }        
    }

    public void SetHand(Hand hand, float offsetFromAnchor)
    {
        this.hand = hand;
        transform.SetParent(hand.transform, false);

        //since our visual is in the child, we need to make sure the values between the two of them respect the offset that we set up
        transform.GetChild(0).localPosition = new Vector3(0, offsetFromAnchor, 0);
        GetComponent<BoxCollider2D>().offset = new Vector2(0, offsetFromAnchor);
        FlipCard(true);
    }

    public void SetSelectState(bool isSelected)
    {
        if (isSelected)
        {            
            transform.localPosition = new Vector2(0, 20f);
        }
        else
        {
            transform.localPosition = new Vector2(0, 0);
        }

        this.isSelected = isSelected;
    }

    void FlipCard(bool isFaceUp)
    {
        if (isFaceUp)
        {
            cardRenderer.sprite = CardArtDictionary.Instance.GetCardImage(cardType);
        }
        else
        {
            cardRenderer.sprite = CardArtDictionary.Instance.GetCardBack();
        }

        this.isFaceUp = isFaceUp;
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
    public int PositionInHand
    { 
        get { return positionInHand; }
        set 
        {
            positionInHand = value;
            cardRenderer.sortingOrder = value;
        }
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
