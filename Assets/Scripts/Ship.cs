using UnityEngine;
using static GlobalVariables;

public class Ship : MonoBehaviour
{
    [SerializeField] ShipName shipName;
    [SerializeField] float damage;

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
        return damage / 2;
    }

    public float GetDamage()
    {
        return damage;
    }
}
