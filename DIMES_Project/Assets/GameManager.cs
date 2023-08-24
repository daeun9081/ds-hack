using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<Card> allCards;
    private Card flippedCard;
    private bool isFlipping = false;
    private int matchesFound = 0;
    private int totalMatches = 10;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Board board = FindObjectOfType<Board>();
        allCards = board.GetCards();

        StartCoroutine("FlipAllCardsRoutine");
    }

    IEnumerator FlipAllCardsRoutine()
    {
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(3f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;
    }

    void FlipAllCards()
    {
        foreach (Card card in allCards)
        {
            card.FlipCard();
        }
    }

    public void CardClicked(Card card)
    {
        if (isFlipping)
        {
            return;
        }
        card.FlipCard();

        if (flippedCard == null)
        {
            flippedCard = card;
        }
        else
        {
            StartCoroutine(CheckMatchRoutin(flippedCard, card));
        }
    }

    IEnumerator CheckMatchRoutin(Card card1, Card card2)
    {
        isFlipping = true;

        if (card1.cardID == card2.cardID)
        {
            card1.setMatched();
            card2.setMatched();

            matchesFound++;

            if (matchesFound == totalMatches)
            {
                GameOver();
            }
        }
        else
        {

            yield return new WaitForSeconds(1f);

            card1.FlipCard();
            card2.FlipCard();

            yield return new WaitForSeconds(0.4f);
        }
        isFlipping = false;
        flippedCard = null;
    }

    void GameOver()
    {
        Debug.Log("Game Success");
    }
}