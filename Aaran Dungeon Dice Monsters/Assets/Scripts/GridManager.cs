using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int length, width;

    [SerializeField] private Tile tilePrefab;

    //[SerializeField] private Transform cam;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for(int z = 0; z < width; z++)
        {
            for (int x = 0; x < length; x++)
            {
                var spawnTile = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                spawnTile.name = $"Tile {x} {z}";
            }
        }

        //cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)length / 2 - 0.5f, 10);
    }
}

