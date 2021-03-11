using UnityEngine;
using UnityEngine.UI;

public class MainStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    GameObject hapticsButton;
    GameObject upgradeButton;

    Text diamondsText;
    Text coinsText;
    Text goldText;
    Text silverText;
    Text bronzeText;
    Text brassText;
    Text titaniumText;
    Text powerText;
    Text upgradePrice;
    Text upgradePower;

    int upgradeIndex;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();

        hapticsButton = GameObject.Find("HapticsButton");
        upgradeButton = GameObject.Find("UpgradeButton");

        Transform diamondsParent = GameObject.Find("MainCanvas").transform.Find("Scoreboard").Find("Diamonds");
        diamondsText = diamondsParent.Find("DiamondCount").GetComponent<Text>();
        Transform coinsParent = GameObject.Find("MainCanvas").transform.Find("Scoreboard").Find("Coins");
        coinsText = coinsParent.Find("CoinCount").GetComponent<Text>();
        Transform materials = GameObject.Find("MainCanvas").transform.Find("Materials");
        goldText = materials.Find("GoldCount").GetComponent<Text>();
        silverText = materials.Find("SilverCount").GetComponent<Text>();
        bronzeText = materials.Find("BronzeCount").GetComponent<Text>();
        brassText = materials.Find("BrassCount").GetComponent<Text>();
        titaniumText = materials.Find("TitaniumCount").GetComponent<Text>();
        powerText = GameObject.Find("MainCanvas").transform.Find("Power").Find("PowerCount").GetComponent<Text>();

        upgradePrice = upgradeButton.transform.Find("Price").Find("PriceCount").GetComponent<Text>();
        upgradePower = upgradeButton.transform.Find("Power").Find("PowerCount").GetComponent<Text>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.ResetPlayer();
        player.LoadPlayer();

        SetScoreboardValues();
        SetButtonInitialState();
        SetUpgradeButton();
    }

    private void SetUpgradeButton()
    {
        upgradeIndex = player.upgradeIndex;
        if (upgradeIndex < player.upgradePower.Count)
        {
            upgradePower.text = "+" + player.upgradePower[upgradeIndex].ToString();
            upgradePrice.text = "-" + player.upgradePrice[upgradeIndex].ToString();
        }
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = player.diamonds.ToString();
        coinsText.text = player.coins.ToString();
        goldText.text = player.gold.ToString();
        silverText.text = player.silver.ToString();
        bronzeText.text = player.bronze.ToString();
        brassText.text = player.brass.ToString();
        titaniumText.text = player.titanium.ToString();
        powerText.text = player.power.ToString();
    }

    public void ClickUpgradeButton()
    {
        Debug.Log("Upgrading");
    }

    public void ClickPlayButton()
    {
        Debug.Log("player.nextLevelIndex");
        Debug.Log(player.nextLevelIndex);
        navigator.LoadNextLevel(player.nextLevelIndex);
    }

    public void ClickShopButton()
    {
        navigator.LoadShop();
    }

    public void ClickPlanetsButton()
    {
        navigator.LoadPlanets();
    }

    public void ClickHapticsButton()
    {
        if (PlayerPrefs.GetInt("Haptics") == 1)
        {
            // Set button state to disabled
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(true);
            // If haptics are turned on => turn them off
            PlayerPrefs.SetInt("Haptics", 0);
        }
        else
        {
            // Set button state to enabled
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(false);
            // If haptics are turned off => turn them on
            PlayerPrefs.SetInt("Haptics", 1);
        }
    }

    // Set initial states of haptics button based on player prefs
    private void SetButtonInitialState()
    {
        if (PlayerPrefs.GetInt("Haptics") == 1)
        {
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(false);
        }
        else
        {
            hapticsButton.transform.Find("Disabled").gameObject.SetActive(true);
        }
    }
}
