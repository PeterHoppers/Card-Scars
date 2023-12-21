using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card cardGameObject;
    public Stack<Card> cardsInDeck;

    void Start()
    {
        cardsInDeck = CreateStandardDeck();
    }

    public Stack<Card> CreateStandardDeck()
    {
        return CreateDeck(13);
    }

    Stack<Card> CreateDeck(int maxValueOfCard)
    {
        Stack<Card> standardDeck = new Stack<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 1; i <= maxValueOfCard; i++)
            {
                Card card = Instantiate(cardGameObject, transform);
                card.gameObject.name = suit + " " + i.ToString();
                card.CardOwner = "Game Master";
                card.CardType = new CardType()
                {
                    suit = suit,
                    value = i
                };
                card.CardScars = 0;

                standardDeck.Push(card);
            }
        }

        return standardDeck;
    }

    public Card DrawTopCard()
    {
        return cardsInDeck.Pop();
    }
}
