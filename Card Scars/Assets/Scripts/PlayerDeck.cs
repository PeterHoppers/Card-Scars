using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public Deck drawPile;
    public Deck discardPile;

    public Card DrawCard()
    {
        var drawnCard = drawPile.GetTopCard();

        if (drawPile.cardsInDeck.Count <= 0)
        {
            ShufflePiles();
        }

        return drawnCard;
    }

    public void GetDiscardPileTopCard()
    {
        Debug.Log("Top Card in the " + discardPile.cardsInDeck.Count + " card Discard Pile is -- " + discardPile.cardsInDeck[^1]);
    }

    public void ShufflePiles()
    {
        int cardInDiscardPile = discardPile.cardsInDeck.Count;

        for (int index = 0; index < cardInDiscardPile; index++)
        {
            var topCard = discardPile.GetTopCard();
            topCard.FlipCard(false);
            drawPile.AddCard(topCard);
        }

        drawPile.ShuffleDeck();        
    }
}
