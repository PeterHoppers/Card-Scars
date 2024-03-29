using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AYellowpaper.SerializedCollections;

public class GameManager : MonoBehaviour
{     
    public GameObject gameBoard;
    public Card cardGameObject;
    [SerializedDictionary("Player Object", "Player Settings")]
    public SerializedDictionary<Player, PlayerSettings> PlayerSetup;

    public BoardManager boardManager; //TODO: Remove direct reference and create an interface

    Deck playableCards;
    List<Player> players = new List<Player>();
    Player activePlayer;
    int activePlayerIndex = 0;

    public const int CARDS_SCARS_MAX_VALUE = 11;
    public const int CARDS_SCARS_PLAYER_COUNT = 2;
    public const int CARDS_SCARS_HAND_AMOUNT = 5;

    public delegate void PlayerTurnStart(Player player);
    public delegate void PlayerTurnEnd(Player player);

    public PlayerTurnStart OnPlayerTurnStart;
    public PlayerTurnEnd OnPlayerTurnEnd;

    IEnumerator Start()
    {
        playableCards = CreateMainDeck();
        players = CreatePlayers(PlayerSetup.Count);
        SplitDeckToPlayers(players.Count);
        SetStartingHands(CARDS_SCARS_HAND_AMOUNT);
        boardManager.SetupManager(this);

        yield return new WaitForSeconds(1.5f); //this will get replaced with showing an animation of some cards getting marked at the start of the game
        StartPlayerTurn(players[activePlayerIndex]);
    }

    private void StartPlayerTurn(Player player)
    {
        OnPlayerTurnStart?.Invoke(player);        
        activePlayer = player;
        activePlayer.SetActivateState(true);
        activePlayerIndex = player.index;
        activePlayer.DrawCards(1);

        //TODO: Have another script handle converting when a card is played to ending the turn
        activePlayer.OnCardPlayed += EndPlayerTurn;
    }

    private void EndPlayerTurn(Card card)
    {
        activePlayer.OnCardPlayed -= EndPlayerTurn;
        StartCoroutine(AdvanceTurn(card));       
    }

    //TODO: Rather than advancing the turn after a short bit, have this code wait until something has been completed, depending on the card
    IEnumerator AdvanceTurn(Card card)
    {
        card.UpdatePosition(Vector2.zero, .25f, false);
        yield return new WaitForSeconds(.5f);
        activePlayer.AddCardToDiscard(card);
        OnPlayerTurnEnd?.Invoke(activePlayer);
        activePlayer.SetActivateState(false);
        StartPlayerTurn(GetNextPlayer());
    }

    private Player GetNextPlayer()
    {
        //Consider switching to a linked list of make this simpler? Althought with only two players, that might be overkill
        int nextIndex = activePlayerIndex + 1;
        if (nextIndex >= players.Count)
        {
            return players[0];
        }
        else
        {
            return players[nextIndex];
        }        
    }

    private Deck CreateMainDeck()
    {
        var deck = gameObject.AddComponent<Deck>();
        deck.CreateDeck(cardGameObject, CARDS_SCARS_MAX_VALUE);
        return deck;
    }

    private List<Player> CreatePlayers(int numPlayers)
    {
        var playerList = new List<Player>();
        for (int i = 0; i < numPlayers; i++)
        {
            // Create an copy of the player game object for each player in the game
            //GameObject playerObject = Instantiate(gamePlayer.gameObject, gameBoard.transform);
            GameObject playerObject = PlayerSetup.ElementAt(i).Key.gameObject;

            // Add the player to the list
            var newPlayer = playerObject.GetComponent<Player>();
            newPlayer.index = i;
            newPlayer.playerName = PlayerSetup.ElementAt(i).Value.playerName;
            newPlayer.name = newPlayer.playerName;
            playerObject.name = newPlayer.playerName + "'s Deck";
            playerList.Add(newPlayer);
        }

        return playerList;
    }
    private void DealCardsToPlayerDecks(int numPlayers)
    {
        int currentPlayer = 0;

        foreach (Card card in playableCards.cardsInDeck)
        {
            //Assign card to the player deck and update the Card Owner
            players[currentPlayer].AddCardToDeck(card);
            card.CardOwner = players[currentPlayer].playerName;

            // Move to the next player in a round-robin fashion
            currentPlayer = (currentPlayer + 1) % numPlayers;
        }
    }

    private void SplitDeckToPlayers(int numPlayers)
    {
        int cardsPerPlayer = playableCards.cardsInDeck.Count / numPlayers;

        for (int playerIndex = 0; playerIndex < players.Count; playerIndex++)
        { 
            Player player = players[playerIndex];
            int startIndex = playerIndex * cardsPerPlayer;
            int endIndex = startIndex + cardsPerPlayer;

            var cardsToAdd = playableCards.cardsInDeck.Skip(startIndex).Take(endIndex - startIndex);

            foreach (Card card in cardsToAdd)
            {
                //Assign card to the player deck and update the Card Owner
                player.AddCardToDeck(card);
                card.CardOwner = player.playerName;
            }

            player.ShuffleCards();
        }        
    }

    private void SetStartingHands(int cardAmount)
    {
        foreach (Player player in players)
        { 
            player.DrawCards(cardAmount);

            if (player.index == activePlayerIndex)
            {
                player.SetActivateState(true);
            }
            else
            {
                player.SetActivateState(false);
            }
        }
    }
}
