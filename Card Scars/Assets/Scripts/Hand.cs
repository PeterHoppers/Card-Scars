using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float rotationPerCard = 20f;
    public int offsetFromAnchor = 200;
    public int maxHandSize = 5; //Unsure if needed, but keeps the visual from getting a bit weird

    List<Card> _cards = new List<Card>();
    Card _selectedCard;

    void Start()
    {
        PositionCardsInHand();
    }

    void PositionCardsInHand()
    { 
        double amountOfCards = _cards.Count;
        double positionMultiplier = -1 * (amountOfCards - 1) / 2;

        for (int cardIndex = 0; cardIndex < amountOfCards; cardIndex++)
        {
            var card = _cards[cardIndex];
            card.PositionInHand = cardIndex;
            float cardRotation = (float)positionMultiplier * rotationPerCard;
            card.transform.rotation = Quaternion.Euler(0, 0, cardRotation);            
            positionMultiplier += 1;
        }
    }

    public void AddCardToHand(Card card)
    {
        if (_cards.Count >= maxHandSize)
        {
            return;
        }

        card.SetHand(this, offsetFromAnchor);
        _cards.Add(card);
        PositionCardsInHand();
    }

    public Card PlaySelectedCard()
    {
        if (_selectedCard == null)
        {
            return null;
        }

        var playedCard = _selectedCard;
        _cards.Remove(playedCard);
        SetSelectedCard(playedCard, false);
        PositionCardsInHand();

        return playedCard;
    }

    public void SetSelectedCard(Card card, bool isBeingSelected)
    {
        if (_selectedCard != null)
        {
            _selectedCard.SetSelectState(false);
        }

        if (!isBeingSelected)
        {
            _selectedCard = null;
        }
        else
        {
            card.SetSelectState(true);
            _selectedCard = card;
        }        
    }
}
