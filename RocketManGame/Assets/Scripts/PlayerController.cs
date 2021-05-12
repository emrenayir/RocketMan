using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private StickBendAndRelease stickBendAndRelease;
    [SerializeField] private Rotator rotator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private slideRotator slideRotator1;
    [SerializeField] private GameObject playerAvatar;
    [SerializeField] private TrailRenderer leftWingTrail;
    [SerializeField] private TrailRenderer rightWingTrail;
    [SerializeField] private TrailRenderer playerTrail;
    [SerializeField] private GroundSpawner groundSpawner;
    [SerializeField] private GameObject failUi;
    [SerializeField] private float wingsCloseGravitySetter = -55f;


    private slideRotator slideRotator;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private GroundDetection groundDetection;

    private static readonly int Open = Animator.StringToHash("Open");

    void Start()
    {
        playerAnim = this.gameObject.GetComponent<Animator>();
        slideRotator = this.gameObject.GetComponent<slideRotator>();
        if (groundSpawner != null)
        {
            groundDetection = groundSpawner.groundTile.GetComponent<GroundDetection>();
        }
    }

    
    void Update()
    {
        if (!GameManager.Instance.isGameFinish)
        {
            if (stickBendAndRelease.flag)
            {
                SetPowerOfStartForce();
            }
            if (stickBendAndRelease.releasePlayer)
            {
                ReleasePlayerFromStick();
            }

            if (CameraManager.Instance.playerAirControl && Input.GetMouseButtonDown(0))
            {
                OpenWings();
            }

            if (!CameraManager.Instance.playerAirControl && Input.GetMouseButtonUp(0))
            {
                CloseWings();
            }

            if (playerMovement.onGround)
            {
                Fail();
            }

            if (this.transform.position.y < -150 )
            {
                Fail();
                GameManager.Instance.ReloadScene();
            }   
        }
    }

    private void SetPowerOfStartForce()
    {
        playerMovement.forwardJumpingPower *= stickBendAndRelease.startPower * 2 ;
        playerMovement.upJumpingPower *= stickBendAndRelease.startPower * 2 ;
        stickBendAndRelease.flag = false;
    }

    private void ReleasePlayerFromStick()
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

    private void CloseWings()
    {
        leftWingTrail.enabled = false;
        rightWingTrail.enabled = false;
        playerTrail.enabled = false;
        Vector3 temp = playerAvatar.transform.localRotation.eulerAngles;
        temp.y = 0f;
        playerAvatar.transform.localRotation = Quaternion.Euler(temp);
        playerMovement._gravitationalPower = wingsCloseGravitySetter;
        playerMovement.constantPower = 80;
        rotator.work = true;
        CameraManager.Instance.playerAirControl = true;
        playerAnim.SetBool(Open, false);
    }

    private void OpenWings()
    {
        playerMovement.forwardJumpingPower = 200f;
        leftWingTrail.enabled = true;
        rightWingTrail.enabled = true;
        playerTrail.enabled = true;
        rotator.work = false;
        rotator.flag = true;
        slideRotator.enabled = true;
        slideRotator1.enabled = true;
        playerAnim.SetBool(Open, true);
        CameraManager.Instance.playerAirControl = false;
        playerMovement.constantPower = 80;
        playerMovement._gravitationalPower = -10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CubePlatform"))
        {
            CubeJump();
        }
        if (other.gameObject.CompareTag("CylinderPlatform"))
        {
            CylinderJump();
        }
        if (other.gameObject.CompareTag("Tile"))
        {
            groundSpawner.groundTile = other.gameObject;
            groundDetection = groundSpawner.groundTile.GetComponent<GroundDetection>();
        }
        
        if (other.gameObject.CompareTag("Back"))
        {
            Debug.Log(groundDetection.transform.gameObject.name);
            groundDetection.lastDetectedWall = 0;
        }
        if (other.gameObject.CompareTag("Front"))
        {
            Debug.Log(groundDetection.transform.gameObject.name);
            groundDetection.lastDetectedWall = 1;
        }
        if (other.gameObject.CompareTag("Left"))
        {
            Debug.Log(groundDetection.transform.gameObject.name);
            groundDetection.lastDetectedWall = 2;
        }
        if (other.gameObject.CompareTag("Right"))
        {
            Debug.Log(groundDetection.transform.gameObject.name);
            groundDetection.lastDetectedWall = 3;
        }
    }

    private void Fail()
    {
        playerAnim.SetBool(Open, false);
        rotator.work = false;
        playerMovement.constantPower = 0f;
        failUi.SetActive(true);
        GameManager.Instance.isGameFinish = true;
        slideRotator.enabled = false;
        slideRotator1.enabled = false;
    }

    private void CylinderJump()
    {
        playerMovement.forwardJumpingPower = 200;
        playerMovement.upJumpingPower = 500;
        playerMovement.jumpingTime = 1f;
        playerMovement.Jump();
        Vector3 temp = playerAvatar.transform.localRotation.eulerAngles;
        temp.y = 0f;
        playerAvatar.transform.localRotation = Quaternion.Euler(temp);
        rotator.work = true;
        playerAnim.SetBool(Open, false);
    }

    private void CubeJump()
    {
        playerMovement.forwardJumpingPower = 200;
        playerMovement.upJumpingPower = 250;
        playerMovement.jumpingTime = 1f;
        playerMovement.Jump();
        Vector3 temp = playerAvatar.transform.localRotation.eulerAngles;
        temp.y = 0f;
        playerAvatar.transform.localRotation = Quaternion.Euler(temp);
        rotator.work = true;
        playerAnim.SetBool(Open, false);
    }
}
