using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private StickBendAndRelease stickBendAndRelease;
    [SerializeField] private float startSpeed;
    [SerializeField] private Rotator rotator;
    [SerializeField] private PlayerMovement playerMovement;
    
    private slideRotator slideRotator;
    [SerializeField] private slideRotator slideRotator1;
    
    
    private Rigidbody playerRb;
    private Animator playerAnim;
    private static readonly int Open = Animator.StringToHash("Open");

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = this.gameObject.GetComponent<Animator>();
        //playerRb = this.GetComponent<Rigidbody>();
        slideRotator = this.gameObject.GetComponent<slideRotator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stickBendAndRelease.releasePlayer)
        {    /*
            playerRb.useGravity = true;
            playerRb.AddForce(new Vector3(0f,0.3f,.5f) * startSpeed,ForceMode.Impulse);
            */
            gameObject.transform.parent = null;
            gameObject.transform.rotation = Quaternion.identity;
            CameraManager.Instance.changeCameraToSecondPosition = true;
            rotator.work = true;
           
            rotator.enabled = true;
            playerMovement.working = true;
            playerMovement.Jump();
            stickBendAndRelease.releasePlayer = false;
        }

        if (CameraManager.Instance.playerAirControl && Input.GetMouseButtonDown(0))
        {
            rotator.work = false;
            slideRotator.enabled = true;
            slideRotator1.enabled = true;
            playerAnim.SetBool(Open,true);
            CameraManager.Instance.playerAirControl = false;
        }
        
    }
}
