using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class LaserItem : MonoBehaviour
{
    [SerializeField] int laserIndex;
    [SerializeField] int laserPrice;
    [SerializeField] Currency laserCurrency;

    [SerializeField] GameObject lockedFrame;
    [SerializeField] GameObject selectedFrame;
    [SerializeField] Text priceText;
    [SerializeField] GameObject diamondIcon;
    [SerializeField] GameObject coinIcon;

    void Awake()
    {
        priceText.text = laserPrice.ToString();
        selectedFrame.SetActive(false);
        // Choose between coin or diamond to show on the lock screen
        if (laserCurrency == Currency.Coin)
        {
            coinIcon.SetActive(true);
            diamondIcon.SetActive(false);
        }
        else if (laserCurrency == Currency.Diamond)
        {
            coinIcon.SetActive(false);
            diamondIcon.SetActive(true);
        }
    }

    public (Currency, int, int) GetData()
    {
        return (laserCurrency, laserIndex, laserPrice);
    }

    public void UnlockLaser()
    {
        lockedFrame.SetActive(false);
        priceText.gameObject.SetActive(false);
        diamondIcon.SetActive(false);
        coinIcon.SetActive(false);
    }

    public void UnselectLaser()
    {
        selectedFrame.SetActive(false);
    }

    public void SelectLaser()
    {
        priceText.gameObject.SetActive(false);
        coinIcon.SetActive(false);
        diamondIcon.SetActive(false);
        lockedFrame.SetActive(false);
        selectedFrame.SetActive(true);
    }
}
