using UnityEngine;
using static GlobalVariables;

public class Ship : MonoBehaviour
{
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

    public float GetDamage()
    {
        return (float)damage;
    }
}
