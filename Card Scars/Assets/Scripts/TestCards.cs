using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCards : MonoBehaviour
{
    // Added in to test creating a basic deck to see that each card does hold the right values
    // REMOVE WHEN DONE TESTING
    public Deck deck;
    private List<Card> standardDeck;

    // Start is called before the first frame update
    void Start()
    {
        standardDeck = deck.CreateStandardDeck();

        foreach (Card card in standardDeck)
        {
            Debug.Log(card);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
