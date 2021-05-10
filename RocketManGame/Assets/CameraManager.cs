using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CameraManager>();
            }

            return _instance;
        }
        
    }

    [SerializeField] public PlayerMovement playerMovement;
    
    [SerializeField] private GameObject camPosition;
    [SerializeField] private GameObject player;

    public bool turnOffRb = false;
    public bool changeCameraToSecondPosition  = false;
    public bool playerAirControl  = false;

    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = this.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mainCam.transform.DOLookAt(player.transform.position, 
            0f);
        if (changeCameraToSecondPosition)
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        this.transform.parent = player.gameObject.transform;
        mainCam.transform.DOMove(camPosition.gameObject.transform.position, 1f).OnComplete((() =>
        {
            //
        }));
        changeCameraToSecondPosition = false;
        playerAirControl = true;
    }
    
}
