using Unity.VisualScripting;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public int mapwidth;
    public int mapheight;
    public generateRooms genrooms;

    public Vector2Int randomStartRoom;
    public Vector2Int randomEndRoom;
    public GameObject TEMP_mapWall;


    [Range(0, 100)]
    public float densityPercent;

    bool[,] occupied;
    





    public void Start()
    {
        occupied = new bool[mapwidth, mapheight];

        GenerateMapSize();
        DrawBackgroundGrid();
    }
    public void GenerateMapSize()
    {
        genrooms.Room_centers.Clear();
        float density = densityPercent / 100f;
        int targetCoverage = Mathf.RoundToInt((mapwidth * mapheight) * density);


        int CurrentroomCount = 0;

        while (CurrentroomCount < targetCoverage)
        {
            int width = Random.Range(10, 40);
            int height = Random.Range(10, 40);

            Vector2Int pos = new Vector2Int(
                Random.Range(10, mapwidth - 10),
                Random.Range(10, mapheight - 10)
            );

            if (!CanplaceRoom(pos, width, height, 4))
                continue;

            int roomArea = genrooms.GenerateRoom(pos, width, height, occupied);
            CurrentroomCount += roomArea;
        }

        int randomRoom = Random.Range(0, genrooms.Room_centers.Count);

        Vector2Int startroom = genrooms.Room_centers[randomRoom];

        randomRoom = Random.Range(0, genrooms.Room_centers.Count);

        Vector2Int EndRoom = genrooms.Room_centers[randomRoom];

        genrooms.PlaceDoors(startroom, EndRoom);

    }

    public void DrawBackgroundGrid()
    {
        for (int x = 0; x < mapwidth; x++) {
            for (int y = 0; y < mapheight; y++){
                bool isEdge = (x == 0 || x == mapwidth - 1 || y == 0 || y == mapheight - 1);

                if (isEdge)
                { GameObject tile = Instantiate(TEMP_mapWall, new Vector3(x, y, 0), Quaternion.identity);
                    tile.transform.parent = this.transform;}
            }
        }
    }

    public bool CanplaceRoom(Vector2Int center,int width, int height, int padding)
    {
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        for (int x = -halfWidth - padding; x <= halfWidth + padding; x++)
        {
            for (int y = -halfHeight - padding; y <= halfHeight + padding; y++)
            {
                int checkX = center.x + x;
                int checkY = center.y + y;

                if (checkX < 0 || checkX >= mapwidth || checkY < 0 || checkY >= mapheight)
                    return false;


                if (occupied[checkX, checkY])
                    return false;
            }
        }

        return true;
    }
}