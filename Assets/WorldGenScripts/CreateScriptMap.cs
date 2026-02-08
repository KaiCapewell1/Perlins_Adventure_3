using System.Collections.Generic;
using UnityEngine;

public class CreateScriptMap : MonoBehaviour
{
    public int MapWidth;
    public int MapHeight;

    public static HashSet<Vector2Int> LitteManWalk(Vector2Int startpos, int walkLen)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startpos);
        var prepos = startpos;

        for (int i = 0; i < walkLen; i++) 
        {
            var newpos = prepos + Direction2D.GetRandomDirection();
            path.Add(newpos);
            prepos = newpos;
        }
        return path;

    }
}

public static class Direction2D
{
    public static List<Vector2Int> DirectionList= new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0) //LEFT
    };

    public static Vector2Int GetRandomDirection()
    {
        return DirectionList[Random.Range(0, DirectionList.Count)];
    }
}
