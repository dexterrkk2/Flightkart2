using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
public class Ranking : MonoBehaviour
{
    public PlayerMvmt[] players; // array of all the players
    public List<GameObject> PlayerPositionPrefabs = new List<GameObject>();
    public GameObject PlayerPositionPrefab;
    public static Ranking instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public void Setup()
    {
        for (int x = 0; x < GameManager.instance.players.Length; x++)
        {
            GameObject buttonObj = Instantiate(PlayerPositionPrefab, gameObject.transform);
            PlayerPositionPrefabs.Add(buttonObj);
        }
        ChangeRankings();
    }
    public void ChangeRankings()
    {
        players = GameManager.instance.players;
        players.OrderBy(x => x.score);
        int x = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            PlayerStanding playerText = PlayerPositionPrefabs[x].GetComponent<PlayerStanding>();
            playerText.playerName.text = player.NickName;
            playerText.playerRanking.text = (x+1).ToString();
            x++;
        }
    }
}
