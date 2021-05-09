using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private StickBendAndRelease stickBendAndRelease;
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
            playerRb.AddForce((Vector3.forward + new Vector3(0f,0.7f,0f)) * 20f,ForceMode.Impulse);
            this.gameObject.transform.parent = null;
            stickBendAndRelease.releasePlayer = false;
        }
    }
}
