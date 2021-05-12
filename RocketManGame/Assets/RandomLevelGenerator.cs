using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGenerator : MonoBehaviour
{
    public List<GameObject> platforms;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            var kindOfPlatform = Random.Range(0, 2);
            Debug.Log(kindOfPlatform);
            var heightOfPlatform = Random.Range(200, 600);
            float transformX = Random.Range(-5000, 5000);
            float transformZ = Random.Range(-5000, 5000);
            var obj = Instantiate(platforms[kindOfPlatform], new Vector3(transformX, 0, transformZ),
                Quaternion.identity,this.transform);
            obj.transform.localPosition = new Vector3(transformX, 0, transformZ);
            obj.transform.localScale = new Vector3(obj.transform.localScale.x,heightOfPlatform,obj.transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
