using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
// Bring classes from Server
using static Server;

public class LeaderboardStatus : MonoBehaviour
{
    // To send player data to server
    private class PlayerJson
    {
        public string playerId;
        public PlayerData playerData;
    }

    Player player;
    Navigator navigator;
    Server server;

    [SerializeField] Text diamondsText;
    [SerializeField] Text coinsText;
    [SerializeField] GameObject diamondIcon;

    // Single line of leadersboard
    [SerializeField] GameObject leaderboardItemPrefab;
    [SerializeField] GameObject leaderboardItemTrippleDots;

    // This is for saving the name before it has been changed, so receive diamonds
    [SerializeField] GameObject changeNameGetDiamondsButton;
    // This is for saving the name after it has been changed
    [SerializeField] GameObject changeNameSaveButton;
    // This is the window where you change your name
    [SerializeField] GameObject changeNameView;
    // To set parent for leaderboard items
    [SerializeField] GameObject leaderboardScrollContent;
    // To scroll down to your position
    [SerializeField] GameObject leaderboardScrollbar;
    // To point at change name button
    [SerializeField] GameObject arrow;
    // To extract value of input field when save or get 3 diamonds is clicked
    [SerializeField] InputField nameInput;
    // All laser sprites
    [SerializeField] Sprite[] allLasers;
    // All ship names
    string[] allShips = { "Imperial Freighter", "Death Star", "Millennium Falcon", "Imperial Star Destroyer", "Stinger Mantis", "Razor Crest" };

    List<LeaderboardItem> before = new List<LeaderboardItem>();
    List<LeaderboardItem> after = new List<LeaderboardItem>();
    List<LeaderboardItem> top = new List<LeaderboardItem>();
    LeaderboardItem you = new LeaderboardItem();

    void Awake()
    {
        server = FindObjectOfType<Server>();
        navigator = FindObjectOfType<Navigator>();
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        //player.ResetPlayer();
        player.LoadPlayer();

        diamondsText.text = player.diamonds.ToString();
        coinsText.text = player.coins.ToString();

        // Hide point arrow until server has replied
        arrow.SetActive(false);

        SwapSaveButton();

        // Widen name input field and hide it
        changeNameView.transform.localScale = new Vector3(1, 1, 1);
        changeNameView.SetActive(false);

        server.GetLeaderboard();
    }

    private void SwapSaveButton()
    {
        // Fetch data from server and choose the button to show
        if (player.nameChanged)
        {
            // The name sent from the server
            arrow.SetActive(false);
            changeNameSaveButton.SetActive(true);
            changeNameGetDiamondsButton.SetActive(false);
        }
        else
        {
            arrow.SetActive(true);
            changeNameSaveButton.SetActive(false);
            changeNameGetDiamondsButton.SetActive(true);
        }
    }

    private void ScrollListToPlayer()
    {
        // Combine all the values from all three lists top, before, after
        int total = top.Count + before.Count + after.Count;
        // Increase by one for your rank if it is outside of top ten
        if (you.rank != 0)
        {
            total++;
        }
        // based on total find where to place the scroll
        if (total > 10)
        {
            // If total is greater than 10, it is safe to show the bottom 5 players to make sure you are also visible
            leaderboardScrollbar.GetComponent<Scrollbar>().value = 0.001f;
        }
        else
        {
            // If you are in the top ten, then if you are in top 5, show first 5 players to make sure you are also visible
            if (you.rank < 6)
            {
                leaderboardScrollbar.GetComponent<Scrollbar>().value = 0.999f;
            }
            else
            {
                // Otherwise your rank is in the range of 5-10, so it is safe to show middle 5 players to make sure you are also visible
                leaderboardScrollbar.GetComponent<Scrollbar>().value = 0.5f;
            }
        }
    }

    public void ChangeNameError()
    {
        // Repopulate leaderboard data
        server.GetLeaderboard();
    }

    public void ChangeNameSuccess()
    {
        // Repopulate leaderboard data
        server.GetLeaderboard();
    }

