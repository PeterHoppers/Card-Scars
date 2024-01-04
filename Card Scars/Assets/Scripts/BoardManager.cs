using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Handles UI and SFXs when dealing with the current state of the game board
public class BoardManager : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    Animator animator;
    GameManager gameManager;


    public void SetupManager(GameManager gameManager)
    { 
        animator = GetComponent<Animator>();
        this.gameManager = gameManager;

        gameManager.OnPlayerTurnStart += DisplayPlayerTurn;
    }

    void DisplayPlayerTurn(Player player)
    {
        mainText.text = $"{player.playerName}'s Turn";
        animator.Play("Pan Text");
    }

    void OnDisplayPlayerTurnEnd()
    {
        //Right now, this is empty, but we might have some effects play after the text is done
    }
}
