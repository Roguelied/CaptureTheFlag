using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    private static string SubmitURL = "https://65c465f2dae2304e92e2883e.mockapi.io/rdgctf/v1/submitUpdate";
    private static string LeaderboardURL = "https://65c465f2dae2304e92e2883e.mockapi.io/rdgctf/v1/leaderboard";

    public static class SubmitData
    {
        public static string taskname = "task";
        public static string teamname = "unknown";
        public static uint score = 0;
    }

    
    public static class LeaderboardData
    {
        public class team
        {
            public string teamname = "unknown";
            public uint score = 0;
            public team(string teamname, uint score)
            {
                this.teamname = teamname;
                this.score = score;
            }
        }
        public static team[] Teams = new team[10];
    }
    
    
    void Start()
    {
        StartCoroutine(GetLeaderboardData());
        StartCoroutine(GetSubmitData());
    }

    public void UpdateData() 
    {
        StartCoroutine(GetLeaderboardData());
        StartCoroutine(GetSubmitData());
    }

    public static IEnumerator GetSubmitData()
    {
        using(UnityWebRequest request = UnityWebRequest.Get(SubmitURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { Debug.LogError(request.error); }
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode jsonArray = SimpleJSON.JSON.Parse(json);

                SubmitData.taskname = jsonArray[0]["taskname"];
                SubmitData.teamname = jsonArray[0]["teamname"];
                SubmitData.score = uint.Parse(jsonArray[0]["score"]);
                
            }
        }
    }
    
    public static IEnumerator GetLeaderboardData()
    {
        using(UnityWebRequest request = UnityWebRequest.Get(LeaderboardURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { Debug.LogError(request.error); }
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode jsonArray = SimpleJSON.JSON.Parse(json);
                
                //Debug.LogError(jsonArray);
                
                for (int i = 0; i < 10; i++)
                {
                    LeaderboardData.team newTeam = new(jsonArray[i]["teamname"], uint.Parse(jsonArray[i]["score"]));
                    LeaderboardData.Teams[i] = newTeam;
                    //Debug.LogError(LeaderboardData.Teams[i].teamname + "   " + LeaderboardData.Teams[i].score);
                }
            }
        }
    }
}
    