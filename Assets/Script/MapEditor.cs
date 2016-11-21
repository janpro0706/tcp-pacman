using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MapEditor : MonoBehaviour {
    public static MapEditor instance;

    public Canvas canvas;
    public Button buttonPrefab;

    public static int MAP_SIZE = 25;

    private int[,] map;
    private Button[,] buttons;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        canvas = FindObjectOfType<Canvas>();
        Debug.Log(canvas);

        InitMap();
    }

    public void InitMap()
    {
        map = new int[MAP_SIZE, MAP_SIZE];
        buttons = new Button[MAP_SIZE, MAP_SIZE];

        for (int i = 0; i < MAP_SIZE; i++)
        {
            for (int j = 0; j < MAP_SIZE; j++)
            {
                map[i, j] = 0;
                buttons[i, j] = (Button)Instantiate(buttonPrefab, new Vector3(i * 20 + 10, j * 20, 0), Quaternion.identity);
                buttons[i, j].transform.SetParent(canvas.transform);

                if (i == 0 || i == MAP_SIZE - 1 || j == 0 || j == MAP_SIZE - 1)
                {
                    buttons[i, j].GetComponent<TileControl>().Toggle();
                }
            }
        }
    }

    public void Invalidate()
    {
        
    }
}
