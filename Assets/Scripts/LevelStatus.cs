using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class LevelStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;
    // Scoreboard gold scrap material icon
    [SerializeField] GameObject goldIcon;
    [SerializeField] GameObject aluminumIcon;
    [SerializeField] GameObject copperIcon;
    [SerializeField] GameObject brassIcon;
    [SerializeField] GameObject titaniumIcon;
    [SerializeField] GameObject diamondsIcon;
    [SerializeField] GameObject coinsIcon;
    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;

    int gold;
    int aluminum;
    int copper;
    int brass;
    int titanium;
    int diamonds;
    int coins;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();

        player.LoadPlayer();
        SetPlayerValues();
        SetScoreboardValues();
    }

    // @Access from droppable diamonds
    // Increment when it reaches the canvas diaond icon
    public void CollectDiamonds(int count)
    {
        diamonds += count;
        diamondsIcon.GetComponent<TriggerAnimation>().Trigger();
        SetScoreboardValues();
    }

    // @Access from droppable coins
    // Increment when it reaches the canvas coin icon
    public void CollectCoins(int count)
    {
        coins += count;
        coinsIcon.GetComponent<TriggerAnimation>().Trigger();
        SetScoreboardValues();
    }

    private void SetPlayerValues()
    {
        coins = player.coins;
        diamonds = player.diamonds;
        gold = player.gold;
        aluminum = player.aluminum;
        copper = player.copper;
        brass = player.brass;
        titanium = player.titanium;
    }

    private void SetScoreboardValues()
    {
        diamondsText.text = diamonds.ToString();
        coinsText.text = coins.ToString();
    }

    // @Access from scrap material script
    // Call when scrap material unit is being destroyed
    public void CollectScrapMaterial(ScrapMaterialName scrapMaterialName)
    {
        // Run the zoom animation on the appropriate scrap material
        // Increase the local appropriate material count
        switch (scrapMaterialName)
        {
            case ScrapMaterialName.Gold:
                goldIcon.GetComponent<TriggerAnimation>().Trigger();
                gold++;
                break;
            case ScrapMaterialName.Aluminum:
                aluminumIcon.GetComponent<TriggerAnimation>().Trigger();
                aluminum++;
                break;
            case ScrapMaterialName.Copper:
                copperIcon.GetComponent<TriggerAnimation>().Trigger();
                copper++;
                break;
            case ScrapMaterialName.Brass:
                brassIcon.GetComponent<TriggerAnimation>().Trigger();
                brass++;
                break;
            case ScrapMaterialName.Titanium:
                titaniumIcon.GetComponent<TriggerAnimation>().Trigger();
                titanium++;
                break;
            default:
                break;
        }
    }

    public void ClickHomeButton()
    {
        navigator.LoadMainScene();
    }
}
