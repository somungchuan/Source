using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class FloorCreator
{
    public Room exitPort;
    public List<Room> triangleRooms = new List<Room>();
    public List<Room> rectangleRooms = new List<Room>();
    public List<Room> pentagonRooms = new List<Room>();
    public List<Room> hexagonRooms = new List<Room>();

    public bool isFirstFloor;
    public bool isLastFloor;

    public FloorCreator(int floorID)
    {
        isFirstFloor = floorID == 1;
        int triangleNum = 1;
        int rectangleNum = 3;
        int pentagonNum = 6;
        int hexagonNum = 6;
        //创建楼层房间
        //三角形房间
        while (triangleNum-- > 0)
        {
            Room room = new TriangleRoom();
            triangleRooms.Add(room);
        }
        // 四边形房间
        while (rectangleNum-- > 0)
        {
            rectangleRooms.Add(new RectangleRoom());
        }
        // 五边形房间
        while (pentagonNum-- > 0)
        {
            pentagonRooms.Add(new PentagonRoom());
        }
        // 六边形房间
        while (hexagonNum-- > 0)
        {
            hexagonRooms.Add(new HexagonRoom());
        }
        // 出口
        exitPort = new Room();
        exitPort.doorNum = 9;
        exitPort.roomID = 140;
        exitPort.roomShape = Room.RoomShape.world;

        // 创建通路
        // 三角形
        foreach (TriangleRoom room in triangleRooms)
        {
            for (int j = 0; j < room.doorNum; j++)
            {
                room.Doors.Add(rectangleRooms[j]);
            }
        }
        // 四边形
        foreach (RectangleRoom room in rectangleRooms)
        {
            int index = GetCurrentRoomIndex(room);
            int currentID = rectangleRooms[index].roomID;
            int lastIndex = (index - 1) < 0 ?
                            (rectangleRooms.Count - 1) :
                            (index - 1);
            int nextIndex = (index + 1) == rectangleRooms.Count ?
                            0 :
                            (index + 1);

            // 连三角形
            room.Doors.Add(triangleRooms[0]);
            // 连四边形
            room.Doors.Add(rectangleRooms[lastIndex]);
            room.Doors.Add(rectangleRooms[nextIndex]);
            // 连五边形
            int targetID = (currentID / 10) * 10 + 10 + (currentID % 10) * 2;
            room.Doors.Add(pentagonRooms.Find(temp => temp.roomID == targetID));
        }
        // 五边形
        foreach(Room room in pentagonRooms)
        {
            int index = GetCurrentRoomIndex(room);
            int currentID = pentagonRooms[index].roomID;
            int lastIndex = (index - 1) < 0 ?
                            (pentagonRooms.Count - 1) :
                            (index - 1);
            int nextIndex = (index + 1) == pentagonRooms.Count ?
                            0 :
                            (index + 1);
            // 偶数房间连四边形
            if (room.roomID % 2 == 0)
            {
                // 连四边形
                int targetID = (room.roomID / 10 - 1) * 10 + (room.roomID % 10 / 2);
                room.Doors.Add(rectangleRooms.Find(temp => temp.roomID == targetID));
            }
            // 连五边形
            room.Doors.Add(pentagonRooms[lastIndex]);
            room.Doors.Add(pentagonRooms[nextIndex]);
            // 连六边形
            room.Doors.Add(hexagonRooms[lastIndex]);
            room.Doors.Add(hexagonRooms[index]);
            // 奇数房间连出口
            if (room.roomID % 2 != 0)
            {
                room.Doors.Add(exitPort);
                exitPort.Doors.Add(room);
            }
        }
        // 六边形
        foreach(Room room in hexagonRooms)
        {
            int index = GetCurrentRoomIndex(room);
            int currentID = hexagonRooms[index].roomID;
            int nextIndex = (index + 1) == hexagonRooms.Count ?
                            0 :
                            (index + 1);
            // 连五边形
            room.Doors.Add(pentagonRooms[index]);
            room.Doors.Add(pentagonRooms[nextIndex]);
            // 连出口
            room.Doors.Add(exitPort);
            exitPort.Doors.Add(room);
        }
    }


    private int GetCurrentRoomIndex(Room room)
    {
        switch (room.roomShape)
        {
            case Room.RoomShape.triangle:
                return triangleRooms.FindIndex(temp => temp.roomID == room.roomID);
            case Room.RoomShape.rectangle:
                return rectangleRooms.FindIndex(temp => temp.roomID == room.roomID);
            case Room.RoomShape.pentagon:
                return pentagonRooms.FindIndex(temp => temp.roomID == room.roomID);
            case Room.RoomShape.hexagon:
                return hexagonRooms.FindIndex(temp => temp.roomID == room.roomID);
        }

        return -1;
    }
    
}
