using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Singleton
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
    
    #endregion
    
    [SerializeField] private GameObject player;
    
    public bool changeCameraToSecondPosition  = false;
    public bool playerAirControl  = false;

    private Camera mainCam;
    
    void Start()
    {
        mainCam = this.gameObject.GetComponent<Camera>();
    }
    void Update()
    {
        
        mainCam.transform.LookAt(player.transform.position);
        if (changeCameraToSecondPosition)
        {
            MoveCamera();
        }
    }
    
    private void MoveCamera()
    {
        this.transform.parent = player.gameObject.transform;
        mainCam.transform.DOLocalMove(new Vector3(0f,25f,-30),  1.5f);
        changeCameraToSecondPosition = false;
        playerAirControl = true;
    }
    
}
