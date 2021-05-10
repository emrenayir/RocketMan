using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool work;

    private bool flag = true;
    // Update is called once per frame
    void Update()
    {
        if (work)
        {
          this.transform.Rotate(new Vector3(10,0,0));
        }
        else if (flag)
        {
            
                Vector3 temp = transform.rotation.eulerAngles;
                temp.x = 100.0f;
                transform.rotation = Quaternion.Euler(temp);
                flag = false;
        }
        
    }
}
