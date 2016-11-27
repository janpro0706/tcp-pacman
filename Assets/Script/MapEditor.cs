using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MapEditor : MonoBehaviour {
    public static MapEditor instance;

    public Canvas canvas;
    public Button buttonPrefab;

    public static int DEFAULT_MAP_SIZE = 25;

    private int mapSize;
    private int[,] map;
    private Button[,] buttons;
    private TileControl[,] tileControllers;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitDefaultMap();
        InstantiateTiles();
    }

    private void InitDefaultMap()
    {
        mapSize = DEFAULT_MAP_SIZE;
        map = new int[mapSize, mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                map[i, j] = (i == 0 || i == mapSize - 1 || j == 0 || j == mapSize - 1 ? 1 : 0);
            }
        }
    }

    public void InstantiateTiles()
    {
        buttons = new Button[mapSize, mapSize];
        tileControllers = new TileControl[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                buttons[i, j] = (Button)Instantiate(buttonPrefab, new Vector3(i * 20 + 10, j * 20, 0), Quaternion.identity);
                buttons[i, j].transform.SetParent(canvas.transform);
                tileControllers[i, j] = buttons[i, j].GetComponent<TileControl>();
                tileControllers[i, j].x = i;
                tileControllers[i, j].y = j;
            }
        }

        Invalidate();
    }

    public void UpdateStatus(int x, int y, int status)
    {
        // restricting tile status code could be placed here (ex: boundary tile can't be land state)
        map[x, y] = status;
        tileControllers[x, y].SetState((status + 1) % 2);
    }

    public void Invalidate()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                tileControllers[i, j].SetState(map[i, j]);
            }
        }
    }
}
