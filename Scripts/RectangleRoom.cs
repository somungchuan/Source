using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleRoom : Room
{
    new public static int roomNum = 110;
    public static RoomShape defaultRoomShape = RoomShape.rectangle;
    new public int doorNum = 4;

    

    public RectangleRoom()
    {
        roomID = roomNum++;
        roomShape = defaultRoomShape;
        Doors = new List<Room>();
    }
}
