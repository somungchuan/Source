using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public enum RoomShape
    {
        triangle,
        rectangle,
        pentagon,
        hexagon,
        world
    }

    public int roomNum;
    public int roomID;
    public RoomShape roomShape;
    public int doorNum;
    public List<Room> Doors;

    public bool canEnter = false;

    public bool haveTrack = false;

    public Room()
    {
        Doors = new List<Room>();
    }

    //public void AddDoor(Room room)
    //{
    //    if (Doors.Count >= this.doorNum) return;
    //    Doors.Add(room);
    //}

    //public void RemoveDoor(Room room)
    //{
    //    if (Doors.Count <= 0) return;
    //    Doors.Remove(room);
    //}

    public int GetRoomID()
    {
        return roomID;
    }
}
