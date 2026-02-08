using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisual : MonoBehaviour
{
    [SerializeField]
    private Tilemap floortilemap;
    [SerializeField]
    private TileBase floorTile;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorpos)
    {
        PaintTiles(floorpos, floortilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var pos in positions) 
        {
            PaintSingleTile(tilemap, tile, pos);
        }

    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int pos)
    {
        var tilePostion = tilemap.WorldToCell((Vector3Int)pos);
        tilemap.SetTile(tilePostion, tile);
    }

    public void Clear()
    {
        floortilemap.ClearAllTiles();
    }
}
