using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerMvmt player = other.GetComponent<PlayerMvmt>();
            player.score += 1;
            score.instance.SetScore();
            Ranking.instance.ChangeRankings();
        }
    }
}
