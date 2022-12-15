using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Pickup : MonoBehaviour
{
    public int numPickups;
    public List<string> pickuptypes;
    public int slowModifier;
    public List<string> misslePrefabs;
    // Start is called before the first frame update
    void Start()
    {
        numPickups = pickuptypes.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            int randPowerup = Random.Range(0, numPickups-1);
            Items(pickuptypes[randPowerup]);
        }
    }
    void Items(string itemType)
    {
        if (itemType == "missle")
        {
            int randMissle = Random.Range(0, misslePrefabs.Count - 1);
            PlayerMvmt.instance.SetMissle(misslePrefabs[randMissle]);
        }
        else if (itemType == "slow")
        {
            int randPlayer = Random.Range(0, GameManager.instance.players.Length-1);
            GameManager.instance.players[randPlayer].forwardForce /= slowModifier;
        }
        PhotonNetwork.Destroy(gameObject);
    }
}
