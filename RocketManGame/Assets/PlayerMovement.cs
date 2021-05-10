using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 velocity;
  
    public bool jumpTest;
 
    [SerializeField] public bool working;
    public CharacterController characterController;
    public bool jumpingNow;

    public bool canJump = true;
    public bool halfJump = false;
    public bool continuousJumps;
    public bool moveDuringJumps;
    public float waitBetweenJumps=2f;
    public bool groundSensitiveJump= true;   
    public bool timeSensitiveJump;
    public float jumpingTime=3;
    public bool _groundSensitiveJump;   
    public bool _timeSensitiveJump;
    public bool jumpDirectionAdditive;
    public Vector3 specialJumpDirection;
    private Vector3 _specialJumpDirection;
    public Vector3 specialJumpGravityAxis;
    private Vector3 _specialJumpGravity;
    public float forwardJumpingPower=10;  
    public float _forwardJumpingPower;  
    public float upJumpingPower = 10;
    public float _upJumpingPower;
    
    
 
    
    public bool useGravity;
    public bool localGravity  ;
    public Vector3 gravityDirection = new Vector3(0,-1,0);
    public float gravitationalPower=-9.81f;
    public float _gravitationalPower ;
    public bool useIncreasingGravity;
    public float gravityBuildUPPerSecond=1;
    public List <Transform > surfaceCheckPositions;
    public float defaultSurfaceCheckDistance=0.4f;
    public List <float >  surfaceDetectionDistances ;
    public LayerMask surfaceMask;
    public LayerMask groundMask;
    public float maxExtraGravity= 25f;
    public float maxGravity ;
    
    
    
    public float constantPower;
    
    public Vector3 constantMovingDirection=new Vector3();
    public Vector3 constantMovingForce;
    public bool useLocalDirection;

    public Vector3 gravitationalForce ;
    public bool onGround; 
    public bool onSurface; 
    
    //------------------------------------------------------
    
    
   
    private float counterJumpFactor =0;
     
    
    private void Start()
    {

        Adjustments();
    }

    private void Adjustments()
    {
        if (characterController==null)
        {
            characterController = gameObject.GetComponent<CharacterController>();
        }

        if (surfaceCheckPositions.Count==0)
        {
            useIncreasingGravity = false;
        }
        else
        {
            foreach (var each in surfaceCheckPositions)
            {
                float checkDistance = defaultSurfaceCheckDistance;
                Renderer checkRenderer = each.GetComponent<Renderer>();
                if (checkRenderer!=null)
                {
                    checkDistance = each.localScale.y / 2;
                }
                surfaceDetectionDistances.Add(checkDistance);
            }
            
        }

        _timeSensitiveJump = timeSensitiveJump;
        _groundSensitiveJump = groundSensitiveJump;
        ResolveContrast();
        getJumpType();

        gravityBuildUPPerSecond = Math.Abs(gravityBuildUPPerSecond);
        maxExtraGravity=  Math.Abs(maxExtraGravity);
        maxGravity=gravitationalPower > 0 ? gravitationalPower+maxExtraGravity : gravitationalPower-maxExtraGravity;
        gravityBuildUPPerSecond = gravitationalPower > 0 ? gravityBuildUPPerSecond : -gravityBuildUPPerSecond;
        _gravitationalPower = gravitationalPower;

    }

    private void getJumpType()
    {
        _groundSensitiveJump = groundSensitiveJump;
        _timeSensitiveJump = timeSensitiveJump;
    }
    
    private bool checkLayerMaskCollision(Transform position,LayerMask layerMask,float distance)
    { 
        return Physics.CheckSphere(position.position, distance, layerMask);
 
    }
    private void ResolveContrast()
    {
        if (_groundSensitiveJump!=groundSensitiveJump)
        {
            if (groundSensitiveJump)
            {
                timeSensitiveJump = false;
            }
             
        }
        if (_timeSensitiveJump!=timeSensitiveJump)
        {
            if (timeSensitiveJump)
            {
                groundSensitiveJump = false;
                 
            }
        }

        


    }
    private void CheckSurfaceCondition()
    {
        bool _onGround = false;
        bool _onSurface = false;
        for (var index = 0; index < surfaceCheckPositions.Count; index++)
        {
            var each = surfaceCheckPositions[index];
            
            if ( checkLayerMaskCollision(each, groundMask,surfaceDetectionDistances[index]))
            {
                _onGround = true;
            }
            if ( checkLayerMaskCollision(each, surfaceMask,surfaceDetectionDistances[index]))
            {
                _onSurface = true;
            }

            if (_onGround&&_onSurface)
            {
                break;
            }
        }

        onGround = _onGround;
        onSurface = _onSurface;
    }

    private Vector3 CalculateGravity()
    {
        if (useGravity)
        {
            
            if (onGround )
            {
                _gravitationalPower = gravitationalPower;
            
            }
            else
            {
                if (useIncreasingGravity)
                {
                    _gravitationalPower += gravityBuildUPPerSecond * Time.deltaTime;

                    if (Math.Abs(_gravitationalPower)>Math.Abs(maxGravity))
                    {
                        _gravitationalPower = maxGravity;
                    }
                }
            }




            if (localGravity)
            {
                return getLocalDirection(gravityDirection, gameObject.transform, Math.Abs(_gravitationalPower));
            }
            else
            {
                return calculateVelocity(gravityDirection, Math.Abs(_gravitationalPower));
            }
        }

        return new Vector3(0,0,0);
    }

    public void Jump()
    {
        if (canJump)
        {
            ResolveContrast();
            getJumpType();
            JumpWithTime();
        }
    }

    private void JumpWithTime()
    {
        _forwardJumpingPower = forwardJumpingPower;
        _upJumpingPower = upJumpingPower;
        jumpingNow = true;
        float jumpingForwardDuration =  halfJump ? jumpingTime : jumpingTime-0.2f;
        bool flag1 = true;
        float endForwardValue = flag1 ? 50 : 0;
      
        DOTween.To(() => _forwardJumpingPower, x => _forwardJumpingPower = x, endForwardValue, jumpingForwardDuration).OnComplete(() =>
        { 
               
                

        });
        float gravityEndValue = halfJump ? 0 : -upJumpingPower;
        DOTween.To(() => _upJumpingPower, x => _upJumpingPower = x, gravityEndValue, jumpingTime).OnComplete(() =>
        {
            _upJumpingPower = 0;
                 
            if (continuousJumps)
            {
                Invoke(nameof(Jump),waitBetweenJumps);
                if (moveDuringJumps)
                {
                    jumpingNow = false;
                }
            }
            else
            {
                jumpingNow = false;
            }

            SetConstant(100f);
        });


    }

    private void SetConstant(float newConstantPower)
    {
        constantPower = newConstantPower;
        useGravity = true;
    }

    public void JumpWithTime(float time)// ADD DOTWEEN 
    {
        if (canJump&&!jumpingNow)
        {
            jumpingTime = time;
            timeSensitiveJump = true;
            Jump();
        }
    }

  private Vector3 jumpingProperties()
  {
      _specialJumpDirection  =useLocalDirection ? getLocalDirection(specialJumpDirection,gameObject.transform,_forwardJumpingPower) : calculateVelocity (specialJumpDirection,_forwardJumpingPower) ;
      _specialJumpGravity  =useLocalDirection ? getLocalDirection(specialJumpGravityAxis,gameObject.transform,_upJumpingPower) : calculateVelocity (specialJumpGravityAxis,_upJumpingPower) ;

      return combineVelocities(_specialJumpDirection, _specialJumpGravity);
      
        
  }
  private Vector3 combineVelocities(Vector3 first ,Vector3 second)
  {
      return first+ second  ;
  }

  private void checkLanding()
  {

      if (onGround&&_groundSensitiveJump&&jumpingNow)
      {
          
      }
  }
  private Vector3 calculateVelocity(Vector3 direction,float power)
  {
      return direction * power;
  }

  private Vector3 getLocalDirection(Vector3 planedDirection, Transform subject,float speed = 1)
  {
      
      Vector3 localYDirection = subject.TransformDirection(Vector3.up)*planedDirection.y;
      Vector3 localXDirection = subject.TransformDirection(Vector3.right)*planedDirection.x;
      Vector3 localZDirection = subject.TransformDirection(Vector3.forward)*planedDirection.z;

      return  speed * (localYDirection + localXDirection + localZDirection);
  }
    void FixedUpdate()
    {

        if (working)
        {
            if (jumpTest&&!jumpingNow)
            {
                jumpTest = false;
                Jump();
            }
            ResolveContrast();
            CheckSurfaceCondition();

            checkLanding();
            
            constantMovingForce  =useLocalDirection ? getLocalDirection(constantMovingDirection,gameObject.transform,constantPower) : calculateVelocity (constantMovingDirection,constantPower) ;
            
            gravitationalForce = CalculateGravity();

            if (jumpingNow)
            {
                velocity = jumpingProperties();
            }
            else
            {
                velocity = combineVelocities(constantMovingForce ,gravitationalForce);
            }
            
            move(velocity);

        }
        
    }

    private void move(Vector3 theVelocity)
    {
        
        characterController.Move(theVelocity * Time.deltaTime) ;

    }
    
    private void rotate(GameObject subject,bool turnX,bool turnY,bool turnZ,Vector3 angles,float xSpeed,float ySpeed, float zSpeed)
    {


        Quaternion currentRotation = subject.transform.rotation; 
        
        if (turnX)
            subject.transform.rotation = Quaternion.RotateTowards(subject.transform.rotation, Quaternion.Euler(angles.x, currentRotation.y, currentRotation.z), xSpeed * Time.deltaTime);
        
        if (turnY)
            subject.transform.rotation = Quaternion.RotateTowards(subject.transform.rotation, Quaternion.Euler(currentRotation.x, angles.y, currentRotation.z), ySpeed * Time.deltaTime);
        
        if (turnZ)
            subject.transform.rotation = Quaternion.RotateTowards(subject.transform.rotation, Quaternion.Euler(currentRotation.x, currentRotation.y, angles.z), zSpeed * Time.deltaTime);

        if (Math.Abs(currentRotation.x - angles.x) < 0.01f)
            turnX = false;
        if (Math.Abs(currentRotation.y - angles.y) < 0.01f)
            turnY = false;
        if (Math.Abs(currentRotation.z - angles.z) < 0.01f)
            turnZ = false;


    }
}
