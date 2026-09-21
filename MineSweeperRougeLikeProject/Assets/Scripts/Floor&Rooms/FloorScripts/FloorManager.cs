using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class FloorManager : MonoBehaviour
{
    public Room roomPreset;
    public int currentRoomAmount;
    public Room roomPresetShop;
    public int currentShopAmount;
    public Room roomPresetElite;
    public int currentEliteAmount;

    //public FloorGrid grid;

    private Vector2 startPos;

    public bool AfterFirstMove;
    
    public List<Room> _rooms;
    public List<int> _dangerRooms;

    public Room currentRoom;

    [SerializeField] private List<FloorRoomButton> floorRoomButtons;


    //To disable
    public PlayerInput inputHandler;
    public BossRoomSquare bossRoom;

    private GameObject bossRoomContainer;

    public void Start()
    {
        RunPlayerStats.Instance.FloorManager = this;
        RunPlayerStats.Instance.SetBossModification();
        bossRoomContainer = bossRoom.gameObject.transform.parent.gameObject;
        BeginLogic();
    }

    public void BeginLogic()
    {
        _rooms = new List<Room>();
        _dangerRooms = new List<int>();

        currentRoomAmount = RunPlayerStats.Instance.RoomCount;
        currentShopAmount = RunPlayerStats.Instance.ShopCount;
        currentEliteAmount = RunPlayerStats.Instance.EliteRoomCount;

        SetDangerRoomsOrder();

        AddRoomToSelection("RoomMine");
        AddRoomToSelection("RoomShop");
    }

/*
    private void SetRooms()
    {
        //Adds basic rooms
        AddRoom(roomPreset, RunPlayerStats.Instance.RoomCount);
        AddRoom(roomPresetShop, RunPlayerStats.Instance.ShopCount);
        AddRoom(roomPresetElite, RunPlayerStats.Instance.EliteRoomCount); 
    }
    */

    public List<Room> AddRoom(Room roomObject, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject roomInst = Instantiate(roomObject.gameObject);
            Room room = roomInst.GetComponent<Room>();
            _rooms.Add(room);
        }
        return _rooms;
    }
    
    public List<Room> AddBasicRoom(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject roomInst = Instantiate(roomPreset.gameObject);
            Room room = roomInst.GetComponent<Room>();
            _rooms.Add(room);
        }
        return _rooms;
    }
    
    public List<Room> AddShopRoom(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject roomInst = Instantiate(roomPresetShop.gameObject);
            Room room = roomInst.GetComponent<Room>();
            _rooms.Add(room);
        }
        return _rooms;
    }
    
    public List<Room> AddEliteRoom(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject roomInst = Instantiate(roomPresetElite.gameObject);
            Room room = roomInst.GetComponent<Room>();
            _rooms.Add(room);
        }
        return _rooms;
    }

    public void SetDangerRoomsOrder()
    {
        int tempCRA = currentRoomAmount;
        int tempCEA = currentEliteAmount;
        int total = tempCRA + tempCEA;

        for (int i = 0; i<=total && tempCEA>0 && tempCRA>0; i++)
        {
            //0 is normal room, 1 is elite room
            int result = Random.Range((int)0, (int)2);

            //First one should always be a normal room
            if (i is 0) result = 0;

            _dangerRooms.Add(result);
            if (result is 0) tempCRA--;
            else tempCEA--;
        }

        if(tempCRA > 0) for (; tempCRA is > 0; tempCRA--)_dangerRooms.Add(0);
        if(tempCEA > 0) for (; tempCEA is > 0; tempCEA--)_dangerRooms.Add(1);
    }

    public Room GetAndCreateCurrentDangerRoom()
    {
        //TODO Probs should be something like a empty method here, but this will do for now
        if(RunPlayerStats.Instance.RoomCountCleared > _dangerRooms.Count-1) return AddBasicRoom().Last();
        return _dangerRooms[RunPlayerStats.Instance.RoomCountCleared] == 0 ? AddBasicRoom().Last() : AddEliteRoom().Last();
    }

    public void RoomExited(Room room)
    {
        AddRoomToSelection(room);
    }

    public void AddRoomToSelection(string roomType)
    {
        switch (roomType)
        {
            case "RoomShop":
                FloorButtonShopRoomSet(floorRoomButtons[1], currentShopAmount<=0);
                currentShopAmount--;
            break;
            case not "RoomShop":
                FloorButtonDangerRoomSet(floorRoomButtons[0], currentRoomAmount+currentEliteAmount<=0);
                currentEliteAmount--;
            break;
        }
    }

    public void AddRoomToSelection(Room room)
    {
        switch (room)
        {
            case RoomShop:
                FloorButtonShopRoomSet(floorRoomButtons[1], currentShopAmount<=0);
                currentShopAmount--;
            break;
            case not RoomShop:
                FloorButtonDangerRoomSet(floorRoomButtons[0], currentRoomAmount+currentEliteAmount<=0);
                currentEliteAmount--;
            break;
        }
    }

    public void FloorButtonDangerRoomSet(FloorRoomButton floorRoomButton, bool Condition = false)
    {
        floorRoomButton.room = GetAndCreateCurrentDangerRoom();
        floorRoomButton.SetVisual(Condition);
    }

    public void FloorButtonShopRoomSet(FloorRoomButton floorRoomButton, bool Condition = false)
    {
        //TODO should add instead a psuedo room here if it is too much rather than the otherway around
        floorRoomButton.room = AddShopRoom().Last();
        floorRoomButton.SetVisual(Condition);
    }

    public void ResetBoard()
    {
        foreach (var room in _rooms)
        {
            Destroy(room.gameObject);
        }
        _rooms.Clear();
        _dangerRooms.Clear();

        currentRoomAmount = RunPlayerStats.Instance.RoomCount;
        currentShopAmount = RunPlayerStats.Instance.ShopCount;
        currentEliteAmount = RunPlayerStats.Instance.EliteRoomCount;
        SetDangerRoomsOrder();

        AddRoomToSelection("RoomMine");
        AddRoomToSelection("RoomShop");

        bossRoom.squareRevealed = false;
        bossRoom.SetRevealed(false);
        bossRoom.SetActive(false);

        AfterFirstMove = false;
        
        RunPlayerStats.Instance.SetBossModification();

    }

    public void DisableFloor(bool state)
    {
        this.gameObject.SetActive(state);
        //grid.gameObject.SetActive(state);
        //inputHandler.gameObject.SetActive(state);
        bossRoomContainer.SetActive(state);
        floorRoomButtons[0].transform.parent.gameObject.SetActive(state);
    }

    public BossRoomSquare _bossRoomSquare;
}
