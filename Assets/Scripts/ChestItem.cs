using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static GlobalVariables;

public class ChestItem : MonoBehaviour
{
    [SerializeField] ChestColors chestColor;
    [SerializeField] Text chestCount;
    [SerializeField] Text chestPrice;

    // Base is used to multiply the reward by factor that effects the chest price
    [Header("Diamond reward")]
    [SerializeField] int diamondChance;
    [SerializeField] int diamondMin;
    [SerializeField] int diamondMax;
    [Header("Coin reward")]
    [SerializeField] int coinChance;
    [SerializeField] int coinBaseMin;
    [SerializeField] int coinBaseMax;
    [Header("Gold reward")]
    [SerializeField] int goldChance;
    [SerializeField] int goldBaseMin;
    [SerializeField] int goldBaseMax;
    [Header("Silver reward")]
    [SerializeField] int silverChance;
    [SerializeField] int silverBaseMin;
    [SerializeField] int silverBaseMax;
    [Header("Bronze reward")]
    [SerializeField] int bronzeChance;
    [SerializeField] int bronzeBaseMin;
    [SerializeField] int bronzeBaseMax;
    [Header("Brass reward")]
    [SerializeField] int brassChance;
    [SerializeField] int brassBaseMin;
    [SerializeField] int brassBaseMax;
    [Header("Titanium reward")]
    [SerializeField] int titaniumChance;
    [SerializeField] int titaniumBaseMin;
    [SerializeField] int titaniumBaseMax;

    List<Rewards> allRewards = new List<Rewards>();

    void Start()
    {
        BuildAllRewards();
    }

    private void BuildAllRewards()
    {
        // Add all the possible rewards into the basket of all rewards
        for (int i = 0; i < diamondChance; i++)
        {
            allRewards.Add(Rewards.Diamond);
        }
        for (int i = 0; i < coinChance; i++)
        {
            allRewards.Add(Rewards.Coin);
        }
        for (int i = 0; i < goldChance; i++)
        {
            allRewards.Add(Rewards.Gold);
        }
        for (int i = 0; i < silverChance; i++)
        {
            allRewards.Add(Rewards.Silver);
        }
        for (int i = 0; i < bronzeChance; i++)
        {
            allRewards.Add(Rewards.Bronze);
        }
        for (int i = 0; i < brassChance; i++)
        {
            allRewards.Add(Rewards.Brass);
        }
        for (int i = 0; i < titaniumChance; i++)
        {
            allRewards.Add(Rewards.Titanium);
        }
    }

    // @access frm ChestsStatus
    public void SetCount(int count)
    {
        chestCount.text = count.ToString();
        // Hide price tag if there are some chests
        if (count > 0)
        {
            chestPrice.transform.parent.gameObject.SetActive(false);
        } else
        {
            chestPrice.transform.parent.gameObject.SetActive(true);
        }
    }

    // @access frm ChestsStatus
    public void SetPrice(int price)
    {
        chestPrice.text = price.ToString();
    }

    public List<Rewards> GetAllRewards()
    {
        return allRewards;
    }
}
