using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerMvmt : MonoBehaviourPunCallbacks
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    public float upwardForce = 50f;
    public float maxY;
    public float maxX;
    [Header("Photon")]
    public int id;
    public Player photonPlayer;
    public static PlayerMvmt instance;
    public string misslePrefab;
    public bool missleEquipped;
    public int acceleration;
    public int accelerationIncrement;
    public int score;
    private void Awake()
    {
        score = 0;
    }
    [PunRPC]
    public void Initialize(Player player)
    {
        Debug.Log("created");
        photonPlayer = player;
        id = player.ActorNumber;
        //photonPlayer = player;
        GameManager.instance.players[id - 1] = this;
        // if this isn't our local player, disable physics as that's
        // controlled by the user and synced to all other clients
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
        else
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        rb.MoveRotation(CameraController.instance.transform.rotation);
        Vector3 forward = new Vector3(0, 0, -transform.position.z).normalized;
        //rb.AddForceAtPosition(-transform.position, forward);
        //transform.position += forward * Time.deltaTime *10;
        rb.GetComponent<Rigidbody>().velocity = transform.forward * Time.deltaTime * (forwardForce + acceleration);
        if (Input.GetButton("Jump"))
        {
            if (photonView.IsMine)
            {
                if (missleEquipped)
                {
                    LaunchMissle();
                }
            }
        }
        if ( Input.GetKey("d"))
        {
            rb.GetComponent<Rigidbody>().velocity = transform.right * Time.deltaTime * forwardForce;
        }

        if (Input.GetKey("a"))
        {
            rb.GetComponent<Rigidbody>().velocity = -transform.right * Time.deltaTime * forwardForce;

        }

        if (Input.GetKey("w"))
        {
            rb.GetComponent<Rigidbody>().velocity = transform.up * Time.deltaTime * forwardForce;

        }

        if (Input.GetKey("s"))
        {
            rb.GetComponent<Rigidbody>().velocity = -transform.up * Time.deltaTime * forwardForce;
        }
        acceleration += accelerationIncrement;
    }
    public void SetMissle(string missletype)
    {
        misslePrefab = missletype;
        missleEquipped = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        acceleration = 0;
    }
    public void LaunchMissle()
    {
        missleEquipped = false;
        PhotonNetwork.Instantiate(misslePrefab, transform.position, Quaternion.identity);
    }
}
