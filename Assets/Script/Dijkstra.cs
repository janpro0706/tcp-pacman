using UnityEngine;
using System.Collections;

public class Dijkstra {
    public static ArrayList dijkstra(int[,] map, int startVer)
    {
        ArrayList weight = new ArrayList();
        ArrayList notTraversed = new ArrayList();
        ArrayList traverse = new ArrayList();

        int vertexNum = (int)System.Math.Sqrt(map.Length);

        for (int i = 0; i < vertexNum; i++)
        {
            notTraversed.Add(i);
            if (i == startVer)
            {
                weight.Add(0);
            } else
            {
                weight.Add(int.MaxValue);
            }
        }

        //Debug.Log("print array");
        //PrintArray(weight);
        //PrintArray(notTraversed);
        //PrintArray(traverse);
        //Debug.Log("-------------");

        while (notTraversed.Count > 0)
        {
            int minVert = getMinVert(weight, notTraversed);
            //Debug.Log("minVert: " + minVert);

            notTraversed.RemoveAt(getVertIdx(notTraversed, minVert));
            traverse.Add(minVert);

            for (int i = 0; i < vertexNum; i++)
            {
                if (map[minVert, i] <= 0) continue;

                int newWeight = map[minVert, i] + (int)weight[minVert];

                if (newWeight < (int)weight[i])
                {
                    weight[i] = newWeight;
                }
            }

            //Debug.Log("print array");
            //PrintArray(weight);
            //PrintArray(notTraversed);
            //PrintArray(traverse);
            //Debug.Log("-------------");
        }

        return traverse;
    }

    private static int getVertIdx(ArrayList notTraversed, int vertex)
    {
        int vertIdx = -1;

        for (int i = 0; i < notTraversed.Count; i++)
        {
            if ((int)notTraversed[i] == vertex)
            {
                vertIdx = i;
                break;
            }
        }

        return vertIdx;
    }

    private static int getMinVert(ArrayList weight, ArrayList notTraversed)
    {
        int minVert = (int)notTraversed[0];
        for (int i = 0; i < notTraversed.Count; i++)
        {
            int vert = (int)notTraversed[i];
            if ((int)weight[vert] < (int)weight[minVert])
            {
                minVert = vert;
            }
        }

        return minVert;
    }

    private static void PrintArray(ArrayList list)
    {
        string log = "";
        for (int i = 0; i < list.Count; i++)
        {
            log += list[i] + " ";
        }
        Debug.Log(log);
    }
}
