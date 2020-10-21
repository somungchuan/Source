using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleRoom : Room
{
    new public static int roomNum = 100;
    public static RoomShape defaultRoomShape = RoomShape.triangle;
    new public int doorNum = 3;

    public TriangleRoom()
    {
        roomID = roomNum++;
        roomShape = defaultRoomShape;
        Doors = new List<Room>();
    }
}
