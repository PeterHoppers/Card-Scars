using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDeck playerDeck;
    public Hand hand;

    public void DrawCardFromDeck()
    {
        var card = playerDeck.DrawCard();
        hand.AddCardToHand(card);
    }

    //TODO: Use events so that the play cards button is disabled until a card is selected
    public void PlayCard()
    { 
        var card = hand.PlaySelectedCard();
        if (card != null)
        {
            playerDeck.discardPile.AddCard(card);
        }        
    }

    public void AddCardToDeck(Card addedCard)
    { 
        playerDeck.drawPile.AddCard(addedCard);
    }
}
