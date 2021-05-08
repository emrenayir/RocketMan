using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBendAndRelease : MonoBehaviour
{
    private Animator stickAnimator;
    
    [SerializeField] private float screenWithEffectInAngles=120;
    [SerializeField] private float pixelDivider;
    [SerializeField] private float currentPixelPosHorizontal;
    [SerializeField] private float pixelStartPosHorizontal;
    [SerializeField] private bool startActive;
    // Start is called before the first frame update
    void Start()
    {
        pixelDivider = Screen.width / screenWithEffectInAngles;
        stickAnimator = this.gameObject.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !startActive)
        {
            stickAnimator.SetBool("Start",true);
            startActive = true;
            pixelStartPosHorizontal = Input.mousePosition.x;
                
            
        }
        else if (Input.GetMouseButton(0) && startActive)
        {
            currentPixelPosHorizontal = Input.mousePosition.x;
            float pixelOffset = (currentPixelPosHorizontal - pixelStartPosHorizontal)/1000;
            pixelOffset =  Mathf.Clamp(pixelOffset, -1f, 0f);
            Debug.Log(pixelOffset);
            stickAnimator.SetFloat("StickBender",-pixelOffset);
        }

        if (Input.GetMouseButtonUp(0))
        {
            stickAnimator.SetBool("Start",false);
            Destroy(this);
        }
    }
}
