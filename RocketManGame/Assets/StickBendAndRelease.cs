using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBendAndRelease : MonoBehaviour
{
    private Animator stickAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        stickAnimator = this.gameObject.GetComponent<Animator>();
        stickAnimator.SetFloat("StickBender",0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            stickAnimator.SetFloat("StickBender",1f);
        }
    }
}
