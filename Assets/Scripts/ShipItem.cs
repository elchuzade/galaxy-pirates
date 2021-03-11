using UnityEngine;

public class ShipItem : MonoBehaviour
{
    [Header("Ship Cost")]
    [SerializeField] int gold;
    [SerializeField] int aluminum;
    [SerializeField] int copper;
    [SerializeField] int brass;
    [SerializeField] int titanium;
    [SerializeField] int power;

    // @access from ShopStatus
    public (int, int, int, int, int, int) GetData()
    {
        return (gold, aluminum, copper, brass, titanium, power);
    }
}
