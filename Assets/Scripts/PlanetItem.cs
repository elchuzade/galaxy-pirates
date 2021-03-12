using UnityEngine;

public class PlanetItem : MonoBehaviour
{
    [Header("Map Reward")]
    [SerializeField] int diamond;
    [SerializeField] int coin;
    [SerializeField] int gold;
    [SerializeField] int aluminum;
    [SerializeField] int copper;
    [SerializeField] int brass;
    [SerializeField] int titanium;

    // @access from PlanetsStatus
    public (int, int, int, int, int, int, int) GetData()
    {
        return (diamond, coin, gold, aluminum, copper, brass, titanium);
    }
}
