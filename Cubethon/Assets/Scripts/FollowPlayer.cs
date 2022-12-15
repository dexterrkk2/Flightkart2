using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 offSet;
    public static FollowPlayer instance;
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    public void Update()
    {
        transform.position = Player.position + offSet;
    }
}
