using UnityEngine;
using UnityEngine.UI;

public class ShopStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    // Scrollbar handle to control its value
    [SerializeField] GameObject scrollbar;
    // All ships from scroll content
    [SerializeField] GameObject[] ships;
    // To get values from ship, initially first ship will be shown
    int shipIndex = 0;

    int diamonds;
    int coins;

    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    // Scoreboard
    [Header("Scoreboard")]
    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;

    int shipCoin;
    int shipDiamond;
    int shipGold;
    int shipSilver;
    int shipBronze;
    int shipBrass;
    int shipTitanium;

    // Values of reward for ship completion
    [Header("Map Reward")]
    [SerializeField] Text shipCoinText;
    [SerializeField] Text shipDiamondText;
    [SerializeField] Text shipGoldText;
    [SerializeField] Text shipSilverText;
    [SerializeField] Text shipBronzeText;
    [SerializeField] Text shipBrassText;
    [SerializeField] Text shipTitaniumText;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.ResetPlayer();
        player.LoadPlayer();

        SetPlayerValues();
        SetScoreboardValues();

        scrollbar.GetComponent<Scrollbar>().value = 0;

        SetShipValues();
    }

    private void SetPlayerValues()
    {
        diamonds = player.diamonds;
        coins = player.coins;
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = diamonds.ToString();
        coinsText.text = coins.ToString();
    }

    public void SwipeShip()
    {
        float value = scrollbar.GetComponent<Scrollbar>().value;
        shipIndex = (int)(value * 4);
        SetShipValues();
    }

    private void SetShipValues()
    {
        ShipItem currentShipItem = ships[shipIndex].GetComponent<ShipItem>();

        (int, int, int, int, int, int, int) shipData = currentShipItem.getData();
        shipCoin = shipData.Item1;
        shipDiamond = shipData.Item2;
        shipGold = shipData.Item3;
        shipSilver = shipData.Item4;
        shipBronze = shipData.Item5;
        shipBrass = shipData.Item6;
        shipTitanium = shipData.Item7;

        shipCoinText.text = shipCoin.ToString();
        shipDiamondText.text = shipDiamond.ToString();
        shipGoldText.text = shipGold.ToString();
        shipSilverText.text = shipSilver.ToString();
        shipBronzeText.text = shipBronze.ToString();
        shipBrassText.text = shipBrass.ToString();
        shipTitaniumText.text = shipTitanium.ToString();

        // Set arrows
        if (shipIndex == 0)
        {
            leftArrow.GetComponent<Button>().interactable = false;
            leftArrow.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else if (shipIndex == ships.Length - 1)
        {
            rightArrow.GetComponent<Button>().interactable = false;
            rightArrow.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else
        {
            leftArrow.GetComponent<Button>().interactable = true;
            rightArrow.GetComponent<Button>().interactable = true;
            leftArrow.GetComponent<Image>().color = new Color32(161, 208, 35, 255);
            rightArrow.GetComponent<Image>().color = new Color32(161, 208, 35, 255);

        }
    }

    public void ClickLeftArrow()
    {
        if (shipIndex > 0)
        {
            shipIndex--;
            scrollbar.GetComponent<Scrollbar>().value = (float)shipIndex / 4;
            SetShipValues();
        }
    }

    public void ClickRightArrow()
    {
        if (shipIndex < ships.Length - 1)
        {
            shipIndex++;
            scrollbar.GetComponent<Scrollbar>().value = (float)shipIndex / 4;
            SetShipValues();
        }
    }
}
