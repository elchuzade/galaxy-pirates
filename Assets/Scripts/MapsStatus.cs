using UnityEngine;
using UnityEngine.UI;

public class MapsStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    // Scrollbar handle to control its value
    [SerializeField] GameObject scrollbar;
    // All planets from scroll content
    [SerializeField] GameObject[] planets;
    // To get values from planet, initially first planet will be shown
    int planetIndex = 0;

    int diamonds;
    int coins;

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
        player.ResetPlayer();
        player.LoadPlayer();

        SetPlayerValues();
        SetScoreboardValues();

        scrollbar.GetComponent<Scrollbar>().value = 0;

        SetPlanetValues();
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

    public void SwipePlanet()
    {
        float value = scrollbar.GetComponent<Scrollbar>().value;
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
        } else if (planetIndex == planets.Length - 1)
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
        if(planetIndex > 0)
        {
            planetIndex--;
            scrollbar.GetComponent<Scrollbar>().value = (float)planetIndex / 4;
            SetPlanetValues();
        }
    }

    public void ClickRightArrow()
    {
        if (planetIndex < planets.Length - 1)
        {
            planetIndex++;
            scrollbar.GetComponent<Scrollbar>().value = (float)planetIndex / 4;
            SetPlanetValues();
        }
    }
}
