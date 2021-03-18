using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalVariables;

public class PlanetRewardsView : MonoBehaviour
{
    Player player;

    [SerializeField] GameObject rewardsGrid;

    [Header("Grid Prefabs")]
    [SerializeField] GameObject diamondItem;
    [SerializeField] GameObject coinItem;
    [SerializeField] GameObject goldItem;
    [SerializeField] GameObject aluminumItem;
    [SerializeField] GameObject copperItem;
    [SerializeField] GameObject brassItem;
    [SerializeField] GameObject titaniumItem;
    [Header("Reward Prefabs")]
    [SerializeField] GameObject diamondReward;
    [SerializeField] GameObject coinReward;
    [SerializeField] GameObject goldReward;
    [SerializeField] GameObject aluminumReward;
    [SerializeField] GameObject copperReward;
    [SerializeField] GameObject brassReward;
    [SerializeField] GameObject titaniumReward;

    // Coordinates of where to place reward and parent to hold them
    [SerializeField] GameObject rewards;

    [SerializeField] Text rewardCountText;

    int collectSpeed = 300;

    int diamondCount;
    int coinCount;
    int goldCount;
    int aluminumCount;
    int copperCount;
    int brassCount;
    int titaniumCount;

    List<Rewards> rewardTypes = new List<Rewards>();

    GameObject currentReward;

    bool rewardDropped;
    int currentRewardIndex = 0;

    void Start()
    {
        player = FindObjectOfType<Player>();

        player.LoadPlayer();

        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (rewardDropped)
        {
            MoveRewardToGrid();
        }
    }

    private void MoveRewardToGrid()
    {
        // Move to grid
        currentReward.transform.position = Vector2.MoveTowards(currentReward.transform.position, rewardsGrid.transform.position, collectSpeed * Time.deltaTime);
        
        // When reward reached the grid destory it
        if (currentReward.transform.position == rewardsGrid.transform.position)
        {
            Destroy(currentReward);
            CreateGridReward();
            rewardDropped = false;
            if (currentRewardIndex < rewardTypes.Count - 1)
            {
                currentRewardIndex++;
                ShowOpenedReward(rewardTypes[currentRewardIndex]);
            }
        }
    }

    private void CreateGridReward()
    {
        GameObject gridReward;
        switch (rewardTypes[currentRewardIndex])
        {
            case Rewards.Diamond:
                gridReward = Instantiate(diamondItem, rewardsGrid.transform.position, Quaternion.identity);
                gridReward.transform.Find("Count").GetComponent<Text>().text = diamondCount.ToString();
                break;
            case Rewards.Coin:
                gridReward = Instantiate(coinItem, rewardsGrid.transform.position, Quaternion.identity);
                gridReward.transform.Find("Count").GetComponent<Text>().text = coinCount.ToString();
                break;
            case Rewards.Gold:
                gridReward = Instantiate(goldItem, rewardsGrid.transform.position, Quaternion.identity);
                gridReward.transform.Find("Count").GetComponent<Text>().text = goldCount.ToString();
                break;
            case Rewards.Aluminum:
                gridReward = Instantiate(aluminumItem, rewardsGrid.transform.position, Quaternion.identity);
                gridReward.transform.Find("Count").GetComponent<Text>().text = aluminumCount.ToString();
                break;
            case Rewards.Copper:
                gridReward = Instantiate(copperItem, rewardsGrid.transform.position, Quaternion.identity);
                gridReward.transform.Find("Count").GetComponent<Text>().text = copperCount.ToString();
                break;
            case Rewards.Brass:
                gridReward = Instantiate(brassItem, rewardsGrid.transform.position, Quaternion.identity);
                gridReward.transform.Find("Count").GetComponent<Text>().text = brassCount.ToString();
                break;
            case Rewards.Titanium:
            default:
                gridReward = Instantiate(titaniumItem, rewardsGrid.transform.position, Quaternion.identity);
                gridReward.transform.Find("Count").GetComponent<Text>().text = titaniumCount.ToString();
                break;
        }
        gridReward.transform.SetParent(rewardsGrid.transform);
    }

    public void SetPlanetData((int, int, int, int, int, int, int) planetData)
    {
        gameObject.SetActive(true);

        diamondCount = planetData.Item1;
        coinCount = planetData.Item2;
        goldCount = planetData.Item3;
        aluminumCount = planetData.Item4;
        copperCount = planetData.Item5;
        brassCount = planetData.Item6;
        titaniumCount = planetData.Item7;

        if (diamondCount > 0)
        {
            rewardTypes.Add(Rewards.Diamond);
        }
        if (coinCount > 0)
        {
            rewardTypes.Add(Rewards.Coin);
        }
        if (goldCount > 0)
        {
            rewardTypes.Add(Rewards.Gold);
        }
        if (aluminumCount > 0)
        {
            rewardTypes.Add(Rewards.Aluminum);
        }
        if (copperCount > 0)
        {
            rewardTypes.Add(Rewards.Copper);
        }
        if (brassCount > 0)
        {
            rewardTypes.Add(Rewards.Brass);
        }
        if (titaniumCount > 0)
        {
            rewardTypes.Add(Rewards.Titanium);
        }
        if (currentRewardIndex < rewardTypes.Count)
        {
            ShowOpenedReward(rewardTypes[currentRewardIndex]);
        }
    }

    private void ShowOpenedReward(Rewards reward)
    {
        // Open the give reward
        switch (reward)
        {
            case Rewards.Diamond:
                currentReward = Instantiate(diamondReward, rewards.transform.position, Quaternion.identity);
                rewardCountText.text = diamondCount.ToString();
                break;
            case Rewards.Coin:
                currentReward = Instantiate(coinReward, rewards.transform.position, Quaternion.identity);
                rewardCountText.text = coinCount.ToString();
                break;
            case Rewards.Gold:
                currentReward = Instantiate(goldReward, rewards.transform.position, Quaternion.identity);
                rewardCountText.text = goldCount.ToString();
                break;
            case Rewards.Aluminum:
                currentReward = Instantiate(aluminumReward, rewards.transform.position, Quaternion.identity);
                rewardCountText.text = aluminumCount.ToString();
                break;
            case Rewards.Copper:
                currentReward = Instantiate(copperReward, rewards.transform.position, Quaternion.identity);
                rewardCountText.text = copperCount.ToString();
                break;
            case Rewards.Brass:
                currentReward = Instantiate(brassReward, rewards.transform.position, Quaternion.identity);
                rewardCountText.text = brassCount.ToString();
                break;
            case Rewards.Titanium:
            default:
                currentReward = Instantiate(titaniumReward, rewards.transform.position, Quaternion.identity);
                rewardCountText.text = titaniumCount.ToString();
                break;
        }
        currentReward.transform.SetParent(rewards.transform);
        // Start moving towards the grid
        rewardDropped = true;
    }

    // Tap anywhere while reward is shown to close immediately
    public void CloseRewardView()
    {
        // Remove all items from grid and appearing rewards
        for (int i = 0; i < rewardsGrid.transform.childCount; i++)
        {
            Destroy(rewardsGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < rewards.transform.childCount; i++)
        {
            Destroy(rewards.transform.GetChild(i).gameObject);
        }

        gameObject.SetActive(false);
    }
}
