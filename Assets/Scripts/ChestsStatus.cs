using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class ChestsStatus : MonoBehaviour
{
    // TODO: add autoselect to the chest that can be opened
    Player player;
    Navigator navigator;

    [SerializeField] int redChestBasePrice;
    [SerializeField] int purpleChestBasePrice;

    // Reward views
    [SerializeField] GameObject chestClosedView;
    [SerializeField] GameObject chestOpenedView;

    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;

    [SerializeField] GameObject redChest;
    [SerializeField] GameObject purpleChest;
    [SerializeField] GameObject blueChest;

    [SerializeField] GameObject buyButton;
    [SerializeField] GameObject watchButton;
    [SerializeField] GameObject openButton;

    ChestColors selectedChestColor;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();

        chestClosedView.transform.localScale = new Vector3(1, 1, 1);
        chestClosedView.SetActive(false);

        HideAllChestsInChestClosedView();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();

        SetPlayerChests();
        SetScoreboardValues();
        SetChestPrices();
    }

    private void HideAllChestsInChestClosedView()
    {
        chestClosedView.transform.Find("RedChest").gameObject.SetActive(false);
        chestClosedView.transform.Find("PurpleChest").gameObject.SetActive(false);
        chestClosedView.transform.Find("BlueChest").gameObject.SetActive(false);
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = player.diamonds.ToString();
        coinsText.text = player.coins.ToString();
    }

    private void SetChestPrices()
    {
        redChest.GetComponent<ChestItem>().SetPrice(redChestBasePrice);
        purpleChest.GetComponent<ChestItem>().SetPrice(purpleChestBasePrice);
    }

    private void SetPlayerChests()
    {
        redChest.GetComponent<ChestItem>().SetCount(player.redChestCount);
        purpleChest.GetComponent<ChestItem>().SetCount(player.purpleChestCount);
        blueChest.GetComponent<ChestItem>().SetCount(player.blueChestCount);
    }

    private void ResetAllButtons()
    {
        openButton.SetActive(false);
        watchButton.SetActive(false);
        buyButton.SetActive(false);
    }

    public void SelectBlueChest()
    {
        // Check if you can open the chest, or watch the ad
        Debug.Log("selecting blue chest");
        // If there are no blue chests, make play button as watch ad button
        selectedChestColor = ChestColors.Blue;
        ResetAllButtons();
        if (player.blueChestCount > 0)
        {
            openButton.SetActive(true);
        } else
        {
            watchButton.SetActive(true);
        }
    }

    public void SelectPurpleChest()
    {
        // Check if you can open the chest, or buy the chest
        Debug.Log("Opening purple chest");
        selectedChestColor = ChestColors.Purple;
        ResetAllButtons();
        if (player.purpleChestCount > 0)
        {
            openButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
        }
    }

    public void SelectRedChest()
    {
        // Check if you can open the chest, or buy the chest
        Debug.Log("Opening red chest");
        selectedChestColor = ChestColors.Red;
        ResetAllButtons();
        if (player.redChestCount > 0)
        {
            openButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
        }
    }

    public void ClickWatchButton()
    {
        Debug.Log("watching ad, then running these functions");
        player.blueChestCount++;
        player.SavePlayer();
        SetPlayerChests();
        SelectBlueChest();
    }

    public void ClickBuyButton()
    {
        switch (selectedChestColor)
        {
            case ChestColors.Red:
                player.coins -= redChestBasePrice;
                player.redChestCount++;
                SelectRedChest();
                break;
            case ChestColors.Purple:
                player.coins -= purpleChestBasePrice;
                player.purpleChestCount++;
                SelectPurpleChest();
                break;
        }
        player.SavePlayer();
        SetPlayerChests();
        SetScoreboardValues();
    }

    public void ClickOpenButton()
    {
        switch (selectedChestColor)
        {
            case ChestColors.Red:
                player.redChestCount--;
                SelectRedChest();
                break;
            case ChestColors.Purple:
                player.purpleChestCount--;
                SelectPurpleChest();
                break;
            case ChestColors.Blue:
                player.blueChestCount--;
                SelectBlueChest();
                break;
        }
        chestClosedView.SetActive(true);
        // Show only selected chest instead of all three chests
        SetChestColorInChestClosedView();
        player.SavePlayer();
        SetPlayerChests();
    }

    private void SetChestColorInChestClosedView()
    {
        switch (selectedChestColor)
        {
            case ChestColors.Red:
                chestClosedView.transform.Find("RedChest").gameObject.SetActive(true);
                break;
            case ChestColors.Purple:
                chestClosedView.transform.Find("PurpleChest").gameObject.SetActive(true);
                break;
            case ChestColors.Blue:
            default:
                chestClosedView.transform.Find("BlueChest").gameObject.SetActive(true);
                break;
        }
    }

    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }
}
