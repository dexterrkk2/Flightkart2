using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public string nextLevel;
    public int maxCheckpoints;
    public int laps;
    public float startTime;
    public float timeTaken;
    private void Awake()
    {
        startTime = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerMvmt player = other.GetComponent<PlayerMvmt>();
            if (player.score >= (maxCheckpoints * laps))
            {
                GameManager.instance.completeLevelUI.SetActive(true);
                timeTaken = Time.time - startTime;
                LeaderBoard.instance.SetLeaderboardEntry(timeTaken);
                Invoke("ChangeScene", 10f);
            }
        }
       
    }
    public void ChangeScene()
    {
            Networkmanager.instance.ChangeScene(nextLevel);
    }
}
