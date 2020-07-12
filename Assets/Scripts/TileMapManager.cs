using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap walls;
    public Tilemap floors;
    public Tilemap tilemapFog;
    [SerializeField] private TileBase darkFog;
    [SerializeField] private TileBase wall_top;
    [SerializeField] private TileBase wall_top_left_corner;
    [SerializeField] private TileBase wall_top_right_corner;
    [SerializeField] private TileBase wall_top_left_piece;
    [SerializeField] private TileBase wall_top_right_piece;
    [SerializeField] private TileBase wall_bottom;
    [SerializeField] private TileBase wall_bottom_left_corner;
    [SerializeField] private TileBase wall_bottom_right_corner;
    [SerializeField] private TileBase wall_bottom_left_piece;
    [SerializeField] private TileBase wall_bottom_right_piece;
    [SerializeField] private TileBase wall_right;
    [SerializeField] private TileBase wall_left;
    [SerializeField] private TileBase wall_blank;

    public Dictionary<MazeDoorEnum, MazeDoor> MazeDoors { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        #region Create Maze Doors Here
        MazeDoors = new Dictionary<MazeDoorEnum, MazeDoor>
        {
            {MazeDoorEnum.Door1, BuildMazeDoor(-1, 3,
                                            3, 2,
                                            wall_top, wall_bottom_left_piece, wall_bottom_right_piece,
                                            wall_bottom, wall_top_left_piece, wall_top_right_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door2, BuildMazeDoor(6, 0,
                                            3, 2,
                                            wall_top, wall_bottom_left_piece, wall_bottom_right_piece,
                                            wall_bottom, wall_top_left_piece, wall_top_right_piece,
                                            wall_left, wall_right)}
        };
        #endregion
    }

    //Test code
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UpdateDoor(MazeDoorEnum.Door1, MazeDoorState.Close);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdateDoor(MazeDoorEnum.Door1, MazeDoorState.Open);
        }
    }

    #region MazeDoor building
    /// <summary>
    /// BuildMazeDoor builds a MazeDoor that contains a list of doorTiles which acts a "door" in the maze.
    /// Requires the programmer to input the correct textures in the correct areas.
    /// Works for 2x2 2x3 3x2 ...
    /// If you want to block 2 spaces in a corridor use a 3x2 and include the walls adjacent to the two spaces.
    /// </summary>
    /// <param name="doorPositionX">Bottom left most x position out of all the tiles in the doorTile list</param>
    /// <param name="doorPositionY">Bottom left most y position out of all the tiles in the doorTile list</param>
    /// <param name="xLength">How far to the right of this x position is part of the door</param>
    /// <param name="yLength">How far above this y position is part of the door</param>
    /// <returns>Maze Door</returns>
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
                else if (y > doorPositionY && x == (doorPositionX + xLength - 1))
                {
                    closedTile = closedLeft;
                }
                //Right
                else if (y > doorPositionY && x == doorPositionX)
                {
                    closedTile = closedRight;
                }
                //Bottom
                else if (x > doorPositionX && y == (doorPositionY + yLength - 1))
                {
                    closedTile = closedBottom;
                }
                //Top
                else if (x > doorPositionX && y == doorPositionY)
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
        return new MazeDoor(new Vector3Int(doorPositionX, doorPositionY, 0), newDoorTileList);
    }

    /// <summary>
    /// BuildDoorTile, each doorTile will have two states open and closed
    /// </summary>
    /// <param name="tilePosition">Position of the doorTile</param>
    /// <param name="OpenDoorTileTilemap">Tilemap the </param>
    /// <param name="doorClosedTile">Tile the doorTile must used when it is closed, the open tile is assumed to be what the doorTile already is</param>
    /// <returns>Door Tile</returns>
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
    /// <param name="door">Door ENUM ID</param>
    /// <param name="doorState">Door State OPEN CLOSED AUTO</param>
    public void UpdateDoor(MazeDoorEnum door, MazeDoorState doorState)
    {
        switch (doorState)
        {
            case MazeDoorState.Auto:
                if (MazeDoors[door].Open)
                {
                    doorState = MazeDoorState.Close;
                }
                else
                {
                    doorState = MazeDoorState.Open;
                }
                MazeDoors[door].Open = !MazeDoors[door].Open;
                break;
            case MazeDoorState.Open:
                MazeDoors[door].Open = true;
                break;
            case MazeDoorState.Close:
                MazeDoors[door].Open = false;
                break;
            default:
                throw new NotImplementedException();
        }

        //Extract the DoorTiles
        List<DoorTile> doorTiles = MazeDoors[door].DoorTiles;

        //Loop through every tile in the door
        foreach (DoorTile doorTile in doorTiles)
        {
            switch (doorState)
            {
                //If the door is opening, update the doorTile to its opened state
                case MazeDoorState.Open:
                    UpdateDoorTileSetter(doorTile.DoorTilePostion, doorTile.OpenTileTilemap, doorTile.OpenTile);
                    break;
                //If the door is closing, update the doorTile to its closed state
                case MazeDoorState.Close:
                    UpdateDoorTileSetter(doorTile.DoorTilePostion, doorTile.ClosedTileTilemap, doorTile.ClosedTile);
                    break;
                default:
                    throw new NotImplementedException();
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
                throw new NotImplementedException();
        }
        tilemapFog.SetTile(tilePostion, darkFog);
    }

    /// <summary>
    /// Open all maze doors
    /// </summary>
    public void OpenAllMazeDoors()
    {
        foreach (MazeDoorEnum mazeDoor in MazeDoors.Keys)
        {
            UpdateDoor(mazeDoor, MazeDoorState.Open);
        }
    }

    /// <summary>
    /// Close all maze doors
    /// </summary>
    public void CloseAllMazeDoors()
    {
        foreach (MazeDoorEnum mazeDoor in MazeDoors.Keys)
        {
            UpdateDoor(mazeDoor, MazeDoorState.Close);
        }
    }

    public Vector3Int MazeDoorPosition(MazeDoorEnum door)
    {
        return MazeDoors[door].DoorPosition;
    }
    #endregion
}

/// <summary>
/// A MazeDoor acts as a container for all the door tiles and keeps track of whether the door is open or not
/// </summary>
public class MazeDoor
{
    public bool Open { get; set; }
    public Vector3Int DoorPosition { get; }
    public List<DoorTile> DoorTiles { get; }

    public MazeDoor(Vector3Int doorPosition, List<DoorTile> doorTiles)
    {
        Open = true;
        DoorPosition = doorPosition;
        DoorTiles = doorTiles;
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

public enum MazeDoorEnum
{
    Door1,
    Door2,
    Door3,
    Door4,
    Door5
}

public enum MazeDoorState
{
    Open,
    Close,
    Auto
}

public enum TilemapEnum
{
    Floors,
    Walls
}