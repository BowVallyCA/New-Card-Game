using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CardDataSO", fileName = "CardDataSO")]

public class CardData : ScriptableObject
{
    public float health;
    public float dmg;
    public float cost;
    public string nameUI;
    public string descriptionUI;

    [Header("Healing Card")]
    public float healAmount;

    [Header("Poision Card")]
    public float poisionDmg;
    public int turnsToPoision;

    [Header("Draw Card")]
    [SerializeField] public GameObject[] _gameObjects;

    [Header("Stall Card")]
    public int roundStalling;

    public enum BelongTo
    {
        Player,
        Enemy,
    }


    
}
