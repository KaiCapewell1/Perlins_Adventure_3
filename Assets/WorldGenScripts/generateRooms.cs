using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class generateRooms : MonoBehaviour
{
    public Tilemap tilemapFloor;
    public Tilemap tilemapWall;

    public Tile LeftWall;
    public Tile RightWall;
    public Tile TopWall;
    public Tile BottomWall;

    public Tile Floor;

    public GameObject Player;

    public GameObject EndDoorObj;
    public GameObject StartDoorObj;


    public List<Vector2Int> Room_centers = new List<Vector2Int>();
    public int GenerateRoom(Vector2Int center, int roomWidth, int roomHeight, bool[,] occupied)
    {

        int tilePlaced = 0;

        int HalfW = roomWidth / 2;
        int HalfH = roomHeight / 2;


        for (int x = -HalfW; x <= HalfW; x++)
        {
            for (int y = -HalfH; y <= HalfH; y++)
            {

                int WorldX = center.x + x;
                int WorldY = center.y + y;

                if (WorldX < 0 || WorldX >= occupied.GetLength(0) ||
                   WorldY < 0 || WorldY >= occupied.GetLength(1))
                    continue;



                Vector3Int pos = new Vector3Int(WorldX, WorldY, 0);


                bool isWall =
                (x == -HalfW ||
                 x == HalfW ||
                 y == -HalfH ||
                 y == HalfH);

                if (isWall)
                { tilemapWall.SetTile(pos, BottomWall); }

                else
                { tilemapWall.SetTile(pos, null); tilemapFloor.SetTile(pos, Floor); }

                occupied[WorldX, WorldY] = true;

                tilePlaced++;


            }
        }

        Room_centers.Add(center);

        return tilePlaced;
    }

    public void PlaceDoors(Vector2Int StartRoomCenter, Vector2Int EndRoomsCenter)
    {

        // --- Start Door ---
        Vector3Int startPos = FindWallSpot(StartRoomCenter);
        tilemapWall.SetTile(startPos, null);
        tilemapFloor.SetTile(startPos, Floor);
        Vector3 startCenteredPos = new Vector3(startPos.x + 0.5f, startPos.y + 1.5f, 0);
        GameObject StartDoor = Instantiate(StartDoorObj, startCenteredPos, Quaternion.identity);
        StartDoor.GetComponent<SpriteRenderer>().sortingOrder = 100;

        // --- Spawn Player ---
        Vector3 playerPosition = new Vector3(startCenteredPos.x, startCenteredPos.y - 5, 0);
        Player.transform.position = playerPosition;

        Random.Range(0, 100);

        // --- End Door ---
        Vector3Int EndDoorPos = FindWallSpot(EndRoomsCenter);
        tilemapWall.SetTile(EndDoorPos, null);
        tilemapFloor.SetTile(EndDoorPos, Floor);
        Vector3 endCenteredPos = new Vector3(EndDoorPos.x + 0.5f, EndDoorPos.y + 1.5f, 0);
        GameObject Endoor = Instantiate(EndDoorObj, endCenteredPos, Quaternion.identity);
        Endoor.GetComponent<SpriteRenderer>().sortingOrder = 100;


        
    }


    public Vector3Int FindWallSpot(Vector2Int RoomCenter)
    {
    
        Vector3Int WallCheckUp = new Vector3Int(RoomCenter.x, RoomCenter.y, 0);


        while (tilemapWall.GetTile(WallCheckUp) == null)
        {
            WallCheckUp.y++;
        }

        Vector3Int WallCheckLeft = new Vector3Int(RoomCenter.x, RoomCenter.y, 0);
        while (tilemapWall.GetTile(WallCheckLeft) == null) { WallCheckLeft.x--; }

        Vector3Int WallCheckRight = new Vector3Int(RoomCenter.x, RoomCenter.y, 0);
        while (tilemapWall.GetTile(WallCheckRight) == null) { WallCheckRight.x++; }

   
        int direction = Random.Range(0, 2);
        int chosenSide = WallCheckUp.x;

        switch (direction)
        {
            case 0:
          
                chosenSide = Random.Range(WallCheckLeft.x + 1, WallCheckUp.x);
                break;
            case 1:
                chosenSide = Random.Range(WallCheckUp.x, WallCheckRight.x - 1);
                break;
        }

    
        return new Vector3Int(chosenSide, WallCheckUp.y, 0);
    }
   
    
}

