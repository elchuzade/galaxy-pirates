using UnityEngine;
using UnityEngine.UI;

public class MapsStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    // Unlock the planet
    [SerializeField] GameObject unlockButton;
    // Play the first level of the planet
    [SerializeField] GameObject playButton;
    // Conquered is the status of previous planets
    [SerializeField] GameObject conqueredButton;
    // To hide all rewards when map is already unlocked
    [SerializeField] GameObject mapReward;

    // Scrollbar handle to control its value
    [SerializeField] GameObject planetScrollbar;
    // All planets from scroll content
    [SerializeField] GameObject[] planets;
    // To get values from planet, initially first planet will be shown
    int planetIndex = 0;
    // Every planet will have 25 levels, to know when to unlock a new planet
    int levelsPerPlanet = 25;

    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    // Scoreboard
    [Header("Scoreboard")]
    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;

    int planetCoin;
    int planetDiamond;
    int planetGold;
    int planetSilver;
    int planetBronze;
    int planetBrass;
    int planetTitanium;

    // Values of reward for planet completion
    [Header("Map Reward")]
    [SerializeField] Text planetCoinText;
    [SerializeField] Text planetDiamondText;
    [SerializeField] Text planetGoldText;
    [SerializeField] Text planetSilverText;
    [SerializeField] Text planetBronzeText;
    [SerializeField] Text planetBrassText;
    [SerializeField] Text planetTitaniumText;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();

        player.nextLevelIndex = 76;
        player.SavePlayer();

        // Autoscroll to current planet
        planetScrollbar.GetComponent<Scrollbar>().value = (float)player.currentPlanetIndex / 4;
        planetIndex = player.currentPlanetIndex;

        SetScoreboardValues();

        SetPlanetValues();

        planetScrollbar.GetComponent<Scrollbar>().onValueChanged.AddListener(value => SwipePlanet(value));
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = player.diamonds.ToString();
        coinsText.text = player.coins.ToString();
    }

    public void SwipePlanet(float value)
    {
        planetIndex = (int)(value * 4);
        SetPlanetValues();
    }

    private void SetPlanetValues()
    {
        PlanetItem currentPlanet = planets[planetIndex].GetComponent<PlanetItem>();

        (int, int, int, int, int, int, int) planetData = currentPlanet.getData();
        planetCoin = planetData.Item1;
        planetDiamond = planetData.Item2;
        planetGold = planetData.Item3;
        planetSilver = planetData.Item4;
        planetBronze = planetData.Item5;
        planetBrass = planetData.Item6;
        planetTitanium = planetData.Item7;

        planetCoinText.text = planetCoin.ToString();
        planetDiamondText.text = planetDiamond.ToString();
        planetGoldText.text = planetGold.ToString();
        planetSilverText.text = planetSilver.ToString();
        planetBronzeText.text = planetBronze.ToString();
        planetBrassText.text = planetBrass.ToString();
        planetTitaniumText.text = planetTitanium.ToString();

        // Set arrows
        if (planetIndex == 0)
        {
            leftArrow.GetComponent<Button>().interactable = false;
            leftArrow.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else if (planetIndex == planets.Length - 1)
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
        // Set unlock or play button
        if (player.currentPlanetIndex == planetIndex)
        {
            // Current map must show play button
            conqueredButton.SetActive(false);
            playButton.SetActive(true);
            unlockButton.SetActive(false);
            // Hide reward, as it is already received
            mapReward.SetActive(false);
        } else
        {
            // Map is either conquered or locked
            if (player.allPlanets[planetIndex] == 0)
            {
                // Planet is Locked
                conqueredButton.SetActive(false);
                playButton.SetActive(false);
                unlockButton.SetActive(true);
                // Show reward, as it is not received yet
                mapReward.SetActive(true);
            } else
            {
                // Planet is conquered
                conqueredButton.SetActive(true);
                playButton.SetActive(false);
                unlockButton.SetActive(false);
                // Hide reward, as it is already received
                mapReward.SetActive(false);
            }
        }
    }

    public void ClickLeftArrow()
    {
        if(planetIndex > 0)
        {
            planetIndex--;
            planetScrollbar.GetComponent<Scrollbar>().value = (float)planetIndex / 4;
            SetPlanetValues();
        }
    }

    public void ClickRightArrow()
    {
        if (planetIndex < planets.Length - 1)
        {
            planetIndex++;
            planetScrollbar.GetComponent<Scrollbar>().value = (float)planetIndex / 4;
            SetPlanetValues();
        }
    }

    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }

    public void ClickUnlockButton()
    {
        // Try unlocking the Planet, if successful, then receive reward and hide symbols
        // To unlock a planet all previous levels need be completed
        if (player.nextLevelIndex > levelsPerPlanet * planetIndex)
        {
            player.coins += planetCoin;
            player.diamonds += planetDiamond;
            player.gold += planetGold;
            player.silver += planetSilver;
            player.bronze += planetBronze;
            player.brass += planetBrass;
            player.titanium += planetTitanium;

            player.allPlanets[planetIndex] = 1;
            player.currentPlanetIndex = planetIndex;
            player.SavePlayer();
            // Update canvas after chaning player values
            SetPlanetValues();
        }

    }

    public void ClickPlayButton()
    {
        navigator.LoadNextLevel(player.nextLevelIndex);
    }
}
