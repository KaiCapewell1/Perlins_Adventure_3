using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class DungeonGen : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startpos = Vector2Int.zero;
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLen = 10;
    [SerializeField]
    public bool starRandomIteration = true;
    [SerializeField]
    private TilemapVisual TilemapVisual;

    public void RunGenerator()
    {
        HashSet<Vector2Int> floorpos = RunRandomWalk();
        TilemapVisual.Clear();
        TilemapVisual.PaintFloorTiles(floorpos);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentpos = startpos;
        HashSet<Vector2Int> floorpos = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations; i++)
        {
            var path = CreateScriptMap.LitteManWalk(currentpos, walkLen);
            floorpos.UnionWith(path);
            if (starRandomIteration)
                currentpos = floorpos.ElementAt(Random.Range(0, floorpos.Count));
        }

        return floorpos;
    }
}
