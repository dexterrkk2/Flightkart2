using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CameraController : MonoBehaviourPun
{
    [Header("Look Sensitivity")]
    public float sensX;
    public float sensY;
    
    [Header("Clapming")]
    public float minY;
    public float maxY;

    [Header("Spectator")]
    public float spectatorMoveSpeed;

    private float rotx;
    private float roty;
    public static CameraController instance;

    private bool isSpectator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Awake()
    {
        instance = this;
    }

    private void LateUpdate()
    {
        rotx += Input.GetAxis("Mouse X") * sensX;
        roty += Input.GetAxis("Mouse Y") * sensY;

        roty = Mathf.Clamp(roty, minY, maxY);

        if (isSpectator)
        {
            transform.rotation = Quaternion.Euler(-roty, rotx, 0);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            float y = 0;

            if (Input.GetKey(KeyCode.E))
            {
                y = 1;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                y = -1;
            }
            Vector3 dir = transform.right * x + transform.up * y + transform.forward * z;
            transform.position += dir * spectatorMoveSpeed * Time.deltaTime;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(-roty, 0, 0);
            transform.parent.rotation = Quaternion.Euler(0, rotx, 0);
        }
    }
    public void SetAsSpectator()
    {
        isSpectator = true;
        transform.parent = null;
    }
}
