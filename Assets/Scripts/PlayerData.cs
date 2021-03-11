using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
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
    public bool playerCreated = false;
    public bool nameChanged = false;
    public int upgradeIndex = 0;
    public List<int> upgradePrice = new List<int>() { 100, 200, 300, 400, 500 };
    public List<int> upgradePower = new List<int>() { 10, 20, 30, 40, 50 };
    public List<int> allPlanets = new List<int>() { 1, 0, 0, 0, 0 };
    public List<int> allShips = new List<int>() { 1, 0, 0, 0, 0, 0 };
    public List<int> allLasers = new List<int>() { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int currentLaserIndex = 0;
    public int currentShipIndex = 0;
    public int currentPlanetIndex = 0;
    public int redChestCount = 0;
    public int purpleChestCount = 0;
    public int blueChestCount = 0;

    public PlayerData (Player player)
    {
        coins = player.coins;
        diamonds = player.diamonds;
        gold = player.gold;
        aluminum = player.aluminum;
        copper = player.copper;
        brass = player.brass;
        titanium = player.titanium;
        power = player.power;
        nextLevelIndex = player.nextLevelIndex;
        playerName = player.playerName;
        playerCreated = player.playerCreated;
        nameChanged = player.nameChanged;
        upgradeIndex = player.upgradeIndex;
        upgradePrice = player.upgradePrice;
        upgradePower = player.upgradePower;
        allPlanets = player.allPlanets;
        allLasers = player.allLasers;
        allShips = player.allShips;
        currentLaserIndex = player.currentLaserIndex;
        currentShipIndex = player.currentShipIndex;
        currentPlanetIndex = player.currentPlanetIndex;
        redChestCount = player.redChestCount;
        purpleChestCount = player.purpleChestCount;
        blueChestCount = player.blueChestCount;
    }
}
