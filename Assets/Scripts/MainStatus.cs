using UnityEngine;
using UnityEngine.UI;

public class MainStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    GameObject hapticsButton;
    GameObject soundsButton;
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

    int gold;
    int silver;
    int bronze;
    int brass;
    int titanium;
    int diamonds;
    int coins;
    int power;
    int upgradeIndex;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();

        hapticsButton = GameObject.Find("HapticsButton");
        soundsButton = GameObject.Find("SoundsButton");
        upgradeButton = GameObject.Find("UpgradeButton");

        Transform diamondsParent = GameObject.Find("Canvas").transform.Find("Scoreboard").Find("Diamonds");
        diamondsText = diamondsParent.Find("DiamondCount").GetComponent<Text>();
        Transform coinsParent = GameObject.Find("Canvas").transform.Find("Scoreboard").Find("Coins");
        coinsText = coinsParent.Find("CoinCount").GetComponent<Text>();
        Transform materials = GameObject.Find("Canvas").transform.Find("Scoreboard").Find("Materials");
        goldText = materials.Find("GoldCount").GetComponent<Text>();
        silverText = materials.Find("SilverCount").GetComponent<Text>();
        bronzeText = materials.Find("BronzeCount").GetComponent<Text>();
        brassText = materials.Find("BrassCount").GetComponent<Text>();
        titaniumText = materials.Find("TitaniumCount").GetComponent<Text>();
        powerText = GameObject.Find("Canvas").transform.Find("Power").Find("PowerCount").GetComponent<Text>();

        upgradePrice = upgradeButton.transform.Find("Price").Find("PriceCount").GetComponent<Text>();
        upgradePower = upgradeButton.transform.Find("Power").Find("PowerCount").GetComponent<Text>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.ResetPlayer();
        player.LoadPlayer();

        SetPlayerValues();
        SetScoreboardValues();
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

    private void SetPlayerValues()
    {
        diamonds = player.diamonds;
        coins = player.coins;
        gold = player.gold;
        silver = player.silver;
        bronze = player.bronze;
        brass = player.brass;
        titanium = player.titanium;
        power = player.power;
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = diamonds.ToString();
        coinsText.text = coins.ToString();
        goldText.text = gold.ToString();
        silverText.text = silver.ToString();
        bronzeText.text = bronze.ToString();
        brassText.text = brass.ToString();
        titaniumText.text = titanium.ToString();
        powerText.text = power.ToString();
    }

    public void ClickUpgradeButton()
    {
        Debug.Log("Upgrading");
    }

    public void ClickPlayButton()
    {
        navigator.LoadNextLevel(player.nextLevelIndex);
    }

    public void ClickShopButton()
    {
        navigator.LoadShop();
    }

    public void ClickMapsButton()
    {
        navigator.LoadMaps();
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

    public void ClickSoundsButton()
    {
        if (PlayerPrefs.GetInt("Sounds") == 1)
        {
            // Set button state to disabled
            soundsButton.transform.Find("Disabled").gameObject.SetActive(true);
            // If sounds are turned on => turn them off
            PlayerPrefs.SetInt("Sounds", 0);
        }
        else
        {
            // Set button state to enabled
            soundsButton.transform.Find("Disabled").gameObject.SetActive(false);
            // If sounds are turned off => turn them on
            PlayerPrefs.SetInt("Sounds", 1);
        }
    }

    // Set initial states of haptics and sounds buttons based on player prefs
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
        if (PlayerPrefs.GetInt("Sounds") == 1)
        {
            soundsButton.transform.Find("Disabled").gameObject.SetActive(false);
        }
        else
        {
            soundsButton.transform.Find("Disabled").gameObject.SetActive(true);
        }
    }
}
