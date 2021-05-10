using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private StickBendAndRelease stickBendAndRelease;
    [SerializeField] private float startSpeed;
    
    private Rigidbody playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stickBendAndRelease.releasePlayer)
        {
            playerRb.useGravity = true;
            playerRb.AddForce(new Vector3(0f,0.3f,.5f) * startSpeed,ForceMode.Impulse);
            this.gameObject.transform.parent = null;
            stickBendAndRelease.releasePlayer = false;
            CameraManager.Instance.changeCameraToSecondPosition = true;
        }
    }
}
