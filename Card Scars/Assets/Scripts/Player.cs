using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerDeck playerDeck;
    public Hand hand;
    public int index;
    public string playerName;

    public delegate void PlayedCard(Card card);
    public PlayedCard OnCardPlayed;

    void Start()
    {
        hand.SetPlayer(this);
    }

    public void DrawCards(int cardAmount)
    {
        for (int index = 0; index < cardAmount; index++)
        {
            var card = playerDeck.DrawCard();
            hand.AddCardToHand(card);
        }            
    }

    //TODO: Use events so that the play cards button is disabled until a card is selected
    public void PlayCard(Card card)
    { 
        if (card != null)
        {
            playerDeck.discardPile.AddCard(card);
            OnCardPlayed?.Invoke(card);
        }        
    }

    public void AddCardToDeck(Card addedCard)
    { 
        playerDeck.drawPile.AddCard(addedCard);
    }

    public void SetActivateState(bool isActive)
    { 
        hand.SetActiveState(isActive);
    }
}
