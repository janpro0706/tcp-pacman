using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MapLoader {
    private static MapLoader instance;
    public static MapLoader Instance
    {
        private set
        {
            instance = value;
        }
        get
        {
            if (instance == null)
            {
                instance = new MapLoader();
            }
            return instance;
        }
    }

    private int mapSize;
    public int[,] map { get; private set; }
    public int[,] weightGraph { get; private set; }
    public Hashtable vertexes { get; private set; }
    
    public void LoadMap(int[,] map)
    {
        this.mapSize = map.GetLength(0);
        this.map = (int[,])map.Clone();

        ParseMapdata();

        // Debugging Code
        // Print Dijkstra Weight graph
        int i = 0;
        string report = "";
        foreach (int weight in weightGraph)
        {
            report += weight + " ";
            if ((++i % weightGraph.GetLength(0)) == 0) report += "\n";
        }
        Debug.Log(report);

        // Print Vertex coords Array
        report = "";
        foreach (Coord crd in vertexes.Keys)
        {
            report += crd.ToString() + " " + vertexes[crd] + "\n";
        }
        Debug.Log(report);

        // Print Dijkstra result from vertex 0
        report = "";
        ArrayList list = Dijkstra.dijkstra(weightGraph, 0);
        foreach (int ver in list)
        {
            report += ver + " ";
        }
        Debug.Log(report);
    }

    private void ParseMapdata()
    {
        weightGraph = new int[mapSize * mapSize, mapSize * mapSize];
        vertexes = new Hashtable();

        // parse coords 0,0 to 1,1
        int[,] dp = new int[mapSize + 1, mapSize + 1];
        for (int i = 0; i < mapSize + 1; i++)
        {
            for (int j = 0; j < mapSize + 1; j++)
            {
                if (i == 0 || j == 0) dp[i, j] = -1;
                else dp[i, j] = map[i - 1, j - 1];
            }
        }

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                // increase weight or add vertex when current point is land
                if (dp[i, j] == 0)
                {
                    // increase weight when current point is NOT VERTEX
                    if (!checkVertex(map, i - 1, j - 1))
                    {
                        if (dp[i - 1, j] != -1) dp[i, j] = dp[i - 1, j] + 1;
                        else if (dp[i, j - 1] != -1) dp[i, j] = dp[i, j - 1] + 1;
                    }
                    else  // add vertex and update weightGraph when it's VERTEX
                    {
                        int curVerIdx = vertexes.Count;

                        int x = i - 1, y = j - 1;
                        vertexes.Add(new Coord(x, y), vertexes.Count);
                        if (vertexes.Count > 1)
                        {
                            Coord leftVer = null, upperVer = null;
                            int leftWeight = dp[i - 1, j] + 1, upperWeight = dp[i, j - 1] + 1;

                            if (dp[i - 1, j] != -1) leftVer = new Coord(x - leftWeight, y);
                            if (dp[i, j - 1] != -1) upperVer = new Coord(x, y - upperWeight);

                            if (leftVer != null)
                            {
                                int verIdx = (int)vertexes[leftVer];

                                weightGraph[curVerIdx, verIdx] = leftWeight;
                                weightGraph[verIdx, curVerIdx] = leftWeight;
                            }
                            if (upperVer != null)
                            {
                                int verIdx = (int)vertexes[upperVer];

                                weightGraph[curVerIdx, verIdx] = upperWeight;
                                weightGraph[verIdx, curVerIdx] = upperWeight;
                            }
                        }
                    }
                }
            }
        }

        int vertexCount = vertexes.Count;
        int[,] temp = new int[vertexCount, vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            for (int j = 0; j < vertexCount; j++)
            {
                temp[i, j] = weightGraph[i, j];
            }
        }
        weightGraph = temp;
    }

    private bool checkVertex(int[,] map, int x, int y)
    {
        int roadType = checkRoadType(map, x, y);

        if (roadType != 3 && roadType != 4)
        {
            return true;
        }
        return false;
    }

    private int checkRoadType(int[,] map, int x, int y)
    {
        int wayCount = 0;
        wayCount += map[x - 1, y] == 0 ? 1 : 0;
        wayCount += map[x + 1, y] == 0 ? 1 : 0;
        wayCount += map[x, y - 1] == 0 ? 1 : 0;
        wayCount += map[x, y + 1] == 0 ? 1 : 0;

        if (wayCount == 4) return 0;
        else if (wayCount == 3) return 1;
        else if (wayCount == 1) return 2;
        else if (wayCount == 0) return 3;
        else if ((map[x - 1, y] + map[x + 1, y]) % 2 == 0) return 4; // 2 ways and straight
        else return 5;                                               // 2 ways and curved
    }
}

class Coord
{
    public int x, y;

    public Coord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        Coord dest = (Coord)obj;

        if (x == dest.x && y == dest.y)
        {
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return this.x * 1000 + this.y;
    }

    public override string ToString()
    {
        return "(" + x + " " + y + ")";
    }
};