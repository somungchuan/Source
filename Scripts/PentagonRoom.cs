using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonRoom : Room
{
    new public static int roomNum = 120;
    public static RoomShape defaultRoomShape = RoomShape.pentagon;
    new public int doorNum = 5;

    public PentagonRoom()
    {
        roomID = roomNum++;
        roomShape = defaultRoomShape;
        Doors = new List<Room>();
    }
}
