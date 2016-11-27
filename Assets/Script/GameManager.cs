using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject wallPrefab;

    private int mapSize;
    private int[,] map;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Default Map
        LoadMap(new int[,] {
            { -1, -1, -1, -1, -1, -1 },
            { -1,  0,  0,  0,  0, -1 },
            { -1, -1, -1,  0, -1, -1 },
            { -1,  0,  0,  0, -1, -1 },
            { -1,  0, -1,  0,  0, -1 },
            { -1, -1, -1, -1, -1, -1 }
        });

        InstantiateTiles();
    }

    public void LoadMap(int[,] map)
    {
        this.map = (int[,])map.Clone();
        this.mapSize = map.GetLength(0);
    }

    public void InstantiateTiles()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                if (map[i, j] == -1)
                {
                    Instantiate(wallPrefab, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }
}
