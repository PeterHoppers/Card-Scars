using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card cardGameObject;
    public List<Card> standardDeck;
    public List<PlayerDeck> playerDecks;
    public List<Card> CreateDeck(string deckType)
    {
        standardDeck = new List<Card>();
        int gameDeckMaxCardValue;

        if(deckType == "CardScars")
        {
            gameDeckMaxCardValue = 10;
        }
        else
        {
            gameDeckMaxCardValue = 13;
        }

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int i = 1; i <= gameDeckMaxCardValue; i++)
            {
                Card card = Instantiate(cardGameObject, transform.position, Quaternion.identity, transform);
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


    public void ShuffleAndDeal(int numPlayers)
    {
        // Shuffle the deck
        ShuffleDeck();

        CreatePlayerDecks(numPlayers);

        // Deal the cards to players
        DealCardsToPlayerDecks(numPlayers);
    }

    private void ShuffleDeck()
    {
        int i = standardDeck.Count;
        while (i > 1)
        {
            i--;
            int k = UnityEngine.Random.Range(0, i + 1);
            Card value = standardDeck[k];
            standardDeck[k] = standardDeck[i];
            standardDeck[i] = value;
        }
    }

    private void CreatePlayerDecks(int numPlayers)
    {
        playerDecks = new List<PlayerDeck>();

        for (int i = 0; i < numPlayers; i++)
        {
            // Create an empty game object for each player in the game
            GameObject playerDeckObject = new GameObject("Player " + (i + 1) + " Deck");
            playerDeckObject.transform.SetParent(transform);

            // Add PlayerDeck component to the game object
            PlayerDeck playerDeck = playerDeckObject.AddComponent<PlayerDeck>();

            // Initialize lists for the player deck
            playerDeck.cardsInHand = new List<Card>();
            playerDeck.drawPile = new List<Card>();
            playerDeck.discardPile = new List<Card>();

            // Add the player deck to the list
            playerDecks.Add(playerDeck);
        }
    }

    private void DealCardsToPlayerDecks(int numPlayers)
    {
        Transform playerDeckParent1 = transform.Find("Player 1 Deck");
        Transform playerDeckParent2 = transform.Find("Player 2 Deck");

        int currentPlayer = 0;

        foreach (Card card in standardDeck)
        {
            //Determine the parent object for the card
            Transform cardParent = currentPlayer == 0 ? playerDeckParent1 : playerDeckParent2;

            //Set the cards new parent object
            card.transform.SetParent(cardParent);

            //Assign card to the player deck and update the Card Owner
            
            playerDecks[currentPlayer].drawPile.Add(card);
            card.CardOwner = "Player " + (currentPlayer + 1);

            // Move to the next player in a round-robin fashion
            currentPlayer = (currentPlayer + 1) % numPlayers;
        }
    }
}
