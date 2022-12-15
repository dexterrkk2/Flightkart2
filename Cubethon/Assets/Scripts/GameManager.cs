using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public float postGameTime;
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;
    [Header("Players")]
    public string playerPrefabLocation; // path in Resources folder to the Player prefab
    public Transform[] spawnPoints; // array of all available spawn points
    public PlayerMvmt[] players; // array of all the players
    public float respawnTime;
    private int playersInGame; // number of players in the game
    // instance
    public static GameManager instance;
    void Awake()
    {
        // instance
        instance = this;
    }
    void Start()
    {
        players = new PlayerMvmt[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void ImInGame()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SpawnPlayer", RpcTarget.All);
        }
    }
    public void CheckWinCondition(int id)
    {
        //if (players[id - 1])
        //{
            //photonView.RPC("WinGame", RpcTarget.All, players, id);
       // }
    }
    [PunRPC]
    void WinGame(int playerId)
    {
        // set the UI to show who's won
        Invoke("GoBackToMenu", postGameTime);
    }
    void GoBackToMenu()
    {
        PhotonNetwork.LeaveRoom();
        Networkmanager.instance.ChangeScene("Menu");
    }
    [PunRPC]
    void SpawnPlayer()
    {
        // instantiate the player across the network
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        playerObj.GetComponent<PlayerMvmt>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
        score.instance.SetScore();
        Ranking.instance.Setup();
    }
    public PlayerMvmt GetPlayer(int playerId)
    {
        foreach (PlayerMvmt player in players)
        {
            if (player != null && player.id == playerId)
                return player;
        }
        return null;
    }
    public PlayerMvmt GetPlayer(GameObject playerObject)
    {
        foreach (PlayerMvmt player in players)
        {
            if (player != null && player.gameObject == playerObject)
                return player;
        }
        return null;
    }
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Gameover");
            Invoke("GoBackToMenu", postGameTime);
        }
    }
    void Restart()
    {
        Invoke("GoBackToMenu", postGameTime);
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }
}
