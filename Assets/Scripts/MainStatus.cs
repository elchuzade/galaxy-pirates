using UnityEngine;
using UnityEngine.UI;

public class MainStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    GameObject hapticsButton;

    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;
    [SerializeField] Text goldText;
    [SerializeField] Text silverText;
    [SerializeField] Text bronzeText;
    [SerializeField] Text brassText;
    [SerializeField] Text titaniumText;
    [SerializeField] Text powerText;
    [SerializeField] Text upgradePrice;
    [SerializeField] Text upgradePower;

    // To run animation and to show notification sign
    [SerializeField] GameObject chestButton;

    int upgradeIndex;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();

        hapticsButton = GameObject.Find("HapticsButton");
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();

        SetScoreboardValues();
        SetButtonInitialState();
        SetUpgradeButton();

        SetChestButton();
    }

    private void SetChestButton()
    {
        if (player.redChestCount > 0 || player.purpleChestCount > 0 || player.blueChestCount > 0)
        {
            chestButton.transform.Find("ChestNotification").gameObject.SetActive(true);
            chestButton.GetComponent<Animator>().Play("ChestShake");
        } else
        {
            chestButton.transform.Find("ChestNotification").gameObject.SetActive(false);
            chestButton.GetComponent<Animator>().enabled = false;
        }
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

    public void ClickChestButton()
    {
        navigator.LoadChests();
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
