using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap walls;
    public Tilemap floors;
    public TileBase wall_top;
    public TileBase wall_top_left;
    public TileBase wall_top_right;
    public TileBase wall_bottom;
    public TileBase wall_bottom_left;
    public TileBase wall_bottom_right;
    public TileBase wall_right;
    public TileBase wall_left;
    public TileBase wall_blank;

    public Dictionary<DoorEnum, MazeDoor> MazeDoors { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        MazeDoors = new Dictionary<DoorEnum, MazeDoor>
        {
            {DoorEnum.Door1, BuildMazeDoor(-1,-4,
                                            3, 3,
                                            wall_top, wall_top_left, wall_top_right,
                                            wall_bottom, wall_bottom_left, wall_bottom_right,
                                            wall_left, wall_right)}
        };
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateDoor(DoorEnum.Door1, DoorStateEnum.Close);
            Debug.Log(MazeDoors[DoorEnum.Door1].Open);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateDoor(DoorEnum.Door1, DoorStateEnum.Open);
            Debug.Log(MazeDoors[DoorEnum.Door1].Open);
        }
    }

    #region MazeDoor building
    /// <summary>
    /// BuildMazeDoor builds a MazeDoor that contains a list of doorTiles which is a "door" in the maze
    /// </summary>
    /// <param name="doorPositionX">Bottom left most x position out of all the tiles in the doorTile list</param>
    /// <param name="doorPositionY">Bottom left most y position out of all the tiles in the doorTile list</param>
    /// <param name="xLength">How far to the right of this x position is part of the door</param>
    /// <param name="yLength">How far above this y position is part of the door</param>
    /// <returns></returns>
    private MazeDoor BuildMazeDoor(int doorPositionX, int doorPositionY, int xLength, int yLength,
                                     TileBase closedTop, TileBase closedTopLeft, TileBase closedTopRight,
                                     TileBase closedBottom, TileBase closedBottomLeft, TileBase closedBottomRight,
                                     TileBase closedLeft, TileBase closedRight)
    {
        List<DoorTile> newDoorTileList = new List<DoorTile>();

        //Loop through the rows (bottom up)
        for (int y = doorPositionY; y < (doorPositionY + yLength); y++)
        {
            //Loop through the columns (left to right)
            for (int x = doorPositionX; x < (doorPositionX + xLength); x++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                #region Determine closedTile yikes
                //Determine closedTile
                TileBase closedTile = wall_blank;

                //BottomLeft
                if (x == doorPositionX && y == doorPositionY)
                {
                    closedTile = closedBottomLeft;
                }
                //BottomRight
                else if (x == (doorPositionX + xLength - 1) && y == doorPositionY)
                {
                    closedTile = closedBottomRight;
                }
                //TopLeft
                else if (y == (doorPositionY + yLength - 1) && x == doorPositionX)
                {
                    closedTile = closedTopLeft;
                }
                //TopRight
                else if (y == (doorPositionY + yLength - 1) && x == (doorPositionX + xLength - 1))
                {
                    closedTile = closedTopRight;
                }
                //Left
                else if (y > doorPositionY && x == (doorPositionX + xLength - 1) && yLength > 2)
                {
                    closedTile = closedLeft;
                }
                //Right
                else if (y > doorPositionY && x == doorPositionX && yLength > 2)
                {
                    closedTile = closedRight;
                }
                //Bottom
                else if (x > doorPositionX && y == (doorPositionY + yLength - 1) && xLength > 2)
                {
                    closedTile = closedBottom;
                }
                //Top
                else if (x > doorPositionX && y == doorPositionY && xLength > 2)
                {
                    closedTile = closedTop;
                }
                #endregion

                //Add newDoorTile to newDoor list
                if (floors.GetTile(tilePosition) == null)
                {
                    newDoorTileList.Add(BuildDoorTile(tilePosition, TilemapEnum.Walls, closedTile));
                }
                else
                {
                    newDoorTileList.Add(BuildDoorTile(tilePosition, TilemapEnum.Floors, closedTile));
                }
            }
        }
        return new MazeDoor(newDoorTileList);
    }

    /// <summary>
    /// BuildDoorTile, each doorTile will have two states open and closed
    /// </summary>
    /// <param name="tilePosition">Position of the doorTile</param>
    /// <param name="OpenDoorTileTilemap">Tilemap the </param>
    /// <param name="doorClosedTile">Tile the doorTile must used when it is closed, the open tile is assumed to be what the doorTile already is</param>
    /// <returns></returns>
    private DoorTile BuildDoorTile(Vector3Int tilePosition, TilemapEnum OpenDoorTileTilemap, TileBase doorClosedTile)
    {
        switch (OpenDoorTileTilemap)
        {
            case TilemapEnum.Floors:
                return new DoorTile(tilePosition, TilemapEnum.Floors, floors.GetTile(tilePosition), TilemapEnum.Walls, doorClosedTile);
            case TilemapEnum.Walls:
                return new DoorTile(tilePosition, TilemapEnum.Walls, walls.GetTile(tilePosition), TilemapEnum.Walls, doorClosedTile);
            default:
                throw new System.NotImplementedException();
        };
    }
    #endregion

    #region MazeDoor Control
    /// <summary>
    /// UpdateDoor will update a door given a door's Enum aka ID and the required door state OPEN/CLOSED
    /// </summary>
    /// <param name="door">Door ENUM/ID</param>
    /// <param name="doorState">Door State OPEN/CLOSED</param>
    public void UpdateDoor(DoorEnum door, DoorStateEnum doorState)
    {
        //Flip the Open status of the door
        MazeDoors[door].Open = !MazeDoors[door].Open;

        //Extract the DoorTiles
        List<DoorTile> doorTiles = MazeDoors[door].DoorTiles;

        //Loop through every tile in the door
        foreach (DoorTile doorTile in doorTiles)
        {
            switch (doorState)
            {
                //If the door is opening, update the doorTile to its opened state
                case DoorStateEnum.Open:
                    UpdateDoorTileSetter(doorTile.DoorTilePostion, doorTile.OpenTileTilemap, doorTile.OpenTile);
                    break;
                //If the door is closing, update the doorTile to its closed state
                case DoorStateEnum.Close:
                    UpdateDoorTileSetter(doorTile.DoorTilePostion, doorTile.ClosedTileTilemap, doorTile.ClosedTile);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }

    /// <summary>
    /// UpdateDoorTileSetter is a helper method for UpdateDoor that update indiviual tiles
    /// </summary>
    /// <param name="tilePostion">Position of the doorTile regardless of state</param>
    /// <param name="tilemapEnum">Wall tiles need to be added to walls tilemap and the opposite for floor tiles</param>
    /// <param name="tile">Tile</param>
    private void UpdateDoorTileSetter(Vector3Int tilePostion, TilemapEnum tilemapEnum, TileBase tile)
    {
        //Switch to correct tilemap for doors or floors
        switch (tilemapEnum)
        {
            case TilemapEnum.Floors:
                //Remove wall tile
                if (walls.GetTile(tilePostion))
                {
                    walls.SetTile(tilePostion, null);
                }
                //Place floor tile
                floors.SetTile(tilePostion, tile);
                break;
            case TilemapEnum.Walls:
                //Remove floor tile
                if (floors.GetTile(tilePostion))
                {
                    floors.SetTile(tilePostion, null);
                }
                //Place wall tile
                walls.SetTile(tilePostion, tile);
                break;
            default:
                throw new System.NotImplementedException();
        }
    }
    #endregion
}

/// <summary>
/// A MazeDoor acts as a container for all the door tiles and keeps track of whether the door is open or not
/// </summary>
public class MazeDoor
{
    public bool Open { get; set; }
    public List<DoorTile> DoorTiles { get; }

    public MazeDoor(List<DoorTile> doorTiles)
    {
        DoorTiles = doorTiles;
        Open = true;
    }
}

/// <summary>
/// A DoorTile Models a tile with two states open and closed. It also tracks the position of the doorTile and the tilemaps each state belongs to.
/// </summary>
public class DoorTile
{
    public Vector3Int DoorTilePostion { get; }
    public TilemapEnum OpenTileTilemap { get; }
    public TileBase OpenTile { get; }
    public TilemapEnum ClosedTileTilemap { get; }
    public TileBase ClosedTile { get; }

    public DoorTile(Vector3Int doorTilePostion, TilemapEnum openTileTilemap, TileBase openTile, TilemapEnum closedTileTilemap, TileBase closedTile)
    {
        DoorTilePostion = doorTilePostion;
        OpenTileTilemap = openTileTilemap;
        OpenTile = openTile;
        ClosedTileTilemap = closedTileTilemap;
        ClosedTile = closedTile;
    }
}

public enum DoorEnum
{
    Door1,
    Door2,
    Door3,
    Door4,
    Door5
}

public enum DoorStateEnum
{
    Open,
    Close
}

public enum TilemapEnum
{
    Floors,
    Walls
}