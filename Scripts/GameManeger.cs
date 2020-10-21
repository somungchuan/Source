using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    private Room room1, room2;
    private FloorCreator floor;
    private List<Room> Path = new List<Room>();
    void Start()
    {
        floor = new FloorCreator(1);
        Debug.Log("floor initialized");
        Path = PathCreator.Instance.GetPath(floor);
        showPath();
        showFloor();
        Debug.Log("finished");
    }

    private void showFloor()
    {
        Debug.Log("======================三角形房间======================");
        foreach(Room room in floor.triangleRooms)
        {
            Debug.Log($"current room ID == {room.roomID}, canEnter is {room.canEnter}");
            for (int i = 0; i < room.Doors.Count; i++)
                Debug.Log($"                    the doors of this room is to room {room.Doors[i].roomID}, and this room canEnter is {room.Doors[i].canEnter}");           
        }
        Debug.Log("======================四边形房间======================");
        foreach (Room room in floor.rectangleRooms)
        {
            Debug.Log($"current room ID == {room.roomID},  canEnter is {room.canEnter}");
            for (int i = 0; i < room.Doors.Count; i++)
                Debug.Log($"                    the doors of this room is to room {room.Doors[i].roomID}, and this room canEnter is {room.Doors[i].canEnter}");
        }
        Debug.Log("======================五边形房间======================");
        foreach (Room room in floor.pentagonRooms)
        {
            Debug.Log($"current room ID == {room.roomID},  canEnter is {room.canEnter}");
            for (int i = 0; i < room.Doors.Count; i++)
                Debug.Log($"                    the doors of this room is to room {room.Doors[i].roomID}, and this room canEnter is {room.Doors[i].canEnter}");
        }
        Debug.Log("======================六边形房间======================");
        foreach (Room room in floor.hexagonRooms)
        {
            Debug.Log($"current room ID == {room.roomID},  canEnter is {room.canEnter}");
            for (int i = 0; i < room.Doors.Count; i++)
                Debug.Log($"                    the doors of this room is to room {room.Doors[i].roomID}, and this room canEnter is {room.Doors[i].canEnter}");
        }
    }
    private void showPath()
    {
        foreach(Room room in Path)
        {
            Debug.Log("croom's ID is " + room.roomID);
        }
    }
}
