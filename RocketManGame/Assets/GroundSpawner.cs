using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public List<GameObject> platforms;
    
    private Vector3 nextSpawnPoint;
    void Start()
    {
        SpawnTile();
        SpawnTile();
        SpawnTile();
    }

    public void SpawnTile()
    {
        GameObject tile = Instantiate(groundTile, new Vector3(nextSpawnPoint.x,-925,nextSpawnPoint.z) , Quaternion.identity);
        nextSpawnPoint = tile.gameObject.transform.GetChild(1).position;
        for (int i = 0; i < 50; i++)
        {
            var kindOfPlatform = Random.Range(0, 2);
            Debug.Log(kindOfPlatform);
            var heightOfPlatform = Random.Range(200, 600);
            float transformX = Random.Range(-5000, 5000);
            float transformZ = Random.Range(-5000, 5000);
            var obj = Instantiate(platforms[kindOfPlatform], new Vector3(transformX, 0, transformZ),
                Quaternion.identity,tile.transform);
            obj.transform.localPosition = new Vector3(transformX, 0, transformZ);
            obj.transform.localScale = new Vector3(obj.transform.localScale.x,heightOfPlatform,obj.transform.localScale.z);
        }
        
    }
}
