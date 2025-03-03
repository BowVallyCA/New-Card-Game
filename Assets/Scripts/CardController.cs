using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class CardController : MonoBehaviour
{
    [SerializeField] private CardData _cardData;
    [SerializeField] public TMP_Text _nameUI, _descriptionUI, _costUI, _hpUI, _dmgUI;
    [SerializeField] public Button _clickButton;
    [SerializeField] public AudioSource _sound;
    [SerializeField] private float timer = 1f;

    private bool playerTurn = true;
    private bool startTimer = false;
    private CardDeck _cardDeck;
    private GameManager _gameManager;

    CardData.BelongTo cardBelongsTo;

    //private enum 

    //private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _sound.GetComponent<AudioSource>();

        _nameUI.text = _cardData.nameUI;
        _descriptionUI.text = _cardData.descriptionUI;
        _costUI.text = "Cost:"+_cardData.cost;
        _hpUI.text = "Hp:" + _cardData.health;
        _dmgUI.text = "Dmg:" + _cardData.dmg;

        if(gameObject.tag == "EnemyCard")
        {
            _clickButton.interactable = false;
        }

        if (cardBelongsTo == CardData.BelongTo.Enemy)
        {
            _clickButton.interactable = false;
        }


    }

    private void Update()
    {
        if (startTimer == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Destroy();
            }
        }
    }

    private void Awake()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();

        if (_gameManager.playerTurn == true && gameObject.tag != "Card")
        {
            cardBelongsTo = CardData.BelongTo.Player;
            Debug.Log(cardBelongsTo);
        }
        else if (_gameManager.playerTurn == false && gameObject.tag != "Card")
        {
            cardBelongsTo = CardData.BelongTo.Enemy;
            Debug.Log(cardBelongsTo);
            _clickButton.interactable = false;
        }

        
    }

    public void SetCardDeck(CardDeck cardDeck)
    {
        _cardDeck = cardDeck;
        //GameObject _cardDeckObject = cardDeck.gameObject;
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Clicked()
    {
        _sound.Play();

        GameManager _GameManager = FindObjectOfType<GameManager>();

        if (gameObject.tag == "SpecialCardPosion")
        {
            _cardData.turnsToPoision = 3;
            _GameManager.Poision(_cardData);

            if (gameObject.tag == "Card")
            {
                _cardDeck.cardsHave -= 1;
            }
        }

        else if (gameObject.tag == "Card")
        {
            if (_gameManager.playerTurn == true && _gameManager._playerMana >= _cardData.cost)
            {
                _gameManager._playerMana -= _cardData.cost;
                _gameManager.DealDamage(_cardData, gameObject);

                if (gameObject.tag == "Card")
                {
                    _cardDeck.cardsHave -= 1;
                }

                startTimer = true;
            }
        }

        else if (gameObject.tag == "EnemyCard")
        {
            if (_gameManager.playerTurn == false && _gameManager._enemyMana >= _cardData.cost)
            {
                if (_gameManager._enemyMana >= _cardData.cost)
                {
                    _gameManager._enemyMana -= _cardData.cost;
                    _gameManager.DealDamage(_cardData, gameObject);

                    if (gameObject.tag == "EnemyCard")
                    {
                        _cardDeck.cardsHaveEnemy -= 1;
                    }

                    startTimer = true;
                }
            }
        }


        else if (gameObject.tag == "SpecialCardHeal")
        {
            Debug.Log("GetHealedNerd");

            _GameManager.Heal(_cardData);
            startTimer = true;

            

            if (gameObject.tag == "Card")
            {
                _cardDeck.cardsHave -= 1;
            }
        }

        else if (gameObject.tag == "SpecialCardSpy")
        {
            _GameManager.RemoveCard(_cardData);
            startTimer = true;

            if (gameObject.tag == "Card")
            {
                _cardDeck.cardsHave -= 1;
            }
        }

        else if (gameObject.tag == "SpecialCardStall")
        {
            _GameManager.StallCard(_cardData);
            startTimer = true;

            if (gameObject.tag == "Card")
            {
                _cardDeck.cardsHave -= 1;
            }
        }

        else if (gameObject.tag == "SpecialCardDraw")
        {
            GameObject parentGameObject = GameObject.Find("PlayerHand");
            GameObject parentGameObjectEnemy = GameObject.Find("EnemyHand");


            System.Random rnd = new System.Random();

            if (playerTurn == true)
            {
                GameObject gameObject = Instantiate(_cardData._gameObjects[rnd.Next(0, 7)], parentGameObject.transform);
                gameObject.tag = "Card";
            }
            else if (playerTurn == false)
            {
                GameObject gameObject = Instantiate(_cardData._gameObjects[rnd.Next(0, 7)], parentGameObjectEnemy.transform);
                gameObject.tag = "EnemyCard";
            }

            startTimer = true;

            if (gameObject.tag == "Card")
            {
                _cardDeck.cardsHave -= 1;
            }
        }

    }  

    public void DisableCards()
    {
        CardDeck _cardDeck = FindObjectOfType<CardDeck>();
        GameObject _cardDeckObject = _cardDeck.gameObject;

        if (_clickButton.interactable == false)
        {
            _clickButton.interactable = true;
            _cardDeck.DisableDeck();
        }
        else if(_clickButton.interactable == true)
        {
            _clickButton.interactable = false;
            _cardDeck.DisableDeck();
        }

    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void RepeatPoision()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();

        _gameManager.Poision(_cardData);
    }
}
