using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonRoom : Room
{
    new public static int roomNum = 130;
    new public int doorNum = 3;
    public static RoomShape defaultRoomShape = RoomShape.hexagon;

    public HexagonRoom()
    {
        roomID = roomNum++;
        roomShape = defaultRoomShape;
        Doors = new List<Room>();
    }
}
