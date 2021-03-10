using UnityEngine;

public class ChestsStatus : MonoBehaviour
{
    [SerializeField] GameObject redChestCount;
    [SerializeField] GameObject purpleChestCount;
    [SerializeField] GameObject blueChestCount;
    [SerializeField] int redChestPrice;
    [SerializeField] int purpleChestPrice;

    [SerializeField] GameObject chestClosedView;
    [SerializeField] GameObject chestOpenedView;

    void Awake()
    {
        chestClosedView.transform.localScale = new Vector3(1, 1, 1);
        chestClosedView.SetActive(false);
        chestOpenedView.transform.localScale = new Vector3(1, 1, 1);
        chestOpenedView.SetActive(false);
    }

    public void OpenBlueChest()
    {
        // Check if you can open the chest, or watch the ad
        Debug.Log("Opening blue chest");
    }

    public void OpenPurpleChest()
    {
        // Check if you can open the chest, or buy the chest
        Debug.Log("Opening purple chest");
    }

    public void OpenRedChest()
    {
        // Check if you can open the chest, or buy the chest
        Debug.Log("Opening red chest");
    }

    public void BuySelectedChest()
    {
        Debug.Log("Buying selected chest");
    }
}
