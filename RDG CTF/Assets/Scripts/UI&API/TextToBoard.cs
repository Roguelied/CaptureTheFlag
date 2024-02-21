using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextToBoard : MonoBehaviour
{
    public Text LeaderboardTeams;

    public Text LastSubmitTaskname;
    public Text LastSubmitTeamname;
    public Text LastSubmitScore;

    
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(UpdateDataCoroutine());
    }
    
    IEnumerator UpdateDataCoroutine()
    {
        
        while (true)
        {
            StartCoroutine(API.GetSubmitData());
            StartCoroutine(API.GetLeaderboardData());
        
            Debug.Log("From Leaderboard Text Field at: " + Time.time + "\nWaiting 2 seconds, updating info from API...");
        
            yield return new WaitForSeconds(2);
        
            LeaderboardTeams.text = "";
            for (var i = 0; i < 10; i++) {
                LeaderboardTeams.text += (API.LeaderboardData.Teams[i].teamname + "  " + API.LeaderboardData.Teams[i].score + '\n');
            }

            LastSubmitTaskname.text = API.SubmitData.taskname;
            LastSubmitTeamname.text = API.SubmitData.teamname;
            LastSubmitScore.text = API.SubmitData.score.ToString();

            Debug.Log("Finished Coroutine at: " + Time.time + "\nData should be loaded now");
        }
    }
}
