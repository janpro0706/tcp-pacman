using UnityEngine;
using System.Collections;

public class Navigator : MonoBehaviour {


	void Awake()
    {
        int[,] map =
        {
            { 0, 10, 30, 15, 0, 0 },
            { 0, 0, 0, 0, 20, 0 },
            { 0, 0, 0, 0, 0, 5 },
            { 0, 0, 5, 0, 0, 20 },
            { 0, 0, 0, 0, 0, 20 },
            { 0, 0, 0, 0, 0, 20 }
        };

        ArrayList traverse = Dijkstra.dijkstra(map, 0);

        for (int i = 0; i < traverse.Count; i++)
        {
            Debug.Log(traverse[i]);
        }
    }
}
