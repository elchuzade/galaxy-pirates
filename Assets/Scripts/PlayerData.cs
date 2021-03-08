using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int coins = 0;
    public int diamonds = 0;
    public int gold = 0;
    public int silver = 0;
    public int bronze = 0;
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

    public PlayerData (Player player)
    {
        coins = player.coins;
        diamonds = player.diamonds;
        diamonds = player.diamonds;
        gold = player.gold;
        silver = player.silver;
        bronze = player.bronze;
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
    }
}
