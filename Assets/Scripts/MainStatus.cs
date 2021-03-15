using UnityEngine;
using UnityEngine.UI;
using static Server;

public class MainStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;
    Server server;
    TV tv;

    GameObject hapticsButton;

    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;
    [SerializeField] Text goldText;
    [SerializeField] Text aluminumText;
    [SerializeField] Text copperText;
    [SerializeField] Text brassText;
    [SerializeField] Text titaniumText;
    [SerializeField] Text powerText;
    [SerializeField] Text upgradePrice;
    [SerializeField] Text upgradePower;

    // To run animation and to show notification sign
    [SerializeField] GameObject chestButton;

    [SerializeField] GameObject privacyWindow;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
        server = FindObjectOfType<Server>();
        tv = FindObjectOfType<TV>();

        hapticsButton = GameObject.Find("HapticsButton");

        privacyWindow.transform.localScale = new Vector3(1, 1, 1);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();

        server.GetVideoLink();

        if (player.privacyPolicy)
        {
            privacyWindow.SetActive(false);
            //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ClickLeaderboardButton());

            if (!player.playerCreated)
            {
                //server.CreatePlayer();
            }
            else
            {
                //server.SavePlayerData(player);
            }
        }
        else
        {
            //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ShowPrivacyPolicy());
            //leaderboardButton.transform.Find("Components").Find("Frame").GetComponent<Image>().color = new Color32(255, 197, 158, 100);
            //leaderboardButton.transform.Find("Components").Find("Icon").GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }

        SetScoreboardValues();
        SetButtonInitialState();
        SetUpgradeButton();

        SetChestButton();
    }

    // Set video link from server file
    public void SetVideoLinkSuccess(VideoJson response)
    {
        tv.SetAdLink(response.video);
        tv.SetAdButton(response.website);
    }

    // Set error actions of video link from server file
    public void SetVideoLinkError(string response)
    {
        tv.SetLightsRed();
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
        upgradePower.text = "+" + player.nextUpgradePower.ToString();
        upgradePrice.text = "-" + player.nextUpgradePrice.ToString();
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = player.diamonds.ToString();
        coinsText.text = player.coins.ToString();
        goldText.text = player.gold.ToString();
        aluminumText.text = player.aluminum.ToString();
        copperText.text = player.copper.ToString();
        brassText.text = player.brass.ToString();
        titaniumText.text = player.titanium.ToString();
        powerText.text = player.power.ToString();
    }

    public void ClickUpgradeButton()
    {
        if (player.coins >= player.nextUpgradePrice)
        {
            int upgradeStepPrice = Random.Range(player.upgradeStepPriceMin, player.upgradeStepPriceMax);
            int upgradeStepPower = Random.Range(player.upgradeStepPowerMin, player.upgradeStepPowerMax);

            player.coins -= player.nextUpgradePrice;
            player.power += player.nextUpgradePower;



            player.nextUpgradePrice += upgradeStepPrice;
            player.nextUpgradePower += upgradeStepPower;

            player.SavePlayer();
            SetScoreboardValues();
            SetUpgradeButton();
        }
    }

    public void ClickPlayButton()
    {
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

    public void ClickLeaderboardButton()
    {
        navigator.LoadLeaderboard();
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

    public void ShowPrivacyPolicy()
    {
        privacyWindow.SetActive(true);
    }

    public void ClickDeclinePrivacyPolicy()
    {
        //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => privacyWindow.SetActive(true));
        privacyWindow.SetActive(false);
    }

    public void ClickAcceptPrivacyPolicy()
    {
        //leaderboardButton.transform.Find("Components").Find("Frame").GetComponent<Image>().color = new Color32(255, 197, 158, 255);
        //leaderboardButton.transform.Find("Components").Find("Icon").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        //leaderboardButton.GetComponent<Button>().onClick.AddListener(() => ClickLeaderboardButton());

        privacyWindow.transform.localScale = new Vector3(0, 1, 1);
        privacyWindow.SetActive(false);
        player.privacyPolicy = true;
        player.SavePlayer();

        //server.CreatePlayer();
    }

    public void ClickTermsOfUse()
    {
        Application.OpenURL("https://abboxgames.com/terms-of-use");
    }

    public void ClickPrivacyPolicy()
    {
        Application.OpenURL("https://abboxgames.com/privacy-policy");
    }
}