    public void SetLeaderboardData(List<LeaderboardItem> topData, List<LeaderboardItem> beforeData, LeaderboardItem youData, List<LeaderboardItem> afterData)
    {
        // Clear the lists incase they already had data in them
        foreach (Transform child in leaderboardScrollContent.transform)
        {
            Destroy(child.gameObject);
        }
        top.Clear();
        before.Clear();
        after.Clear();
        if (topData != null)
        {
            // Loop though top ten list provided by the server and add them to local list
            for (int i = 0; i < topData.Count; i++)
            {
                top.Add(topData[i]);
            }
        }
        if (beforeData != null)
        {
            // Loop though up to 3 players before you list provided by the server
            // and if they have not yet been added to the list add them
            for (int i = 0; i < beforeData.Count; i++)
            {
                if (!CheckIfExists(beforeData[i].rank))
                {
                    before.Add(beforeData[i]);
                }
            }
        }
        if (youData != null)
        {
            you.rank = youData.rank;
            // Check if your rank has already been added to the list if not add it
            if (!CheckIfExists(youData.rank))
            {
                you = youData;
                // Set the name in the change name field to prepopulate
                nameInput.text = youData.playerName;
            }
        }
        if (afterData != null)
        {
            // Loop though up to 3 players after you list provided by the server
            // and if they have not yet been added to the list add them
            for (int i = 0; i < afterData.Count; i++)
            {
                if (!CheckIfExists(afterData[i].rank))
                {
                    after.Add(afterData[i]);
                }
            }
        }

        BuildUpList();
    }

    // Loop through top ten, 3 before and 3 after lists to find if give data exists not to repeat
    private bool CheckIfExists(int rank)
    {
        if (CheckIfExistInTop(rank) ||
            CheckIfExistInBefore(rank) ||
            CheckIfExistInAfter(rank))
        {
            return true;
        }
        return false;
    }
    // Loop through the list of players ranked in top ten and see if iven data exists
    private bool CheckIfExistInTop(int rank)
    {
        for (int i = 0; i < top.Count; i++)
        {
            if (top[i].rank == rank)
            {
                return true;
            }
        }
        return false;
    }
    // Loop through the list of players ranked before you and see if iven data exists
    private bool CheckIfExistInBefore(int rank)
    {
        for (int i = 0; i < before.Count; i++)
        {
            if (before[i].rank == rank)
            {
                return true;
            }
        }
        return false;
    }
    // Loop through the list of players ranked after you and see if iven data exists
    private bool CheckIfExistInAfter(int rank)
    {
        for (int i = 0; i < after.Count; i++)
        {
            if (after[i].rank == rank)
            {
                return true;
            }
        }
        return false;
    }

