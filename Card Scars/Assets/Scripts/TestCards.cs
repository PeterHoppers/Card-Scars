using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCards : MonoBehaviour
{
    // Added in to test creating a basic deck to see that each card does hold the right values
    // REMOVE WHEN DONE TESTING
    public static List<Card> standardDeck;

    // Start is called before the first frame update
    void Start()
    {
        standardDeck = Card.CreateStandardDeck();

        foreach (Card card in standardDeck)
        {
            Debug.Log("Card values: (Owner, Suit, Value, Scars) -- " + card.CardOwner + ", " + card.CardSuit + ", " + card.CardValue + ", " + card.CardScars);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
