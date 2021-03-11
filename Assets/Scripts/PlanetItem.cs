using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetItem : MonoBehaviour
{
    [Header("Map Reward")]
    [SerializeField] int coin;
    [SerializeField] int diamond;
    [SerializeField] int gold;
    [SerializeField] int aluminum;
    [SerializeField] int copper;
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
        return (coin, diamond, gold, aluminum, copper, brass, titanium);
    }
}
