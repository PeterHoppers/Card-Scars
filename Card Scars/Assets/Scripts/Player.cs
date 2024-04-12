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
        StartCoroutine(DrawCards(cardAmount, .25f));
    }

    IEnumerator DrawCards(int cardAmount, float delayPerCard)
    {
        for (int index = 0; index < cardAmount; index++)
        {
            var card = playerDeck.DrawCard();
            hand.AddCardToHand(card);
            yield return new WaitForSeconds(delayPerCard);
        }
    }

    public void PlayCard(Card card)
    { 
        if (card != null)
        {            
            OnCardPlayed?.Invoke(card);
        }        
    }

    public void AddCardToDeck(Card addedCard)
    { 
        playerDeck.drawPile.AddCard(addedCard);
    }

    public void AddCardToDiscard(Card discardedCard) 
    {
        playerDeck.discardPile.AddCard(discardedCard, true, .25f); //TODO: remove magic number like this
    }

    public void SetActivateState(bool isActive)
    { 
        hand.SetActiveState(isActive);
    }

    public void ShuffleCards()
    {
        hand.DiscardHand();
        playerDeck.ShufflePiles();
    }
}
