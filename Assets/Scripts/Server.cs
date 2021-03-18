﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using static Utilities;

public class Server : MonoBehaviour
{
    public class Header
    {
        public string deviceId;
        public string deviceOS;
    }

    // Change player name
    public class PlayerName
    {
        public string playerName;
    }
    // Each row of leaderboard
    public class LeaderboardItem
    {
        public string playerName;
        public int rank;
        public int currentLaserIndex;
        public int currentShipIndex;
    }

    // Video Link
    public class VideoJson
    {
        public string video; // link to video
        public string name; // product title
        public string website; // link to follow on click
    }

    public class PlayerData
    {
        public int coins;
        public int diamonds;
        public int gold;
        public int aluminum;
        public int copper;
        public int brass;
        public int titanium;
        public int power;
        public int nextLevelIndex;
        public int currentLaserIndex;
        public int currentShipIndex;
        public int currentPlanetIndex;
        public List<int> allLasers;
        public List<int> allPlanets;
        public List<int> allShips;
    }

    // LOCAL TESTING
    //string abboxAdsApi = "http://localhost:5002";
    string ballAndWallsApi = "http://localhost:5001/galaxyPirates";

    // STAGING
    //string abboxAdsApi = "https://staging.ads.abbox.com";
    //string ballAndWallsApi = "https://staging.api.abboxgames.com/galaxyPirates";

    // PRODUCTION
    string abboxAdsApi = "https://ads.abbox.com";
    //string ballAndWallsApi = "https://api.abboxgames.com/galaxyPirates";

    List<LeaderboardItem> top = new List<LeaderboardItem>();
    List<LeaderboardItem> before = new List<LeaderboardItem>();
    List<LeaderboardItem> after = new List<LeaderboardItem>();
    LeaderboardItem you = new LeaderboardItem();

    // To send response to corresponding files
    [SerializeField] MainStatus mainStatus;
    // This is to call the functions in leaderboard scene
    [SerializeField] LeaderboardStatus leaderboardStatus;

    Header header = new Header();

    void Awake()
    {
        header.deviceId = SystemInfo.deviceUniqueIdentifier;
        header.deviceOS = SystemInfo.operatingSystem;
    }

    /* ---------- LOAD SCENE ---------- */

    // CREATE NEW PLAYER
    public void CreatePlayer(Player player)
    {
        string playerUrl = ballAndWallsApi + "/player";

        string playerDataUrl = ballAndWallsApi + "/data";

        PlayerData playerData = new PlayerData();
        playerData.coins = player.coins;
        playerData.diamonds = player.diamonds;
        playerData.gold = player.gold;
        playerData.aluminum = player.aluminum;
        playerData.copper = player.copper;
        playerData.brass = player.brass;
        playerData.titanium = player.titanium;
        playerData.power = player.power;
        playerData.nextLevelIndex = player.nextLevelIndex;
        playerData.currentShipIndex = player.currentShipIndex;
        playerData.currentLaserIndex = player.currentLaserIndex;
        playerData.currentPlanetIndex = player.currentPlanetIndex;
        playerData.allPlanets = new List<int>();
        playerData.allShips = new List<int>();
        playerData.allLasers = new List<int>();

        player.allPlanets.ForEach(p =>
        {
            playerData.allPlanets.Add(p);
        });

        player.allShips.ForEach(s =>
        {
            playerData.allShips.Add(s);
        });

        player.allLasers.ForEach(l =>
        {
            playerData.allLasers.Add(l);
        });

        string playerDataJson = JsonUtility.ToJson(playerData);

        StartCoroutine(CreatePlayerCoroutine(playerUrl, playerDataJson));
    }

