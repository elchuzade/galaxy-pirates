using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItem : MonoBehaviour
{
    [Header("Map Reward")]
    [SerializeField] int coin;
    [SerializeField] int diamond;
    [SerializeField] int gold;
    [SerializeField] int silver;
    [SerializeField] int bronze;
    [SerializeField] int brass;
    [SerializeField] int titanium;

    void Start()
    {

    }

    void Update()
    {

    }

    // @Access from MapsStatus
    public (int, int, int, int, int, int, int) getData()
    {
        return (coin, diamond, gold, silver, bronze, brass, titanium);
    }
}
