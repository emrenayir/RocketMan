using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private StickBendAndRelease stickBendAndRelease;
    [SerializeField] private Rotator rotator;
    [SerializeField] private PlayerMovement playerMovement;
    
    private slideRotator slideRotator;
    [SerializeField] private slideRotator slideRotator1;
    [SerializeField] private GameObject playerAvatar;
    
    
    private Rigidbody playerRb;
    private Animator playerAnim;
    private static readonly int Open = Animator.StringToHash("Open");

    [SerializeField] private TrailRenderer leftWingTrail;
    [SerializeField] private TrailRenderer rightWingTrail;
    [SerializeField] private TrailRenderer playerTrail;
    
    
    void Start()
    {
        playerAnim = this.gameObject.GetComponent<Animator>();
        slideRotator = this.gameObject.GetComponent<slideRotator>();
    }

    
    void Update()
    {
        if (stickBendAndRelease.releasePlayer)
        {
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
            leftWingTrail.enabled = true;
            rightWingTrail.enabled = true;
            playerTrail.enabled = true; 
            rotator.work = false;
            rotator.flag = true;
            slideRotator.enabled = true;
            slideRotator1.enabled = true;
            playerAnim.SetBool(Open,true);
            CameraManager.Instance.playerAirControl = false;
            playerMovement.constantPower = 80;
            playerMovement._gravitationalPower = -10;
        }

        if (!CameraManager.Instance.playerAirControl && Input.GetMouseButtonUp(0))
        {
            leftWingTrail.enabled = false;
            rightWingTrail.enabled = false;
            playerTrail.enabled = false; 
            Vector3 temp = playerAvatar.transform.localRotation.eulerAngles;
            temp.y = 0f;
            playerAvatar.transform.localRotation = Quaternion.Euler(temp);
            playerMovement._gravitationalPower = -55;
            playerMovement.constantPower = 80;
            rotator.work = true;
            CameraManager.Instance.playerAirControl = true;
            playerAnim.SetBool(Open,false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CubePlatform"))
        {
            Debug.Log("CubePlatform");
            playerMovement.forwardJumpingPower = 200;
            playerMovement.upJumpingPower = 1000;
            playerMovement.jumpingTime = .3f;
            playerMovement.Jump();
            Vector3 temp = playerAvatar.transform.localRotation.eulerAngles;
            temp.y = 0f;
            playerAvatar.transform.localRotation = Quaternion.Euler(temp);
            rotator.work = true;
            playerAnim.SetBool(Open,false);
        }
        if (other.gameObject.CompareTag("CylinderPlatform"))
        {
            Debug.Log("CylinderPlatform");
            playerMovement.forwardJumpingPower = 200;
            playerMovement.upJumpingPower = 1000;
            playerMovement.jumpingTime = .6f;
            playerMovement.Jump();
            Vector3 temp = playerAvatar.transform.localRotation.eulerAngles;
            temp.y = 0f;
            playerAvatar.transform.localRotation = Quaternion.Euler(temp);
            rotator.work = true;
            playerAnim.SetBool(Open,false);
        }
    }
}
