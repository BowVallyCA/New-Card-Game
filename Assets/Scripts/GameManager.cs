using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //private CardController cardController;
    //private GameObject cardObject;

    [SerializeField] public float _playerHealth = 24f;
    [SerializeField] public float _enemyHealth = 24f;
    [SerializeField] TMP_Text _playerHealthText;
    [SerializeField] TMP_Text _enemyHealthText;
    [SerializeField] TMP_Text _playerManaText;
    [SerializeField] TMP_Text _enemyManaText;
    [SerializeField] public float _playerMana = 2f;
    [SerializeField] public float _enemyMana = 2f;
    [SerializeField] TMP_Text _turnButtonText;
    [SerializeField] public GameObject _winScreen;
    [SerializeField] public GameObject _loseScreen;
    [SerializeField] public CardDeck _cardDeck;
    [SerializeField] public AudioSource _sound;
    [SerializeField] private EnemyAiController _enemyAi;
    [SerializeField] public Button _turnButton;
    public bool playerTurn = true;

    private string playerTurnText = "Players Turn";
    private string enemyTurnText = "Enemies Turn";

    private bool Poisioned = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealthText.GetComponent<TMP_Text>();
        _enemyHealthText.GetComponent<TMP_Text>();

        _playerManaText.GetComponent<TMP_Text>();
        _enemyManaText.GetComponent<TMP_Text>();

        _turnButtonText.GetComponent<TMP_Text>();

        _sound.GetComponent<AudioSource>();

        _turnButtonText.SetText("Players Turn");
    }

    // Update is called once per frame
    void Update()
    {
        _playerHealthText.SetText(_playerHealth.ToString());
        _enemyHealthText.SetText(_enemyHealth.ToString());

        _playerManaText.SetText(_playerMana.ToString());
        _enemyManaText.SetText(_enemyMana.ToString());

        if (_enemyHealth <= 0f)
        {
            _winScreen.SetActive(true);
        }
        else if (_playerHealth <= 0f)
        {
            _loseScreen.SetActive(true);
        }
    }

    public void DealDamage(CardData _cardData, GameObject targetGameObject)
    {
        if (targetGameObject.CompareTag("Card"))
        {
            _enemyHealth -= _cardData.dmg;
        }
        else if (targetGameObject.CompareTag("EnemyCard"))
        {
            _playerHealth -= _cardData.dmg;
        }

    }

    public void Heal(CardData _cardData)
    {
        Debug.Log("Healed");

        if(playerTurn == true)
        {
            _playerHealth += _cardData.healAmount;
        }
        else if (playerTurn == false)
        {
            _enemyHealth += _cardData.healAmount;
        }
            
    }

    public void Poision(CardData _cardData)
    {
        if (_cardData.turnsToPoision > 0)
        {
            Debug.Log(_cardData.turnsToPoision);

            Poisioned = true;

            if (playerTurn == true)
            {
                _playerHealth -= _cardData.poisionDmg;
            }
            else if (playerTurn == false)
            {
                _enemyHealth -= _cardData.poisionDmg;
            }


            _cardData.turnsToPoision--;
        }

    }

    public void RemoveCard(CardData _cardData)
    {
        CardController _cardController = FindObjectOfType<CardController>();
        GameObject _cardControllerObject = _cardController.gameObject;

        Destroy(_cardControllerObject);
    }

    public void StallCard(CardData _cardData)
    {
        //if (_cardData.roundStalling > 0)
        //{
            FinishTurn();
            FinishTurn();
        //}
    }

    public void FinishTurn()
    {
        _sound.Play();

        _cardDeck.cardsTaken = 0;
        _cardDeck.cardsTakenEnemy = 0;

        if (GameObject.FindGameObjectsWithTag("Card").Length != 0)
        {
            //foreach (GameObject.FindGameObjectsWithTag("Card").Length != 0)
            //{

            //}
            GameObject[] targetGameObject = GameObject.FindGameObjectsWithTag("Card");

            // Loop through each GameObject with the "Card" tag
            foreach (GameObject card in targetGameObject)
            {
                // Get the CardController component in the current GameObject (or its children)
                CardController cardController = card.GetComponentInChildren<CardController>();

                if (cardController != null)
                {
                    // Call the DisableCards method to disable the card
                    cardController.DisableCards();
                    //_cardDeck.DisableDeck();
                    if (Poisioned == true)
                    {
                        cardController.RepeatPoision();
                    }
                }
            }
        }
        else
        {

        }


        if (_playerMana < 10)
        {
            if (_playerMana == 9)
            {
                _playerMana = 10f;
            }
            else
            {
                _playerMana += 2f;
            }

        }
        else
        {
            _playerMana = 10f;

        }


        if (_enemyMana < 10)
        {
            if (_enemyMana == 9)
            {
                _enemyMana = 10f;
            }
            else
            {
                _enemyMana += 2f;
            }

        }
        else
        {
            _enemyMana = 10f;

        }

        


        if (playerTurn == true)
        {
            _turnButtonText.SetText(enemyTurnText);
            playerTurn = false;
            _enemyAi.AiPlay();
        }
        else if (playerTurn == false)
        {
            _turnButtonText.SetText(playerTurnText);
            playerTurn = true;
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
