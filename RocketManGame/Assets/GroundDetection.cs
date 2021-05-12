using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField] public int lastDetectedWall;
    [SerializeField] private bool createWall;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject groundPrefab;
    
    private void OnTriggerExit(Collider other)
    {
        CreateGround(lastDetectedWall);
        Debug.Log("anan");
    }

    private void CreateGround(int lastdetected)
    {
       var obj = Instantiate(groundPrefab, spawnPoints[lastdetected].position, Quaternion.identity);
       obj.name = GroundSpawner.tileCounter.ToString();
       GroundSpawner.tileCounter += 1;
       GroundSpawner.tileUpdated = true;
    }
}
