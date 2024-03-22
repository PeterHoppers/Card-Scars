using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class Card : MonoBehaviour
{
    private string cardOwner;
    private CardType cardType;
    private int cardScars;
    private int positionInCollection;
    private bool isFaceUp = false;
    private bool isSelected = false;
    private bool isInteractable = true;
    private Hand hand;

    private SpriteRenderer cardRenderer;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        cardRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        FlipCard(isFaceUp);
    }

    void OnMouseEnter()
    {
        //TODO: When we have multiple players, make sure a player can't hover/interact with their opponent's cards
        if (hand != null && isInteractable)
        {
            SetHoverStyle(true);
        }
    }

    void OnMouseExit()
    {
        if (hand != null && isInteractable)
        {
            SetHoverStyle(false);
        }
    }

    void OnMouseDown()
    {
        if (hand != null && isInteractable)
        {
            hand.SetSelectedCard(this);
        }        
    }

    public void SetHand(Hand hand, float offsetFromAnchor, bool isShown)
    {
        this.hand = hand;
        transform.SetParent(hand.transform, false);

        //since our visual is in the child, we need to make sure the values between the two of them respect the offset that we set up
        UpdateVisualOffset(offsetFromAnchor);
        FlipCard(isShown);
        IsInteractable = isShown;
    }

    public void RemoveHand()
    {
        SetSelectState(false);
        this.hand = null;
        UpdateVisualOffset(0);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void UpdateVisualOffset(float offsetFromAnchor)
    {
        transform.GetChild(0).localPosition = new Vector3(0, offsetFromAnchor, 0);
        boxCollider.offset = new Vector2(0, offsetFromAnchor);
    }

    void SetHoverStyle(bool isHovered)
    {
        if (isHovered)
        {
            transform.localScale = new Vector2(1.1f, 1.1f);
        }
        else
        {
            transform.localScale = new Vector2(1.0f, 1.0f);
        }
    }

    public void SetSelectState(bool isSelected)
    {
        if (isSelected)
        {            
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition .y + 20f);
        }
        else
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 20f);
        }

        this.isSelected = isSelected;
    }

    public void FlipCard(bool isFaceUp)
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
        return $"(Owner: {CardOwner}, Suit: {CardType.suit}, Value: {CardType.value}, Scars: {CardScars})";
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
    public int PositionInCollection
    { 
        get { return positionInCollection; }
        set 
        {
            positionInCollection = value;
            cardRenderer.sortingOrder = value;
        }
    }
    public bool IsFaceUp
    {
        get { return isFaceUp; }
        set { isFaceUp = value; }
    }

    public bool IsInteractable
    { 
        get { return isInteractable; }
        set { isInteractable = value; }
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
