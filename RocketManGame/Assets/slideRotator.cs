using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideRotator : MonoBehaviour
{
  //  public float test;
  //  public bool testBool;
   // public Transform parentRotator;
    public bool allowWastedTouch;
    public bool useLimit;
    public bool reverseInput;
    [SerializeField] private bool inputHorizontalElseVertical=true;
    
    [SerializeField] private char rotationLocalAxis = 'y';
    
    [SerializeField] private float rotationSpeed = 20;
   // [SerializeField] private float tiltAngle=60f;
    [SerializeField] private Vector3  startRotation  ;

    [SerializeField] private bool startActive;
    [SerializeField] private float pixelStartPosHorizontal;
    [SerializeField] private float currentPixelPosHorizontal;
    [SerializeField] private float screenWithEffectInAngles=120;
    [SerializeField] private float pixelDivider;

    [SerializeField] private float offsetRotation;
    [SerializeField] private float minRotationAngleLimit=0;
    // threshold can be added to make min angle check only for first reach
    [SerializeField] private float maxRotationAngleLimit=35;
    [SerializeField] private float initialBaseRotation ;
    [SerializeField] private float rotationMax ;
    [SerializeField] private float rotationMin ;


   // [SerializeField] private Quaternion target;
    
    
    
    void Start()
    {
        pixelDivider = Screen.width / screenWithEffectInAngles;
        
        
        //-// Vector3 currentRotation= transform.rotation.eulerAngles;
        Vector3 currentRotation= transform.localRotation.eulerAngles;
        
        Quaternion initialRotation=Quaternion.Euler(currentRotation.x , currentRotation.y, currentRotation.z) ;
        if (rotationLocalAxis=='x'||rotationLocalAxis=='X')
        {
            initialBaseRotation = initialRotation.x;
        }
        else if (rotationLocalAxis=='z'||rotationLocalAxis=='Z')
        {
            initialBaseRotation = initialRotation.z;
        }
        else// (rotationLocalAxis=='y'||rotationLocalAxis=='Y')
        {
            initialBaseRotation = initialRotation.y;
        }

        rotationMax = initialBaseRotation + maxRotationAngleLimit/90;
        rotationMin = initialBaseRotation - maxRotationAngleLimit/90;
        
    }

    // Update is called once per frame
    void Update()
    {
        int reverser = reverseInput ? -1 : 1;
        
         if (Input.GetMouseButton(0) && !startActive)
        {
            //-// startRotation = transform.rotation.eulerAngles;
            startRotation = transform.localRotation.eulerAngles;
            
            
            startActive = true;
            
            if (inputHorizontalElseVertical)
            {
                pixelStartPosHorizontal = Input.mousePosition.x;
            }
            else
            {
                pixelStartPosHorizontal = Input.mousePosition.y;
            }
            
            
        }else if (Input.GetMouseButton(0) && startActive)
        {
           // startRotation = transform.rotation.eulerAngles;
            if (inputHorizontalElseVertical)
            {
                currentPixelPosHorizontal = Input.mousePosition.x;
            }
            else
            {
                currentPixelPosHorizontal = Input.mousePosition.y;
            }
            
            
            float pixelOffset = currentPixelPosHorizontal - pixelStartPosHorizontal;
            offsetRotation = pixelOffset / pixelDivider;
            
            if (Math.Abs(offsetRotation)<minRotationAngleLimit)
            {
                return;
            }
             
             
           //-// Vector3 currentRotation= transform.rotation.eulerAngles;
            Vector3 currentRotation= transform.localRotation.eulerAngles;
            
            
            Quaternion targetRotation ;
            float singularTargetRotation;
            offsetRotation *= reverser;

            Vector3 _startRotation = startRotation; //+  parentRotator.rotation.eulerAngles;
            
            if (rotationLocalAxis=='x'||rotationLocalAxis=='X')
            {
                targetRotation = Quaternion.Euler(_startRotation.x+offsetRotation, _startRotation.y, _startRotation.z);
                singularTargetRotation = targetRotation.x;
            }
            else if (rotationLocalAxis=='z'||rotationLocalAxis=='Z')
            {
                targetRotation = Quaternion.Euler(_startRotation.x, _startRotation.y , _startRotation.z+offsetRotation);
                singularTargetRotation = targetRotation.z;
            }
            else// (rotationLocalAxis=='y'||rotationLocalAxis=='Y')
            {
                targetRotation = Quaternion.Euler(_startRotation.x, _startRotation.y+offsetRotation, _startRotation.z);
                singularTargetRotation = targetRotation.y;
            }

           // test=singularTargetRotation;
            // target = targetRotation;
            if (useLimit&&(singularTargetRotation>rotationMax||singularTargetRotation<rotationMin))
            {
               // testBool = true;
                if (!allowWastedTouch)
                {
                  //-//  startRotation = transform.rotation.eulerAngles; 
                  startRotation = transform.localRotation.eulerAngles;
                    
                    if (inputHorizontalElseVertical)
                    {
                        pixelStartPosHorizontal = Input.mousePosition.x;
                    }
                    else
                    {
                        pixelStartPosHorizontal = Input.mousePosition.y;
                    }
                }
                return;
            }
           // testBool = false;
           
           
         //-//   transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,  Time.deltaTime * rotationSpeed); 
           
           transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation,  Time.deltaTime * rotationSpeed);
                
         /*   if (Math.Abs(offsetRotation)>1) // constant fix local rotation adjustment  would be better
            {
                if (inputHorizontalElseVertical)
                {
                    pixelStartPosHorizontal = Input.mousePosition.x;
                }
                else
                {
                    pixelStartPosHorizontal = Input.mousePosition.y;
                }
            }*/
              
        }
        else if(startActive == true)
        {
            startActive = false;
        }



         
    }
}
