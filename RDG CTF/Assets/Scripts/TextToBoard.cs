using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextToBoard : MonoBehaviour
{
    public Text LeaderboardTeams;

    public Text LastSubmitTaskname;
    public Text LastSubmitTeamname;
    public Text LastSubmitScore;

    public API APIobj;

    void Start()
    {
        StartCoroutine(InitCoroutine());
        StartCoroutine(UpdateCoroutine());
    }
    
    IEnumerator InitCoroutine()
    {
        Debug.Log("( From Leaderboard Text Field obj) Started Coroutine at timestamp :" + Time.time + "\nWaiting for API to load");
    
        yield return new WaitForSeconds(3);
        
        LeaderboardTeams.text = "";
        for (int i = 0; i < 10; i++) {
            LeaderboardTeams.text += (API.LeaderboardData.Teams[i].teamname + "  " + API.LeaderboardData.Teams[i].score + '\n');
        }

        LastSubmitTaskname.text = API.SubmitData.taskname;
        LastSubmitTeamname.text = API.SubmitData.teamname;
        LastSubmitScore.text = API.SubmitData.score.ToString();

        Debug.Log("Finished Coroutine at timestamp : " + Time.time + "\nHopefully API fields is loaded now");

    }

    IEnumerator UpdateCoroutine() //call every n seconds
    {
        Debug.Log("Updating info... Trying to get new data from API...");

        APIobj.UpdateData();

        // LeaderboardTeams.text = "";
        // for (int i = 0; i < 10; i++) {
        //     LeaderboardTeams.text += (API.LeaderboardData.Teams[i].teamname + "  " + API.LeaderboardData.Teams[i].score + '\n');
        // }

        // LastSubmitTaskname.text = API.SubmitData.taskname;
        // LastSubmitTeamname.text = API.SubmitData.teamname;
        // LastSubmitScore.text = API.SubmitData.score.ToString();
        
        Debug.Log("Info updated, init sleep for 5 seconds...");

        yield return new WaitForSeconds(5);
        StartCoroutine(UpdateCoroutine());
    }
}
