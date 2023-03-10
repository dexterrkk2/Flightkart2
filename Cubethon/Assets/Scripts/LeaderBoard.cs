using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
public class LeaderBoard : MonoBehaviour
{
    public GameObject leaderboardCanvas;
    public GameObject[] leaderboardEntries;

    public static LeaderBoard instance;
    private void Awake()
    {
        instance = this;
        DisplayLeaderboard();
    }
    public void OnLoggedIn()
    {
        leaderboardCanvas.SetActive(true);
        DisplayLeaderboard();
    }
    public void DisplayLeaderboard()
    {
        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest
        {
            StatisticName = "FastestTime",
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest,
            result => UpdateLeaderboardUI(result.Leaderboard),
            error => Debug.Log(error.ErrorMessage)
        );
    }
    private void UpdateLeaderboardUI(List<PlayerLeaderboardEntry> leaderboard)
    {
        for (int x = 0; x < leaderboardEntries.Length; x++)
        {
            leaderboardEntries[x].SetActive(x < leaderboard.Count);

            if (x >= leaderboard.Count)
            {
                continue;
            }
            leaderboardEntries[x].transform.Find("PlayerName").GetComponent<TextMeshProUGUI>().text = (leaderboard[x].Position + 1) + "." + leaderboard[x].DisplayName;
            leaderboardEntries[x].transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = ((float)leaderboard[x].StatValue).ToString();
        }
    }
    public void SetLeaderboardEntry(float newScore)
    {
        bool useLegacyMethod = false;
        if (useLegacyMethod)
        {
            ExecuteCloudScriptRequest request = new ExecuteCloudScriptRequest
            {
                FunctionName = "UpdateHighscore",
                FunctionParameter = new { score = newScore }
            };
            PlayFabClientAPI.ExecuteCloudScript(request,
                result =>
                {
                    Debug.Log(result);
                    //Debug.Log("SUCCESS");
                    //Debug.Log(result.FunctionName);
                    //Debug.Log(result.FunctionResult);
                    //Debug.Log(result.FunctionResultTooLarge);
                    //Debug.Log(result.Error);
                    DisplayLeaderboard();
                    Debug.Log(result.ToJson());
                },
                error =>
                {
                    Debug.Log(error.ErrorMessage);
                    Debug.Log("ERROR");
                }
            );
        }
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
            {
                // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate { StatisticName = "FastestTime", Value= (int)newScore },
                }
            },
            result => { Debug.Log("Score to submit: " + newScore); },
            error => { Debug.LogError(error.GenerateErrorReport()); }
        );
    }
}
