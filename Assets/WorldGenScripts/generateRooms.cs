using UnityEngine;
using UnityEngine.Tilemaps;

public class generateRooms : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile LeftWall;
    public Tile RightWall;
    public Tile TopWall;
    public Tile BottomWall;
   
    public GameObject Player;
    public void GenStartEndRoom(Vector2Int randomStartRoom)
    {
        int startroomRadius = 4;
        //create a 5x5 room with corrasponding tile at the correct place
      
        for (int x  = -startroomRadius; x <= startroomRadius; x++)
        {
            for (int y = -startroomRadius; y <= startroomRadius; y++) 
            {
                Vector3Int tilePos = new Vector3Int(randomStartRoom.x + x, randomStartRoom.y + y, 0);
                if (x == -startroomRadius || x == startroomRadius || y == -startroomRadius || y == startroomRadius)
                {
                    tilemap.SetTile(tilePos, BottomWall);
                }
            }
        }

        //Generate the door

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: //left
                for(int i = -1; i <= 1; i++)
                {
                    tilemap.SetTile(new Vector3Int(randomStartRoom.x - startroomRadius, randomStartRoom.y + i, 0), null);
                }
                break;
            case 1: // Right 
                for (int i = -1; i <= 1; i++)
                {
                    tilemap.SetTile(new Vector3Int(randomStartRoom.x + startroomRadius, randomStartRoom.y + i, 0), null);
                }
                break;

            case 2: // Top
                for (int i = -1; i <= 1; i++)
                {
                    tilemap.SetTile(new Vector3Int(randomStartRoom.x + i, randomStartRoom.y + startroomRadius, 0), null);
                }
                break;

            case 3: // Bottom 
                for (int i = -1; i <= 1; i++)
                {
                    tilemap.SetTile(new Vector3Int(randomStartRoom.x + i, randomStartRoom.y - startroomRadius, 0), null);
                }
                break;
        }

        Player.transform.position = new Vector3(randomStartRoom.x , randomStartRoom.y, 0);
    }
}

