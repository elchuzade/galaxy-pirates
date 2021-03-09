using UnityEngine;
using UnityEngine.UI;

public class MapsStatus : MonoBehaviour
{
    Navigator navigator;
    Player player;

    // Conquered is the status of previous planets
    [SerializeField] GameObject conqueredButton;
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
        // Set conquered buttons on some planets
        if (player.currentPlanetIndex == planetIndex)
        {
            // Current map must be little different
            conqueredButton.SetActive(false);
        } else
        {
            // Map is either conquered or not
            if (player.allPlanets[planetIndex] == 0)
            {
                // Planet is not conquered
                conqueredButton.SetActive(false);
            } else
            {
                // Planet is conquered
                conqueredButton.SetActive(true);
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
}
