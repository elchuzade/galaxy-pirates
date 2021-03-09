using UnityEngine;

public class ShipItem : MonoBehaviour
{
    [Header("Map Reward")]
    [SerializeField] int gold;
    [SerializeField] int silver;
    [SerializeField] int bronze;
    [SerializeField] int brass;
    [SerializeField] int titanium;

    // @Access from MapsStatus
    public (int, int, int, int, int) GetData()
    {
        return (gold, silver, bronze, brass, titanium);
    }
}
