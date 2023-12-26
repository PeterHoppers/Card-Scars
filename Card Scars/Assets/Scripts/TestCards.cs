using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCards : MonoBehaviour
{
    // Added in to test creating a basic deck to see that each card does hold the right values
    // REMOVE WHEN DONE TESTING
    public Deck deck;
    public const int CARDS_SCARS_MAX_VALUE = 10;
    private List<Card> standardDeck;


    // Start is called before the first frame update
    void Start()
    {
        /*standardDeck = deck.CreateDeck(CARDS_SCARS_MAX_VALUE);

        deck.ShuffleAndDeal(2);

        deck.playerDecks[0].DrawCard();

        deck.playerDecks[0].DiscardTopCardOfDeck();
        deck.playerDecks[0].DiscardTopCardOfDeck();
        deck.playerDecks[0].DiscardTopCardOfDeck();

        deck.playerDecks[0].GetDiscardPileTopCard();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
