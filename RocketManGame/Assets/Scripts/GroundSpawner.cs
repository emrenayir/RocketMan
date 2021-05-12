using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public static int tileCounter;
    public static bool tileUpdated  = false;
    [SerializeField] private List<GameObject> tiles;
     

    private void Start()
    {
        tiles = new List<GameObject>();
    }

    private void Update()
    {
        if (tileUpdated)
        {
            tiles.Add(groundTile.transform.parent.gameObject);
            tileUpdated = false;
        }

        while (tileCounter > 2)
        {
            tiles.RemoveAt(0);
            Destroy(tiles[0].gameObject);
            tileCounter = tiles.Count;
        }
    }
}
