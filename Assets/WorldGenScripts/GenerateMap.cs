using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public int mapwidth;
    public int mapheight;
    public generateRooms genrooms;

    public Vector2Int randomStartRoom;
    public Vector2Int randomEndRoom;
    public GameObject TEMP_mapWall;
    public void Start()
    {
        GenerateMapSize();
        DrawBackgroundGrid();
    }
    public void GenerateMapSize()
    {
        int buffer = 7;

        randomStartRoom = new Vector2Int(Random.Range(2, mapwidth - buffer), Random.Range(2, mapheight - buffer));
        genrooms.GenStartEndRoom(randomStartRoom);

        randomEndRoom = new Vector2Int(Random.Range(2, mapwidth - 2), Random.Range(2, mapheight - 2));
        genrooms.GenStartEndRoom(randomEndRoom);
    }

    public void DrawBackgroundGrid()
    {
        for (int x = 0; x < mapwidth; x++)
        {
            for (int y = 0; y < mapheight; y++)
            {
               
                bool isEdge = (x == 0 || x == mapwidth - 1 || y == 0 || y == mapheight - 1);

                if (isEdge)
                {
                    GameObject tile = Instantiate(TEMP_mapWall, new Vector3(x, y, 0), Quaternion.identity);
                    tile.transform.parent = this.transform;
                }
            }
        }
    }
}