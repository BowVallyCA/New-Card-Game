using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CardDeck _enemyDeck;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_enemyDeck.cardsHave);
        //Debug.Log(_enemyDeck.cardsHaveEnemy);
    }

    public void AiPlay()
    {
        GameObject[] targetGameObject = GameObject.FindGameObjectsWithTag("EnemyCard");

        foreach (GameObject card in targetGameObject)
        {
            CardController cardController = card.GetComponentInChildren<CardController>();

            cardController.Clicked();
        }

        //GameObject[] targetGameObjectDraw = GameObject.FindGameObjectsWithTag("SpecialCardDraw");

        //foreach (GameObject card in targetGameObjectDraw)
        //{
        //    CardController cardController = card.GetComponentInChildren<CardController>();

        //    cardController.Clicked();
        //}

        //GameObject[] targetGameObjectHeal = GameObject.FindGameObjectsWithTag("SpecialCardHeal");

        //foreach (GameObject card in targetGameObjectHeal)
        //{
        //    CardController cardController = card.GetComponentInChildren<CardController>();

        //    cardController.Clicked();
        //}

        //GameObject[] targetGameObjectSpy = GameObject.FindGameObjectsWithTag("SpecialCardSpy");

        //foreach (GameObject card in targetGameObjectSpy)
        //{
        //    CardController cardController = card.GetComponentInChildren<CardController>();

        //    cardController.Clicked();
        //}

        //GameObject[] targetGameObjectStall = GameObject.FindGameObjectsWithTag("SpecialCardStall");

        //foreach (GameObject card in targetGameObjectStall)
        //{
        //    CardController cardController = card.GetComponentInChildren<CardController>();

        //    cardController.Clicked();
        //}

        //GameObject[] targetGameObjectPosion = GameObject.FindGameObjectsWithTag("SpecialCardPoision");

        //foreach (GameObject card in targetGameObjectPosion)
        //{
        //    CardController cardController = card.GetComponentInChildren<CardController>();

        //    cardController.Clicked();
        //}

        _enemyDeck.SpawnCard();

            _gameManager.FinishTurn();
    }
}
