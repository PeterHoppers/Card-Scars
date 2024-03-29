using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public HandDisplaySettings handDisplaySettings;
    public int maxHandSize = 5; //Unsure if needed, but keeps the visual from getting a bit weird

    [Header("Selection Settings")]
    public bool doesDoubleSelectPlay = true; //change these settings when we need to select multiple cards at once
    public bool isAutomaticReselect = true;
    public int maxSelectedCards = 1;

    List<Card> _cardsInHand = new List<Card>();
    List<Card> _selectedCards = new List<Card>();
    bool _isShown = false;
    Player _handOwner;

    void Start()
    {
        PositionCardsInHand();
    }

    void PositionCardsInHand()
    { 
        double amountOfCards = _cardsInHand.Count;
        double positionMultiplier = -1 * (amountOfCards - 1) / 2;

        for (int cardIndex = 0; cardIndex < amountOfCards; cardIndex++)
        {
            var card = _cardsInHand[cardIndex];
            card.PositionInCollection = cardIndex;
            float cardRotation = (float)positionMultiplier * handDisplaySettings.rotationPerCard;
            float cardXPosition = (float)positionMultiplier * handDisplaySettings.spacingPerCard;
            card.UpdateRotation(Quaternion.Euler(0, 0, cardRotation));
            card.UpdatePosition(new Vector2(cardXPosition, Mathf.Abs(cardRotation) * -3)); //use the rotation here to keep the top of the cards straight
            card.UpdateScale(Vector3.one);
            positionMultiplier += 1;
        }
    }

    public void SetPlayer(Player player)
    {
        _handOwner = player;
        _handOwner.OnCardPlayed += RemoveCardFromHand;
    }

    public void AddCardToHand(Card card)
    {
        if (_cardsInHand.Count >= maxHandSize)
        {
            return;
        }

        card.SetHand(this, handDisplaySettings.offsetFromAnchor, _isShown);
        _cardsInHand.Add(card);
        PositionCardsInHand();
    }

    public void RemoveCardFromHand(Card card)
    {
        card.RemoveHand();
        _cardsInHand.Remove(card);
        _selectedCards.Remove(card);
        PositionCardsInHand();
    }

    public void DiscardHand()
    {
        foreach (Card card in _cardsInHand)
        {
            RemoveCardFromHand(card);
        }
    }

    public void SetSelectedCard(Card card)
    {
        if (_selectedCards.Contains(card))
        {
            if (doesDoubleSelectPlay)
            {
                _handOwner.PlayCard(card);
            }
            else
            {
                SetCardSelectedState(card, false);
            }
        }
        else
        {
            //if we can add the new card to the list of selected cards
            if (_selectedCards.Count < maxSelectedCards)
            {
                SetCardSelectedState(card, true);
            }
            else
            {
                if (isAutomaticReselect)
                {
                    Card cardToDeselect = _selectedCards.First();
                    SetCardSelectedState(cardToDeselect, false);
                    SetCardSelectedState(card, true);
                }
            }
        }     
    }

    void SetCardSelectedState(Card card, bool isSelected)
    {
        card.SetSelectState(isSelected);

        if (isSelected)
        {
            _selectedCards.Add(card);
        }
        else
        {
            _selectedCards.Remove(card);
        }        
    }

    public void SetActiveState(bool isActive)
    {
        if (isActive == _isShown)
        {
            return;
        }

        _isShown = isActive;

        foreach (var card in _cardsInHand)
        { 
            card.FlipCard(_isShown);
            card.IsInteractable = _isShown;

            if (!_isShown)
            {
                card.SetHoverStyle(false);
            }
        }
    }

    void DeselectAllCards()
    {
        //clone selected cards like this so that we don't modify our foreach loop while we remove from the selected cards list
        var selectedCards = _selectedCards.ToArray();

        foreach (var card in selectedCards)
        {
            SetCardSelectedState(card, false);
        }
    }
}
