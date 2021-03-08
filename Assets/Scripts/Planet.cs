using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Map Reward")]
    [SerializeField] int coin;
    [SerializeField] int diamond;
    [SerializeField] int gold;
    [SerializeField] int silver;
    [SerializeField] int bronze;
    [SerializeField] int brass;
    [SerializeField] int titanium;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // @Access from MapsStatus
    public (int, int, int, int, int, int, int) getData()
    {
        return (coin, diamond, gold, silver, bronze, brass, titanium);
    }
}
