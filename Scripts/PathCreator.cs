using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator
{
    private Room croom;
    private Room.RoomShape cshape;
    private List<Room> Path = new List<Room>();

    private static PathCreator instance = new PathCreator();

    public static PathCreator Instance { get => instance; set => instance = value; }

    private PathCreator()
    {
        
    }

    public List<Room> GetPath(FloorCreator floor)
    {
        do
        {
            CreateDefaultPath(floor);
        } while (Path.Count > 7);
        return Path;
    }
    
    public void CreateDefaultPath(FloorCreator floor)
    {
        
        if (floor.isFirstFloor)
        {
            int rdom = Random.Range(0, floor.exitPort.Doors.Count);
            croom = floor.exitPort.Doors[rdom];
            cshape = croom.roomShape;
            croom.canEnter = true;
            Path.Add(croom);
        }
        while(croom.roomShape != Room.RoomShape.triangle)
        {
            int rdom = Random.Range(0, croom.Doors.Count);
            while((croom.Doors[rdom].roomShape > cshape && croom.Doors[rdom].roomShape != Room.RoomShape.hexagon) 
                    || Path.Contains(croom.Doors[rdom]))
            {
                rdom = Random.Range(0, croom.Doors.Count);
            }
            //RemoveOtherDoors(croom, rdom);
            croom = croom.Doors[rdom];
            croom.canEnter = true;
            cshape = croom.roomShape;
            Path.Add(croom);
        }

    }

    private void RemoveOtherDoors(Room room, int index)
    {
        for(int i = 0; i < room.Doors.Count; i++)
        {
            if (i != index) room.Doors.Remove(room.Doors[i]);
        }
    }
}