    private void BuildUpList()
    {
        // Loop through top ten players and instantiate an item object
        top.ForEach(itemData =>
        {
            GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, transform.position, Quaternion.identity);
            // Set its parent to be scroll content, for scroll functionality to work properly
            leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);

            // Compare item from top ten with your rank incase you are in top ten
            if (itemData.rank == you.rank)
            {
                // Show frame around your entry
                ShowYourEntryFrame(leaderboardItem);
            }

            // Set its name component text to name from top list
            leaderboardItem.transform.Find("NameItem").Find("NameText").GetComponent<Text>().text = itemData.playerName;
            // Set its rank component text to rank from top list converted to string
            leaderboardItem.transform.Find("NameItem").Find("RankText").GetComponent<Text>().text = itemData.rank.ToString();
            // Set its ship component text to item's ship name
            leaderboardItem.transform.Find("NameItem").Find("ShipText").GetComponent<Text>().text = allShips[itemData.currentShipIndex];
            // Set its laser icon based on data from server and indexes of lasers
            leaderboardItem.transform.Find("LaserItem").Find("Laser").GetComponent<Image>().sprite = allLasers[itemData.currentLaserIndex];

            if (itemData.rank == 1)
            {
                ShowGoldEntry(leaderboardItem);
            }
            else if (itemData.rank == 2)
            {
                ShowSilverEntry(leaderboardItem);
            }
            else if (itemData.rank == 3)
            {
                ShowBronzeEntry(leaderboardItem);
            }
        });

        // Add tripple dots after top ten only if your rank is > 14,
        // since at 14 the the top ten and 3 before you become continuous, so no need for dots in between
        if (you.rank > 14)
        {
            CreateTrippleDotsEntry();
        }

        // Loop through before players and instantiate an item object
        before.ForEach(itemData =>
        {
            GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, transform.position, Quaternion.identity);
            // Set its parent to be scroll content, for scroll functionality to work properly
            leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
            SetItemEntry(leaderboardItem, itemData);
        });

        // Create your entry item only if your rank is not in top ten
        // 0 is assigned by default if there is no value

        if (you.rank > 10)
        {
            CreateYourEntry();
        }

        // Loop through after players and instantiate an item object
        after.ForEach(itemData =>
        {
            GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, transform.position, Quaternion.identity);
            // Set its parent to be scroll content, for scroll functionality to work properly
            leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
            SetItemEntry(leaderboardItem, itemData);
        });

        CreateTrippleDotsEntry();

        // Add the scroll value after all the data is populated
        ScrollListToPlayer();
    }

    private void ShowYourEntryFrame(GameObject item)
    {
        // Show leaderboard frame white for your entry
        item.transform.Find("LaserItem").Find("LaserFrame").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        item.transform.Find("NameItem").Find("NameFrame").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    private void ShowGoldEntry(GameObject item)
    {
        // Show leaderboard frame gold color for top 1 entry
        item.transform.Find("LaserItem").Find("LaserFrame").GetComponent<Image>().color = new Color32(254, 209, 0, 255);
        item.transform.Find("NameItem").Find("NameFrame").GetComponent<Image>().color = new Color32(254, 209, 0, 255);
    }

    private void ShowSilverEntry(GameObject item)
    {
        // Show leaderboard frame silver color for top 1 entry
        item.transform.Find("LaserItem").Find("LaserFrame").GetComponent<Image>().color = new Color32(211, 211, 211, 255);
        item.transform.Find("NameItem").Find("NameFrame").GetComponent<Image>().color = new Color32(211, 211, 211, 255);
    }

    private void ShowBronzeEntry(GameObject item)
    {
        // Show leaderboard frame bronze color for top 1 entry
        item.transform.Find("LaserItem").Find("LaserFrame").GetComponent<Image>().color = new Color32(205, 127, 50, 255);
        item.transform.Find("NameItem").Find("NameFrame").GetComponent<Image>().color = new Color32(205, 127, 50, 255);
    }

    private void CreateTrippleDotsEntry()
    {
        // Create tripple dots to separate different lists
        GameObject leaderboardItem = Instantiate(leaderboardItemTrippleDots, transform.position, Quaternion.identity);
        // Set its parent to be scroll content, for scroll functionality to work properly
        leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
    }

    private void SetItemEntry(GameObject item, LeaderboardItem value)
    {
        // Set its name component text to item name
        item.transform.Find("NameItem").Find("NameText").GetComponent<Text>().text = value.playerName;
        // Set its rank component text to item rank
        item.transform.Find("NameItem").Find("RankText").GetComponent<Text>().text = value.rank.ToString();
        // Set its ship component text to item ship name
        item.transform.Find("NameItem").Find("ShipText").GetComponent<Text>().text = allShips[value.currentShipIndex];
        // Set its laser icon based on data from server and indexes of lasers
        item.transform.Find("LaserItem").Find("Laser").GetComponent<Image>().sprite = allLasers[value.currentLaserIndex];
    }

    private void CreateYourEntry()
    {
        // Create tripple dots to separate different lists
        GameObject leaderboardItem = Instantiate(leaderboardItemPrefab, transform.position, Quaternion.identity);
        // Set its parent to be scroll content, for scroll functionality to work properly
        leaderboardItem.transform.SetParent(leaderboardScrollContent.transform);
        // Show frame around your entry
        ShowYourEntryFrame(leaderboardItem);
        SetItemEntry(leaderboardItem, you);
    }

    public void ClickBackButton()
    {
        navigator.LoadMainScene();
    }

    public void ClickChangeNameButton()
    {
        changeNameView.SetActive(true);
        arrow.SetActive(false);
    }

    // This is for invisible button that covers the rest of the screen when modal is open
    public void CloseChangeName()
    {
        changeNameView.SetActive(false);
    }

    // Save name
    public void ClickSaveName()
    {
        server.ChangePlayerName(nameInput.text);
        if (player.playerName.Length < 2)
        {
            player.playerName = nameInput.text;
            player.diamonds += 3;
            diamondIcon.GetComponent<TriggerAnimation>().Trigger();
            player.nameChanged = true;
            player.SavePlayer();

            diamondsText.text = player.diamonds.ToString();
        }

        CloseChangeName();
    }
}
