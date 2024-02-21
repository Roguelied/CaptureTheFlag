using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public static class API 
{
    /*
     
     Ссылки заменить на апи стфа, пока стоит локалка для тестов
     https://codeby.games/game_api/rdg/scoreboard/teams?limit=10
     https://codeby.games/game_api/rdg/tasks/last_submit
     
     */

    private static string SubmitURL = "http://127.0.0.1:5500/lastsubmit.json";
    private static string LeaderboardURL = "http://127.0.0.1:5500/leaderboards.json";

    
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
    
 
    public static IEnumerator GetSubmitData()
    {
        Application.runInBackground = true;
        
        
        //request.SetRequestHeader("Authorization", APIKey);
        //or request.SetRequestHeader("Authorization", "TOKEN " + yourAPIKey);
        //or UnityWebRequest request = UnityWebRequest.Get(AVWXURL + "?token="+yourAPIKey); 
        
        
        using(UnityWebRequest request = UnityWebRequest.Get(SubmitURL))
        {
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.ConnectionError) { Debug.LogError(request.error); }
            else
            {
                string json = request.downloadHandler.text;
                
                if (string.IsNullOrEmpty(json)) { Debug.LogError("Json string is null or empty"); }
                else
                {
                    SimpleJSON.JSONNode jsonArray = SimpleJSON.JSON.Parse(json);

                    SubmitData.taskname = jsonArray["data"][0]["task"]["title"];
                    SubmitData.teamname = jsonArray["data"][0]["team"]["name"];
                    SubmitData.score = uint.Parse(jsonArray["data"][0]["task"]["points"]);
                    
                }
            }
        }
    }
    
    
    public static IEnumerator GetLeaderboardData()
    {
        Application.runInBackground = true;
        
        
        //request.SetRequestHeader("Authorization", APIKey);
        //or request.SetRequestHeader("Authorization", "TOKEN " + yourAPIKey);
        
        
        using(UnityWebRequest request = UnityWebRequest.Get(LeaderboardURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError) { Debug.LogError(request.error); }
            else
            {
                string json = request.downloadHandler.text;
                
                if (string.IsNullOrEmpty(json)) { Debug.LogError("Json string is null or empty"); }
                else
                {
                    SimpleJSON.JSONNode jsonArray = SimpleJSON.JSON.Parse(json);
                    for (var i = 0; i < 10; i++)
                    {
                        LeaderboardData.team newTeam = new(jsonArray["data"][i]["team"]["name"], 
                                                        uint.Parse(jsonArray["data"][i]["points"]));
                        LeaderboardData.Teams[i] = newTeam;
                    }
                }
            }
        }
    }
}
    