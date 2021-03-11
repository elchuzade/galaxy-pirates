using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using static GlobalVariables;

public class OpenedChestView : MonoBehaviour
{
    Player player;

    [SerializeField] GameObject redChest;
    [SerializeField] GameObject purpleChest;
    [SerializeField] GameObject blueChest;

    [SerializeField] GameObject diamondReward;
    [SerializeField] GameObject coinReward;
    [SerializeField] GameObject goldReward;
    [SerializeField] GameObject silverReward;
    [SerializeField] GameObject bronzeReward;
    [SerializeField] GameObject brassReward;
    [SerializeField] GameObject titaniumReward;

    List<Rewards> allRewards;

    void Start()
    {
        player = FindObjectOfType<Player>();

        player.LoadPlayer();

        CloseAllRewards();

        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
    }

    public void OpenChestReward(ChestColors chestColor)
    {
        switch (chestColor)
        {
            case ChestColors.Red:
                allRewards = redChest.GetComponent<ChestItem>().GetAllRewards();
                break;
            case ChestColors.Purple:
                allRewards = purpleChest.GetComponent<ChestItem>().GetAllRewards();
                break;
            case ChestColors.Blue:
            default:
                allRewards = blueChest.GetComponent<ChestItem>().GetAllRewards();
                break;
        }

        int rewardIndex = Random.Range(0, allRewards.Count);
        Rewards reward = allRewards[rewardIndex];

        ShowOpenedReward(reward);
    }

    private void ShowOpenedReward(Rewards reward)
    {
        // Open the give reward
        switch (reward)
        {
            case Rewards.Diamond:
                diamondReward.SetActive(true);
                break;
            case Rewards.Coin:
                coinReward.SetActive(true);
                break;
            case Rewards.Gold:
                goldReward.SetActive(true);
                break;
            case Rewards.Silver:
                silverReward.SetActive(true);
                break;
            case Rewards.Bronze:
                bronzeReward.SetActive(true);
                break;
            case Rewards.Brass:
                brassReward.SetActive(true);
                break;
            case Rewards.Titanium:
            default:
                titaniumReward.SetActive(true);
                break;
        }
        StartCoroutine(CloseChestOpenedView(2));
    }

    // Automatically close the reward from chest after 2 seconds
    private IEnumerator CloseChestOpenedView(float time)
    {
        yield return new WaitForSeconds(time);

        CloseRewardView();
    }

    // Tap anywhere while reward is shown to close immediately
    public void CloseRewardView()
    {
        CloseAllRewards();
        gameObject.SetActive(false);
    }

    private void CloseAllRewards()
    {
        diamondReward.SetActive(false);
        coinReward.SetActive(false);
        goldReward.SetActive(false);
        silverReward.SetActive(false);
        bronzeReward.SetActive(false);
        brassReward.SetActive(false);
        titaniumReward.SetActive(false);
    }
}
