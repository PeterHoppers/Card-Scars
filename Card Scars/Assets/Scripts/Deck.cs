using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card cardGameObject;
    public List<Card> CreateStandardDeck()
    {
        List<Card> standardDeck = new List<Card>();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 1; i <= 13; i++)
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

                standardDeck.Add(card);
            }
        }

        return standardDeck;
    }
}