    // This one is called when the game is just launched
    // Either create a new player or move on
    private IEnumerator CreatePlayerCoroutine(string url, string playerData)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(playerData);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest webRequest =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        string message = JsonUtility.ToJson(header);
        string headerMessage = BuildHeaders(message);
        webRequest.SetRequestHeader("token", headerMessage);

        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError)
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Set the error received from creating a player
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Make the success actions received from creating a player
            mainStatus.CreatePlayerSuccess();
        }
    }

    /* ---------- MAIN SCENE ---------- */

    // SAVE PLAYER DATA
    public void SavePlayerData(Player player)
    {
        string playerDataUrl = ballAndWallsApi + "/data";

        PlayerData playerData = new PlayerData();
        playerData.coins = player.coins;
        playerData.diamonds = player.diamonds;
        playerData.gold = player.gold;
        playerData.aluminum = player.aluminum;
        playerData.copper = player.copper;
        playerData.brass = player.brass;
        playerData.titanium = player.titanium;
        playerData.power = player.power;
        playerData.nextLevelIndex = player.nextLevelIndex;
        playerData.currentShipIndex = player.currentShipIndex;
        playerData.currentLaserIndex = player.currentLaserIndex;
        playerData.currentPlanetIndex = player.currentPlanetIndex;
        playerData.allPlanets = new List<int>();
        playerData.allShips = new List<int>();
        playerData.allLasers = new List<int>();

        player.allPlanets.ForEach(p =>
        {
            playerData.allPlanets.Add(p);
        });

        player.allShips.ForEach(s =>
        {
            playerData.allShips.Add(s);
        });

        player.allLasers.ForEach(l =>
        {
            playerData.allLasers.Add(l);
        });

        string playerDataJson = JsonUtility.ToJson(playerData);

        StartCoroutine(SavePlayerDataCoroutine(playerDataUrl, playerDataJson));
    }

    private IEnumerator SavePlayerDataCoroutine(string url, string playerData)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(playerData);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest webRequest =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        string message = JsonUtility.ToJson(header);
        string headerMessage = BuildHeaders(message);
        webRequest.SetRequestHeader("token", headerMessage);

        yield return webRequest.SendWebRequest();
        //if (webRequest.isNetworkError)
        //{
        //    Debug.Log(webRequest.downloadHandler.text);
        //    // Set the error received from creating a player
        //}
        //else
        //{
        //    Debug.Log(webRequest.downloadHandler.text);
        //    // Make the success actions received from creating a player
        //}
    }

    public void GetVideoLink()
    {
        string videoUrl = abboxAdsApi + "/api/v1/videos";
        StartCoroutine(GetAdLinkCoroutine(videoUrl));
    }

    // This one is for TV in main scene
    // Get the latest video link, for now in general, in future personal based on the DeviceId
    private IEnumerator GetAdLinkCoroutine(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string message = JsonUtility.ToJson(header);
            string headerMessage = BuildHeaders(message);
            webRequest.SetRequestHeader("token", headerMessage);

            // Send request and wait for the desired response.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Set the error of video link received from the server
                mainStatus.SetVideoLinkError(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Parse the response from server to retrieve all data fields
                VideoJson videoInfo = JsonUtility.FromJson<VideoJson>(webRequest.downloadHandler.text);

                // Set the video link received from the server
                mainStatus.SetVideoLinkSuccess(videoInfo);
            }
        }
    }

    /* ---------- LEADERBOARD SCENE ---------- */

    // CHANGE PLAYER NAME
    public void ChangePlayerName(string playerName)
    {
        string nameUrl = ballAndWallsApi + "/name";

        PlayerName nameObject = new PlayerName();
        nameObject.playerName = playerName;

        string nameJson = JsonUtility.ToJson(nameObject);

        StartCoroutine(ChangeNameCoroutine(nameUrl, nameJson));
    }

    // CHANGE PLAYER NAME
    private IEnumerator ChangeNameCoroutine(string url, string playerName)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(playerName);
        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();
        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest webRequest =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        string message = JsonUtility.ToJson(header);
        string headerMessage = BuildHeaders(message);
        webRequest.SetRequestHeader("token", headerMessage);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Set the error received from creating a player
            //leaderboardStatus.ChangeNameError();
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            // Make the success actions received from creating a player
            //leaderboardStatus.ChangeNameSuccess();
        }
    }


    // GET LEADERBOARD LIST
    public void GetLeaderboard()
    {
        string leaderboardUrl = ballAndWallsApi + "/leaderboard";
        StartCoroutine(LeaderboardCoroutine(leaderboardUrl));
    }

    // Get leaderboard data and populate it into the scroll list
    private IEnumerator LeaderboardCoroutine(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            string message = JsonUtility.ToJson(header);
            string headerMessage = BuildHeaders(message);
            webRequest.SetRequestHeader("token", headerMessage);

            // Send request and wait for the desired response.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                // Set the error of leaderboard data received from the server
                Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                // Parse the response from server to retrieve all data fields
                PopulateLeaderboardData(webRequest.downloadHandler.text);
            }
        }
    }

    private void PopulateLeaderboardData(string jsonData)
    {
        // Clear the lists incase they already had data in them
        top.Clear();
        before.Clear();
        after.Clear();
        // Extract string arrays of top, before, after and stirng of you data
        string[] topData = JsonHelper.GetJsonObjectArray(jsonData, "top");
        string[] beforeData = JsonHelper.GetJsonObjectArray(jsonData, "before");
        string youData = JsonHelper.GetJsonObject(jsonData, "you");
        string[] afterData = JsonHelper.GetJsonObjectArray(jsonData, "after");

        if (topData != null)
        {
            // Parse top data to leaderboard item to populate the list
            for (int i = 0; i < topData.Length; i++)
            {
                LeaderboardItem item = JsonUtility.FromJson<LeaderboardItem>(topData[i]);
                top.Add(item);
            }
        }

        if (beforeData != null)
        {
            // Parse before data
            for (int i = 0; i < beforeData.Length; i++)
            {
                LeaderboardItem item = JsonUtility.FromJson<LeaderboardItem>(beforeData[i]);
                before.Add(item);
            }
        }

        // Parse you data
        you = JsonUtility.FromJson<LeaderboardItem>(youData);

        if (afterData != null)
        {
            // Parse after data
            for (int i = 0; i < afterData.Length; i++)
            {
                LeaderboardItem item = JsonUtility.FromJson<LeaderboardItem>(afterData[i]);
                after.Add(item);
            }
        }

        // Send leaderboard data to leaderboard scene
        leaderboardStatus.SetLeaderboardData(top, before, you, after);
    }
}

