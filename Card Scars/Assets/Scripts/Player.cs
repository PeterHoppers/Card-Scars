using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Deck deck;
    public Hand hand;

    public void DrawCardFromDeck()
    {
        var card = deck.DrawTopCard();
        hand.AddCardToHand(card);
    }

    //TODO: Use events so that the play cards button is disabled until a card is selected
    public void PlayCard()
    { 
        var card = hand.PlaySelectedCard();
        if (card != null)
        {
            //TODO: Add the card here to the discard pile
            card.gameObject.SetActive(false);
        }        
    }
}
