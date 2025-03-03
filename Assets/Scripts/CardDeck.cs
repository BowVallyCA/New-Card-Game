using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    [SerializeField] private GameObject _parentGameObject;
    [SerializeField] private GameObject _parentEnemyGameObject;
    [SerializeField] private Button _deckButton;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CardDeck _enemyCardDeck;

    public int cardsHave = 3;
    private int startCardCount = 0;
    public int cardsTaken = 0;

    public int cardsHaveEnemy = 3;
    private int startCardCountEnemy = 0;
    public int cardsTakenEnemy = 0;

    private void Start()
    {
        if (gameObject.tag == "EnemyDeck")
        {
            _deckButton.interactable = false;
        }

        if (gameObject.tag != "EnemyDeck")
        {
            while (startCardCount < 3)
            {
                SpawnCard();
                startCardCount++;
            }

            while (startCardCountEnemy < 3)
            {
                System.Random rnd = new System.Random();
                GameObject gameObject = Instantiate(_gameObjects[rnd.Next(0, 7)], _parentEnemyGameObject.transform);
                if (gameObject.tag != "Card")
                {
                    
                }
                else
                {
                    gameObject.tag = "EnemyCard";
                }


                cardsHaveEnemy += 1;
                startCardCountEnemy++;
            }
        }

        _sound.GetComponent<AudioSource>();

        CardController _cardController = FindObjectOfType<CardController>();

        GameObject[] targetGameObject = GameObject.FindGameObjectsWithTag("Card");

        foreach (GameObject card in targetGameObject)
        {
            CardController cardController = card.GetComponentInChildren<CardController>();

            if (cardController.tag == "Card")
            {
                cardController.SetCardDeck(this);
                cardController.SetGameManager(_gameManager);
            }
        }

        GameObject[] targetGameObjectEnemy = GameObject.FindGameObjectsWithTag("EnemyCard");

        foreach (GameObject card in targetGameObjectEnemy)
        {
            CardController cardControllerEnemy = card.GetComponentInChildren<CardController>();

            if (cardControllerEnemy.tag == "EnemyCard")
            {
                cardControllerEnemy.SetCardDeck(_enemyCardDeck);
                cardControllerEnemy.SetGameManager(_gameManager);
            }
        }


    }

    public void SpawnCard()
    {
        _sound.Play();

        if (_gameManager.playerTurn == true)
        {
            if (cardsTaken < 3)
            {
                cardsTaken++;

                if (cardsHave <= 4)
                {
                    System.Random rnd = new System.Random();
                    GameObject gameObject = Instantiate(_gameObjects[rnd.Next(0, 7)], _parentGameObject.transform);

                    if(gameObject.tag != "Card")
                    {
                        
                    }
                    else
                    {
                        gameObject.tag = "Card";
                    }
                    

                    cardsHave += 1;

                    GameObject[] targetGameObject = GameObject.FindGameObjectsWithTag("Card");

                    foreach (GameObject card in targetGameObject)
                    {
                        CardController cardController = card.GetComponentInChildren<CardController>();

                        if (cardController.tag == "Card")
                        {
                            cardController.SetCardDeck(this);
                            cardController.SetGameManager(_gameManager);
                        }
                    }

                }
                else if (cardsHave >= 5)
                {
                }
            }
            else
            {

            }
        }
        else if (_gameManager.playerTurn == false)
        {
            System.Random rnd = new System.Random();
            GameObject gameObject = Instantiate(_gameObjects[rnd.Next(0, 7)], _parentEnemyGameObject.transform);
            if (gameObject.tag != "Card")
            {
                
            }
            else
            {
                gameObject.tag = "EnemyCard";
            }


            GameObject[] targetGameObjectEnemy = GameObject.FindGameObjectsWithTag("EnemyCard");

            foreach (GameObject card in targetGameObjectEnemy)
            {
                CardController cardControllerEnemy = card.GetComponent<CardController>();

                if (cardControllerEnemy.tag == "EnemyCard")
                {
                    cardControllerEnemy.SetCardDeck(_enemyCardDeck);
                    cardControllerEnemy.SetGameManager(_gameManager);
                }
            }
        }



    }

    public void DisableDeck()
    {
        if (gameObject.tag != "EnemyDeck")
        {
            if (_deckButton.interactable == false)
            {
                _deckButton.interactable = true;
            }
            else if (_deckButton.interactable == true)
            {
                _deckButton.interactable = false;
            }
        }

    }
    
}
