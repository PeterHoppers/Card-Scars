using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> cardsInHand;
    public List<Card> drawPile;
    public List<Card> discardPile;

    public void DrawCard()
    {
        if (drawPile.Count <= 0)
        {
            ShufflePiles();
        }

        //Draw the Top Card
        Card drawnCard = drawPile[0];
        drawPile.Remove(drawnCard);
        cardsInHand.Add(drawnCard);
        Debug.Log("Player Drew 1 Card with values of -- " + cardsInHand[cardsInHand.Count-1]);
    }

    public void GetDiscardPileTopCard()
    {
        Debug.Log("Top Card in the " + discardPile.Count + " card Discard Pile is -- " + discardPile[discardPile.Count-1]);
    }

    public void ShufflePiles()
    {
        foreach(Card card in discardPile)
        {
            Card movedCard = card;
            discardPile.Remove(card);
            drawPile.Add(card);
        }

        int i = drawPile.Count;
        while (i > 1)
        {
            i--;
            int k = UnityEngine.Random.Range(0, i + 1);
            Card value = drawPile[k];
            drawPile[k] = drawPile[i];
            drawPile[i] = value;
        }
    }

    public void DiscardTopCardOfDeck()
    {
        if(drawPile.Count <= 0)
        {
            ShufflePiles();
        }

        Card drawnCard = drawPile[0];
        discardPile.Add(drawnCard);
        drawPile.Remove(drawnCard);

        Debug.Log("Top card of Draw Pile added to the Discard -- " + discardPile[discardPile.Count-1]);
    }
}
