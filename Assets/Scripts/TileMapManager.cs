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
            {MazeDoorEnum.Door0, BuildMazeDoor(25, 9,
                                            2, 3,
                                            wall_top, wall_right, wall_top_left_piece,
                                            wall_bottom, wall_right, wall_bottom_left_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door1, BuildMazeDoor(21, 13,
                                            3, 2,
                                            wall_top, wall_bottom_left_piece, wall_bottom_right_piece,
                                            wall_bottom, wall_top, wall_top,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door2, BuildMazeDoor(21, 6,
                                            3, 2,
                                            wall_top, wall_bottom, wall_bottom,
                                            wall_bottom, wall_top_left_piece, wall_top_right_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door3, BuildMazeDoor(3, 11,
                                            3, 2,
                                            wall_top, wall_bottom_left_piece, wall_bottom_right_piece,
                                            wall_bottom, wall_top, wall_top_right_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door4, BuildMazeDoor(1, 4,
                                            3, 2,
                                            wall_top, wall_bottom, wall_bottom,
                                            wall_bottom, wall_top_left_piece, wall_top_right_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door5, BuildMazeDoor(34, 6,
                                            2, 3,
                                            wall_top, wall_right, wall_top_left_piece,
                                            wall_bottom, wall_right, wall_bottom_left_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door6, BuildMazeDoor(55, 0,
                                            2, 3,
                                            wall_top, wall_top_right_piece, wall_left,
                                            wall_bottom, wall_bottom_right_piece, wall_bottom_left_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door7, BuildMazeDoor(55, 6,
                                            2, 3,
                                            wall_top, wall_top_right_piece, wall_left,
                                            wall_bottom, wall_bottom_right_piece, wall_left,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door8, BuildMazeDoor(55, 12,
                                            2, 3,
                                            wall_top, wall_top_right_piece, wall_top_left_piece,
                                            wall_bottom, wall_bottom_right_piece, wall_left,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door9, BuildMazeDoor(67, 0,
                                            2, 3,
                                            wall_top, wall_right, wall_top_left_piece,
                                            wall_bottom, wall_bottom_right_piece, wall_bottom_left_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door10, BuildMazeDoor(67, 6,
                                            2, 3,
                                            wall_top, wall_right, wall_top_left_piece,
                                            wall_bottom, wall_right, wall_bottom_left_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door11, BuildMazeDoor(67, 12,
                                            2, 3,
                                            wall_top, wall_top_right_piece, wall_top_left_piece,
                                            wall_bottom, wall_right, wall_bottom_left_piece,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door12, BuildMazeDoor(37, 21,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_blank)},

            {MazeDoorEnum.Door13, BuildMazeDoor(81, 29,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door14, BuildMazeDoor(3, 31,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door15, BuildMazeDoor(58, 34,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door16, BuildMazeDoor(35, 38,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door17, BuildMazeDoor(22, 42,
                                            2, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door18, BuildMazeDoor(71, 46,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door19, BuildMazeDoor(80, 45,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door20, BuildMazeDoor(65, 54,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door21, BuildMazeDoor(78, 54,
                                            3, 4,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door22, BuildMazeDoor(37, 56,
                                            2, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door23, BuildMazeDoor(57, 56,
                                            2, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door24, BuildMazeDoor(70, 65,
                                            3, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door25, BuildMazeDoor(80, 79,
                                            4, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door26, BuildMazeDoor(59, 79,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door27, BuildMazeDoor(75, 82,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door28, BuildMazeDoor(46, 75,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door29, BuildMazeDoor(37, 82,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door30, BuildMazeDoor(32, 66,
                                            2, 3,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door31, BuildMazeDoor(15, 59,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door32, BuildMazeDoor(15, 72,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door33, BuildMazeDoor(6, 76,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door34, BuildMazeDoor(15, 85,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door35, BuildMazeDoor(10, 94,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)},

            {MazeDoorEnum.Door36, BuildMazeDoor(5, 59,
                                            3, 2,
                                            wall_top, wall_top_left_corner, wall_top_right_corner,
                                            wall_bottom, wall_bottom_left_corner, wall_bottom_right_corner,
                                            wall_left, wall_right)}
        };
        #endregion
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
    Door0,
    Door1,
    Door2,
    Door3,
    Door4,
    Door5,
    Door6,
    Door7,
    Door8,
    Door9,
    Door10,
    Door11,
    Door12,
    Door13,
    Door14,
    Door15,
    Door16,
    Door17,
    Door18,
    Door19,
    Door20,
    Door21,
    Door22,
    Door23,
    Door24,
    Door25,
    Door26,
    Door27,
    Door28,
    Door29,
    Door30,
    Door31,
    Door32,
    Door33,
    Door34,
    Door35,
    Door36,
    Door37
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