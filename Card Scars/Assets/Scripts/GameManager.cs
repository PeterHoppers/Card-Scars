using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{     
    public GameObject gameBoard;
    public Player gamePlayer;
    public Card cardGameObject;

    Deck playableCards;
    List<Player> players = new List<Player>();

    public const int CARDS_SCARS_MAX_VALUE = 10;

    void Start()
    {
        ShuffleAndDeal(2);
    }

    public void ShuffleAndDeal(int numPlayers)
    {
        playableCards = gameObject.AddComponent<Deck>();
        playableCards.CreateDeck(cardGameObject, CARDS_SCARS_MAX_VALUE);
        // Shuffle the deck
        playableCards.ShuffleDeck();

        CreatePlayerDecks(numPlayers);

        // Deal the cards to players
        DealCardsToPlayerDecks(numPlayers);
    }

    private void CreatePlayerDecks(int numPlayers)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            // Create an copy of the player game object for each player in the game
            GameObject playerObject = Instantiate(gamePlayer.gameObject, gameBoard.transform);
            playerObject.name = "Player " + (i + 1) + " Deck";

            // Add the player to the list
            players.Add(playerObject.GetComponent<Player>());
        }
    }

    private void DealCardsToPlayerDecks(int numPlayers)
    {
        int currentPlayer = 0;

        foreach (Card card in playableCards.cardsInDeck)
        {
            //Assign card to the player deck and update the Card Owner
            players[currentPlayer].AddCardToDeck(card);
            card.CardOwner = "Player " + (currentPlayer + 1);

            // Move to the next player in a round-robin fashion
            currentPlayer = (currentPlayer + 1) % numPlayers;
        }
    }
}
