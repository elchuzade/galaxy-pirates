using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class LevelStatus : MonoBehaviour
{
    // Scoreboard gold scrap material icon
    GameObject goldIcon;
    GameObject aluminumIcon;
    GameObject copperIcon;
    GameObject brassIcon;
    GameObject titaniumIcon;
    GameObject diamondsIcon;
    GameObject coinsIcon;
    Text diamondsText;
    Text coinsText;

    int gold;
    int aluminum;
    int copper;
    int brass;
    int titanium;
    int diamonds;
    int coins;

    void Awake()
    {
        // materials inside scoreboard inside canvas
        Transform materials = GameObject.Find("Canvas").transform.Find("Scoreboard").Find("Materials");
        goldIcon = materials.Find("Gold").gameObject;
        aluminumIcon = materials.Find("Aluminum").gameObject;
        copperIcon = materials.Find("Copper").gameObject;
        brassIcon = materials.Find("Brass").gameObject;
        titaniumIcon = materials.Find("Titanium").gameObject;

        Transform diamondsParent = GameObject.Find("Canvas").transform.Find("Scoreboard").Find("Diamonds");
        Transform coinsParent = GameObject.Find("Canvas").transform.Find("Scoreboard").Find("Coins");

        diamondsIcon = diamondsParent.Find("Diamond").gameObject;
        coinsIcon = coinsParent.Find("Coin").gameObject;
        diamondsText = diamondsParent.Find("DiamondCount").GetComponent<Text>();
        coinsText = coinsParent.Find("CoinCount").GetComponent<Text>();
    }

    void Start()
    {
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
}
