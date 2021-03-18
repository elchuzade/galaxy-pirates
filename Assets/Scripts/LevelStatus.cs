using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class LevelStatus : MonoBehaviour
{
    Player player;
    Navigator navigator;

    [SerializeField] GameObject winPhrase;

    [SerializeField] GameObject gamePlane;
    [SerializeField] GameObject keys;

    [SerializeField] GameObject redKeyItem;
    [SerializeField] GameObject purpleKeyItem;
    [SerializeField] GameObject blueKeyItem;

    // Coordiantes where to place a player ship
    [SerializeField] GameObject playerShipPosition;
    [SerializeField] GameObject[] playerShips;

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
    int redKeys;
    int purpleKeys;
    int blueKeys;

    void Awake()
    {
        navigator = FindObjectOfType<Navigator>();

        winPhrase.transform.localScale = new Vector3(1, 1, 1);
        winPhrase.SetActive(false);
        gamePlane.SetActive(false);
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        // player.ResetPlayer()
        player.LoadPlayer();

        SetPlayerShip();
        SetPlayerValues();
        SetScoreboardValues();
    }

    public void PassLevel()
    {
        Debug.Log("efe");
        winPhrase.SetActive(true);
    }

    private void SetPlayerShip()
    {
        // Instantiate i'th ship from playerShips that corresponds to the current player ship. Order matters
        GameObject playerShip = Instantiate(playerShips[player.currentShipIndex], playerShipPosition.transform.position, Quaternion.identity);
        playerShip.transform.SetParent(playerShipPosition.transform);
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

    public void CollectDroppableItem(DroppableItemName itemName)
    {
        if (itemName == DroppableItemName.Coin)
        {
            CollectCoins(1);
        }
        else if (itemName == DroppableItemName.Diamond)
        {
            CollectDiamonds(1);
        }
        else if (itemName == DroppableItemName.RedKey ||
            itemName == DroppableItemName.PurpleKey ||
            itemName == DroppableItemName.BlueKey)
        {
            CollectKey(itemName);
        }
    }

    private void CollectKey(DroppableItemName keyName)
    {
        // Increase count of that key and instantiate it in the keys horizontal layout
        if (keyName == DroppableItemName.RedKey)
        {
            redKeys++;
            GameObject redKeyInstance = Instantiate(redKeyItem, keys.transform.position, Quaternion.Euler(0, 0, -30));
            redKeyInstance.transform.SetParent(keys.transform);
        }
        else if (keyName == DroppableItemName.PurpleKey)
        {
            purpleKeys++;
            GameObject purpleKeyInstance = Instantiate(purpleKeyItem, keys.transform.position, Quaternion.Euler(0, 0, -30));
            purpleKeyInstance.transform.SetParent(keys.transform);
        }
        else if (keyName == DroppableItemName.BlueKey)
        {
            blueKeys++;
            GameObject blueKeyInstance = Instantiate(blueKeyItem, keys.transform.position, Quaternion.Euler(0, 0, -30));
            blueKeyInstance.transform.SetParent(keys.transform);
        }
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
