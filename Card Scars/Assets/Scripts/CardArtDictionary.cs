using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using AYellowpaper.SerializedCollections.KeysGenerators;
using System;

public class CardArtDictionary : MonoBehaviour
{
    public Sprite cardBack;
    [SerializedDictionary("Card Type", "Card Art")]
    public SerializedDictionary<CardType, Sprite> CardArts;

    public static CardArtDictionary Instance;

    private void Awake()
    {
        Instance = this;
    }

    //Create a method that returns a sprite based upon some parameters
    public Sprite GetCardImage(CardType cardType)
    { 
        return CardArts[cardType];
    }
}

//Pulled and modified from the ReadMe found in the 'Serialized Collections" plugin in
[KeyListGenerator("Card Type Range", typeof(CardType))]
public class CardTypeRangeGenerator : KeyListGenerator
{
    [SerializeField]
    private int _startValue = 1;

    [SerializeField]
    private int _endValue = 10;

    public override IEnumerable GetKeys(Type type)
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int value = _startValue; value <= _endValue; value++)
            {
                yield return new CardType()
                {
                    suit = suit,
                    value = value
                };
            }
        }
    }
}
