using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class ShopStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    // Build current ship
    [SerializeField] GameObject buildButton;
    // Play the next level with current ship
    [SerializeField] GameObject playButton;
    // Select current ship
    [SerializeField] GameObject selectButton;
    // To hide ship building price when it is unlocked
    [SerializeField] GameObject shipPrice;

    // Scrollbar handle to control its value for ships
    [SerializeField] GameObject shipScrollbar;
    // Scrollbar handle to control its value for lasers
    [SerializeField] GameObject laserScrollbar;
    // All ships from scroll content
    [SerializeField] GameObject[] ships;
    // All lasers to scroll content
    [SerializeField] GameObject[] lasers;

    // To get values from ship, initially first ship will be shown.
    // Required for swipe function to disable arrows when first or last are shown
    int shipIndex = 0;
    // This is index of laser page. Not current laser
    int laserIndex = 0;

    [SerializeField] GameObject shipLeftArrow;
    [SerializeField] GameObject shipRightArrow;

    [SerializeField] GameObject laserLeftArrow;
    [SerializeField] GameObject laserRightArrow;

    // Scoreboard
    [Header("Scoreboard")]
    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;

    int shipGold;
    int shipSilver;
    int shipBronze;
    int shipBrass;
    int shipTitanium;

    // Values of reward for ship completion
    [Header("Map Reward")]
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
        //player.ResetPlayer();
        player.LoadPlayer();

        shipScrollbar.GetComponent<Scrollbar>().value = (float)player.currentShipIndex / 5;
        laserScrollbar.GetComponent<Scrollbar>().value = (float)(player.currentLaserIndex / 4) / 4;

        shipIndex = player.currentShipIndex;
        laserIndex = player.currentLaserIndex / 4;

        SetScoreboardValues();

        SetShipValues();
        SetLaserValues();

        shipScrollbar.GetComponent<Scrollbar>().onValueChanged.AddListener(value => SwipeShip(value));
        laserScrollbar.GetComponent<Scrollbar>().onValueChanged.AddListener(value => SwipeLaser(value));
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = player.diamonds.ToString();
        coinsText.text = player.coins.ToString();
    }

    public void SwipeShip(float value)
    {
        shipIndex = (int)(value * 5);
        SetShipValues();
    }

    public void SwipeLaser(float value)
    {
        laserIndex = (int)(value * 4);
        SetLaserValues();
    }

    private void SetShipValues()
    {
        ShipItem currentShipItem = ships[shipIndex].GetComponent<ShipItem>();

        (int, int, int, int, int) shipData = currentShipItem.GetData();
        shipGold = shipData.Item1;
        shipSilver = shipData.Item2;
        shipBronze = shipData.Item3;
        shipBrass = shipData.Item4;
        shipTitanium = shipData.Item5;

        shipGoldText.text = shipGold.ToString();
        shipSilverText.text = shipSilver.ToString();
        shipBronzeText.text = shipBronze.ToString();
        shipBrassText.text = shipBrass.ToString();
        shipTitaniumText.text = shipTitanium.ToString();

        // Set arrows
        shipLeftArrow.GetComponent<Button>().interactable = true;
        shipRightArrow.GetComponent<Button>().interactable = true;
        shipLeftArrow.GetComponent<Image>().color = new Color32(161, 208, 35, 255);
        shipRightArrow.GetComponent<Image>().color = new Color32(161, 208, 35, 255);

        if (shipIndex == 0)
        {
            shipLeftArrow.GetComponent<Button>().interactable = false;
            shipLeftArrow.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else if (shipIndex == ships.Length - 1)
        {
            shipRightArrow.GetComponent<Button>().interactable = false;
            shipRightArrow.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        // Set unlock or play button
        if (player.currentShipIndex == shipIndex)
        {
            // Current ship must show play button
            playButton.SetActive(true);
            selectButton.SetActive(false);
            buildButton.SetActive(false);
            // Hide price as the ship is already built
            shipPrice.SetActive(false);
        }
        else
        {
            // Ship is either built or locked
            if (player.allShips[shipIndex] == 0)
            {
                // Ship is Locked
                selectButton.SetActive(false);
                playButton.SetActive(false);
                buildButton.SetActive(true);
                // Show ship price as it is not built yet
                shipPrice.SetActive(true);
            }
            else
            {
                // Ship is built
                selectButton.SetActive(true);
                playButton.SetActive(false);
                buildButton.SetActive(false);
                // Hide ship price as it is already built
                shipPrice.SetActive(false);
            }
        }
    }

    private void SetLaserValues()
    {
        // Set arrows
        laserLeftArrow.GetComponent<Button>().interactable = true;
        laserRightArrow.GetComponent<Button>().interactable = true;
        laserLeftArrow.GetComponent<Image>().color = new Color32(161, 208, 35, 255);
        laserRightArrow.GetComponent<Image>().color = new Color32(161, 208, 35, 255);

        if (laserIndex == 0)
        {
            laserLeftArrow.GetComponent<Button>().interactable = false;
            laserLeftArrow.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else if (laserIndex == lasers.Length / 4 - 1)
        {
            laserRightArrow.GetComponent<Button>().interactable = false;
            laserRightArrow.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        for (int i = 0; i < lasers.Length; i++)
        {
            if (player.allLasers[i] == 1)
            {
                lasers[i].GetComponent<LaserItem>().UnlockLaser();
            }
            if (player.currentLaserIndex == i)
            {
                lasers[i].GetComponent<LaserItem>().SelectLaser();
            }
        }
    }

    public void ClickShipLeftArrow()
    {
        if (shipIndex > 0)
        {
            shipIndex--;
            shipScrollbar.GetComponent<Scrollbar>().value = (float)shipIndex / 5;
            SetShipValues();
        }
    }

    public void ClickShipRightArrow()
    {
        if (shipIndex < ships.Length - 1)
        {
            shipIndex++;
            shipScrollbar.GetComponent<Scrollbar>().value = (float)shipIndex / 5;
            SetShipValues();
        }
    }

    public void ClickLaserLeftArrow()
    {
        if (laserIndex > 0)
        {
            laserIndex--;
            laserScrollbar.GetComponent<Scrollbar>().value = (float)laserIndex / 4;
            SetLaserValues();
        }
    }

    public void ClickLaserRightArrow()
    {
        if (laserIndex < lasers.Length / 4 - 1)
        {
            laserIndex++;
            laserScrollbar.GetComponent<Scrollbar>().value = (float)laserIndex / 4;
            SetShipValues();
        }
    }

    public void ClickLaserItem(int index)
    {
        // Either unlock and select or just select laser
        if (player.allLasers[index] == 1)
        {
            // Laser is unlocked. Unselect previous laser, select new laser, save player data
            lasers[player.currentLaserIndex].GetComponent<LaserItem>().UnselectLaser();
            lasers[index].GetComponent<LaserItem>().SelectLaser();
            player.currentLaserIndex = index;
            player.SavePlayer();
        } else
        {
            // Laser is locked. Try buying it
            // Laser Currency, Laser Index, Laser Price
            (Currency, int, int) laserData = lasers[index].GetComponent<LaserItem>().GetData();

            if (laserData.Item1 == Currency.Coin)
            {
                // Try spending coins to unlock laser
                if (player.coins >= laserData.Item3)
                {
                    // Player has sufficient coins. Charge him and unlock laser
                    player.coins -= laserData.Item3;
                    lasers[index].GetComponent<LaserItem>().UnlockLaser();
                    // Unselect previous laser
                    lasers[player.currentLaserIndex].GetComponent<LaserItem>().UnselectLaser();
                    // Select new laser
                    lasers[index].GetComponent<LaserItem>().SelectLaser();
                    // Save current laser index in player Data
                    player.currentLaserIndex = index;
                    player.SavePlayer();
                    // Update scoreboard after new purchase
                    SetScoreboardValues();
                }
            } else if (laserData.Item1 == Currency.Diamond)
            {
                // Try spending coins to unlock laser
                if (player.diamonds >= laserData.Item3)
                {
                    // Player has sufficient coins. Charge him and unlock laser
                    player.diamonds -= laserData.Item3;
                    lasers[index].GetComponent<LaserItem>().UnlockLaser();
                    // Unselect previous laser
                    lasers[player.currentLaserIndex].GetComponent<LaserItem>().UnselectLaser();
                    // Select new laser
                    lasers[index].GetComponent<LaserItem>().SelectLaser();
                    // Save current laser index in player Data
                    player.currentLaserIndex = index;
                    player.SavePlayer();
                    // Update scoreboard after new purchase
                    SetScoreboardValues();
                }
            }
        }
    }

    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }

    public void ClickSelectShip()
    {
        player.currentShipIndex = shipIndex;
        player.SavePlayer();
        SetShipValues();
    }

    public void ClickBuildShip()
    {
        // Try building the ship, if successful, then spend build price and hide symbols
        if (player.gold >= shipGold &&
            player.silver >= shipSilver &&
            player.bronze >= shipBronze &&
            player.brass >= shipBrass &&
            player.titanium >= shipTitanium)
        {
            player.gold -= shipGold;
            player.silver -= shipSilver;
            player.bronze -= shipBronze;
            player.brass -= shipBrass;
            player.titanium -= shipTitanium;

            player.allShips[shipIndex] = 1;
            player.currentShipIndex = shipIndex;
            player.SavePlayer();
            // Update canvas after changing player values
            SetShipValues();
        }
    }

    public void ClickPlayButton()
    {
        navigator.LoadNextLevel(player.nextLevelIndex);
    }
}
