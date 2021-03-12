using UnityEngine;
using UnityEngine.UI;

public class PlanetsStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    [SerializeField] PlanetRewardsView planetRewardView;
    // Conquered is the status of previous planets
    [SerializeField] GameObject conqueredButton;
    // Collect is the button to show if you passed the 25th levels and haven't collected reward yet
    [SerializeField] GameObject collectButton;
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

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        player.ResetPlayer();
        player.LoadPlayer();

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
        conqueredButton.SetActive(false);
        collectButton.SetActive(false);

        // To decide what to drop as a reward if it has not been collected yet
        PlanetItem currentPlanet = planets[planetIndex].GetComponent<PlanetItem>();
        (int, int, int, int, int, int, int) planetData = currentPlanet.GetData();

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

        if (player.allPlanets[planetIndex] == 0)
        {
            planets[planetIndex].transform.Find("PlanetIcon").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else if (player.allPlanets[planetIndex] == -1)
        {
            planets[planetIndex].transform.Find("PlanetIcon").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            // Planet is not collected but passed 
            collectButton.SetActive(true);
        }
        else if (player.allPlanets[planetIndex] == 1)
        {
            planets[planetIndex].transform.Find("PlanetIcon").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            // Planet is conquered and collected
            conqueredButton.SetActive(true);
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

    public void ClickCollectPlanetReward()
    {
        // Open the reward view and pass planet data to it show one by one
        planetRewardView.SetPlanetData(planets[planetIndex].GetComponent<PlanetItem>().GetData());
    }

    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }
}
