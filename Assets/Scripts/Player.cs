using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int coins = 0;
    public int diamonds = 0;
    public int gold = 0;
    public int aluminum = 0;
    public int copper = 0;
    public int brass = 0;
    public int titanium = 0;
    public int power = 100;
    public int nextLevelIndex = 1;
    public string playerName = "";
    public bool nameChanged = false;
    public bool playerCreated = false;
    public List<int> allPlanets = new List<int>() { 1, 0, 0, 0, 0 };
    public List<int> allShips = new List<int>() { 1, 0, 0, 0, 0, 0 };
    public List<int> allLasers = new List<int>() { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int currentLaserIndex = 0;
    public int currentShipIndex = 0;
    public int currentPlanetIndex = 0;
    public int redChestCount = 0;
    public int purpleChestCount = 0;
    public int blueChestCount = 0;
    // Upgrade logic
    public int nextUpgradePrice = 100; // What you will have next
    public int nextUpgradePower = 10; // What you will have next
    public int upgradeStepPriceMin = 10; // How much price will increase at least
    public int upgradeStepPriceMax = 50; // How much price will increase at most
    public int upgradeStepPowerMin = 2; // How much power will increase at least
    public int upgradeStepPowerMax = 5; // How much power will increase at most

    void Awake()
    {
        transform.SetParent(transform.parent.parent);
        // Singleton
        int instances = FindObjectsOfType<Player>().Length;
        if (instances > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void ResetPlayer()
    {
        coins = 44444;
        playerName = "";
        diamonds = 555;
        gold = 9555;
        aluminum = 9555;
        copper = 9555;
        brass = 9555;
        titanium = 9555;
        power = 100;
        nextLevelIndex = 1;
        playerName = "";
        playerCreated = false;
        nameChanged = false;
        allPlanets = new List<int>() { 1, 1, 1, 0, 0 };
        allShips = new List<int>() { 1, 1, 0, 1, 0, 0 };
        allLasers = new List<int>() { 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0 };
        currentLaserIndex = 13;
        currentShipIndex = 3;
        currentPlanetIndex = 2;
        redChestCount = 0;
        purpleChestCount = 0;
        blueChestCount = 0;
        // Upgrade logic
        nextUpgradePrice = 100;
        nextUpgradePower = 10;
        upgradeStepPriceMin = 10;
        upgradeStepPriceMax = 50;
        upgradeStepPowerMin = 2;
        upgradeStepPowerMax = 5;

        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            ResetPlayer();
            data = SaveSystem.LoadPlayer();
        }

        playerCreated = data.playerCreated;
        playerName = data.playerName;
        coins = data.coins;
        diamonds = data.diamonds;
        gold = data.gold;
        aluminum = data.aluminum;
        copper = data.copper;
        brass = data.brass;
        titanium = data.titanium;
        power = data.power;
        nameChanged = data.nameChanged;
        nextLevelIndex = data.nextLevelIndex;
        allPlanets = data.allPlanets;
        allLasers = data.allLasers;
        allShips = data.allShips;
        currentLaserIndex = data.currentLaserIndex;
        currentShipIndex = data.currentShipIndex;
        currentPlanetIndex = data.currentPlanetIndex;
        redChestCount = data.redChestCount;
        purpleChestCount = data.purpleChestCount;
        blueChestCount = data.blueChestCount;
        // Upgrade logic
        nextUpgradePrice = data.nextUpgradePrice;
        nextUpgradePower = data.nextUpgradePower;
        upgradeStepPriceMin = data.upgradeStepPriceMin;
        upgradeStepPriceMax = data.upgradeStepPriceMax;
        upgradeStepPowerMin = data.upgradeStepPowerMin;
        upgradeStepPowerMax = data.upgradeStepPowerMax;
    }
}
