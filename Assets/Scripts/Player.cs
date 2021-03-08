using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
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
    public bool nameChanged = false;
    public bool playerCreated = false;
    public int upgradeIndex = 0;
    public List<int> upgradePrice = new List<int>() { 100, 200, 300, 400, 500 };
    public List<int> upgradePower = new List<int>() { 10, 20, 30, 40, 50 };

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
        gold = 555;
        silver = 555;
        bronze = 555;
        brass = 555;
        titanium = 555;
        power = 100;
        nextLevelIndex = 1;
        playerName = "";
        playerCreated = false;
        nameChanged = false;
        upgradeIndex = 0;
        upgradePrice = new List<int>() { 100, 200, 300, 400, 500 };
        upgradePower = new List<int>() { 10, 20, 30, 40, 50 };

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
        silver = data.silver;
        bronze = data.bronze;
        brass = data.brass;
        titanium = data.titanium;
        power = data.power;
        nameChanged = data.nameChanged;
        nextLevelIndex = data.nextLevelIndex;
        upgradeIndex = data.upgradeIndex;
        upgradePrice = data.upgradePrice;
        upgradePower = data.upgradePower;
    }
}
