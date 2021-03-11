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
    [SerializeField] int diamondBaseMin;
    [SerializeField] int diamondBaseMax;
    [Header("Coin reward")]
    [SerializeField] int coinChance;
    [SerializeField] int coinBaseMin;
    [SerializeField] int coinBaseMax;
    [Header("Gold reward")]
    [SerializeField] int goldChance;
    [SerializeField] int goldBaseMin;
    [SerializeField] int goldBaseMax;
    [Header("Aluminum reward")]
    [SerializeField] int aluminumChance;
    [SerializeField] int aluminumBaseMin;
    [SerializeField] int aluminumBaseMax;
    [Header("Copper reward")]
    [SerializeField] int copperChance;
    [SerializeField] int copperBaseMin;
    [SerializeField] int copperBaseMax;
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
        for (int i = 0; i < aluminumChance; i++)
        {
            allRewards.Add(Rewards.Aluminum);
        }
        for (int i = 0; i < copperChance; i++)
        {
            allRewards.Add(Rewards.Copper);
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

    // @access from ChestsStatus
    public void DeselectChest()
    {
        transform.Find("ChestSelect").GetComponent<TriggerAnimation>().TriggerSpecificAnimation("Deselect");
    }

    // @access from ChestsStatus
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

    // @access from ChestsStatus
    public void SetPrice(int price)
    {
        chestPrice.text = price.ToString();
    }

    // @access from OpenedChestView
    public List<Rewards> GetAllRewards()
    {
        return allRewards;
    }
    // @access from OpenedChestView
    public int GetRewardCount(Rewards reward)
    {
        int result = 0;
        switch (reward)
        {
            case Rewards.Diamond:
                result = Random.Range(diamondBaseMin, diamondBaseMax);
                break;
            case Rewards.Coin:
                result = Random.Range(coinBaseMin, coinBaseMax);
                break;
            case Rewards.Gold:
                result = Random.Range(goldBaseMin, goldBaseMax);
                break;
            case Rewards.Aluminum:
                result = Random.Range(aluminumBaseMin, aluminumBaseMax);
                break;
            case Rewards.Copper:
                result = Random.Range(copperBaseMin, copperBaseMax);
                break;
            case Rewards.Brass:
                result = Random.Range(brassBaseMin, brassBaseMax);
                break;
            case Rewards.Titanium:
                result = Random.Range(titaniumBaseMin, titaniumBaseMax);
                break;
        }
        return result;
    }
}
