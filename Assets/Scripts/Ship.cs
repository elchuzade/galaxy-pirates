﻿using UnityEngine;

public class Ship : MonoBehaviour
{
    public enum ShipName { ImperialFreighter, DeathStar, MillenniumFalcon, ImperialStarDestroyer, StingerMantis, RazorCrest };
    [SerializeField] ShipName shipName;
    [SerializeField] int damage;

    Weapon weapon;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public float GetWidthFromDamage()
    {
        return damage / 10;
    }
}
