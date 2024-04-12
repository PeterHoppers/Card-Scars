using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cardsInDeck;  
    public float perCardXOffset = 0;
    public float perCardYOffset = 0;
    
    public List<Card> CreateDeck(Card cardType, int maxValueOfCard)
    {
        List<Card> standardDeck = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 1; i <= maxValueOfCard; i++)
            { 
                Card card = Instantiate(cardType, transform.position, Quaternion.identity, transform);
                card.gameObject.name = suit + " " + i.ToString();
                card.CardOwner = "Game Master";
                card.CardType = new CardType()
                {
                    suit = suit,
                    value = i
                };
                card.CardScars = 0;

                standardDeck.Add(card);
            }
        }
        cardsInDeck = standardDeck;
        return standardDeck;
    }

    public void AddCard(Card addedCard, bool changePosition = false, float speed = .25f)
    {
        int indexInDeck = cardsInDeck.Count + 1;
        addedCard.transform.SetParent(transform, changePosition);
        addedCard.UpdatePosition(new Vector2(indexInDeck * perCardXOffset, indexInDeck * perCardYOffset), speed);
        addedCard.PositionInCollection = indexInDeck;
        cardsInDeck.Add(addedCard);
    }

    public Card GetTopCard()
    {
        Card topCard = cardsInDeck[^1];
        cardsInDeck.Remove(topCard);
        return topCard;
    }

    public void ShuffleDeck()
    {
        int i = cardsInDeck.Count;
        while (i > 1)
        {
            i--;
            int k = UnityEngine.Random.Range(0, i + 1);
            Card value = cardsInDeck[k];
            cardsInDeck[k] = cardsInDeck[i];
            cardsInDeck[i] = value;
        }
    }
}
